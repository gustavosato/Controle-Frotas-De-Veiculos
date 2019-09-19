using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.EquipmentAccessories;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.EquipmentAccessories;
using Lean.Test.Cloud.Domain.Entities.EquipmentAccessories;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;
using System.Globalization;
using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.MVC.Models.Historicals;
using FluentValidation.Mvc;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class EquipmentAccessorieController : BaseController
    {
        private readonly IEquipmentAccessorieService _equipmentAccessorieService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IHistoricalService _historicalService;
        private readonly IProfilesService _profilesService;
        private readonly IUserService _userService;

        public EquipmentAccessorieController(IEquipmentAccessorieService equipmentAccessorieService,
                                             ICustomerService customerService,
                                             IUserService userService,
                                             IProfilesService profilesService,
                                             IHistoricalService historicalService,
                                             IParameterValueService parameterValueService)
        {
            _userService = userService;
            _equipmentAccessorieService = equipmentAccessorieService;
            _customerService = customerService;
            _profilesService = profilesService;
            _historicalService = historicalService;
            _parameterValueService = parameterValueService;
        }

        private string SystemFeatureID = "314";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new EquipmentAccessorieModel();
            var assignTos = _userService.GetAll(0);
            var types = _parameterValueService.GetAllByParameterID("314300");

            model.SearchLoadAssignTo = assignTos.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

       
        [RuleSetForClientSideMessages("MyRuleset")]
        [HttpPost]
        public ActionResult Add(EquipmentAccessorieModel model)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterProfileCommand
                {
                    AllowAdd = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para adicionar um registro em Inventário!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceEquipmentAccessorieCommand(model);

                    _equipmentAccessorieService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso!", model.ModelNames));

                    return RedirectToAction("Index");
                }
                ErrorNotification(string.Format("Não foi possível realizar o registro!"));

                return RedirectToAction("Index");

            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, EquipmentAccessorieModel model)
        {
            var gridModel = new DataSourceResult();

            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowView = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para visualizar os registros de apropriação de horas!");

                return Json(gridModel);
            }
            else
            {
                var movimentEmployees = _equipmentAccessorieService.GetAll(new FilterEquipmentAccessorieCommand
                {
                    AssignToID = model.SearchAssignToID,
                    TypeID = model.SearchTypeID,

                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = movimentEmployees.Select(x =>
                    {
                        var movimentEmployeeModel = x.ToModel();

                        return movimentEmployeeModel;
                    }),
                    Total = movimentEmployees.TotalCount
                };
                return Json(gridModel);
            }
        }
        public ActionResult New()
        {
            var model = new EquipmentAccessorieModel();
            var users = _userService.GetAll(0);
            var types = _parameterValueService.GetAllByParameterID("314300");

            model.LoadAssignTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


            return PartialView("Maintenance", model);
        }

        //public ActionResult LoadActiveDesactiveCustomer()
        //{
            //return PartialView("ActiveDesactiveCustomer");
        //}

        private MaintenanceEquipmentAccessorieCommand MaintenanceEquipmentAccessorieCommand(EquipmentAccessorieModel model)
        {
            MaintenanceEquipmentAccessorieCommand command = new MaintenanceEquipmentAccessorieCommand();

            command.EquipmentAccessorieID = model.EquipmentAccessorieID;
            command.Description = model.Description;
            command.SerialNumber = model.SerialNumbers;
            command.ModelName = model.ModelNames;
            command.AssignToID = model.AssignToID;
            command.TypeID = model.TypeID;
            command.Invoicing = model.Invoicing;
            if (model.AmountInvoicing != null) command.AmountInvoicing = model.AmountInvoicing.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            command.StartDate = model.StartDate;
            command.EndDate = model.EndDate;


            return command;
        }

        public ActionResult GetByID(int equipmentAccessorieID, string ActionName)
        {
            var model = new EquipmentAccessorieModel();

            Result<EquipmentAccessorie> EquipmentAccessorie = _equipmentAccessorieService.GetByID(equipmentAccessorieID);

            if (EquipmentAccessorie.IsSuccess)
            {
                model = EquipmentAccessorie.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var assignTos = _userService.GetAll(0);
                    var types = _parameterValueService.GetAllByParameterID("314300");
                    var users = _userService.GetAll(Convert.ToInt32(model.AssignToID));

                    model.LoadAssignTo = assignTos.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                    model.LoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                                                  
                if (model.AmountInvoicing != null) model.AmountInvoicing = String.Format(new CultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(model.AmountInvoicing.Replace(",", ".")));

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }

            return RedirectToAction("Index", "EquipmentAccessorie");
        }

        public ActionResult Delete(int equipmentAccessorieID)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterProfileCommand
                {
                    AllowDelete = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para excluir um registro em inventário!");

                    return RedirectToAction("Index");
                }

                if (equipmentAccessorieID == 0)
                {
                    ErrorNotification(string.Format("Registro não pode ser excluído! "));

                    return Redirect("Index");
                }
                var model = new EquipmentAccessorieModel();

                Result<EquipmentAccessorie> equipmentAccessorie = _equipmentAccessorieService.GetByID(equipmentAccessorieID);

                if (equipmentAccessorie.IsSuccess)
                {
                    model = equipmentAccessorie.Value.ToModel();

                    _equipmentAccessorieService.Delete(model.EquipmentAccessorieID);

                    _historicalService.Delete(SystemFeatureID, equipmentAccessorieID);
                    
                    SuccessNotification(string.Format("Registro do equipamento/acessório excluido com sucesso! Equipamento/Acessório: {0}", model.ModelNames));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("A aplicação contêm funcionalidades associadas, exclua primeiro as funcionalidades.");

                return RedirectToAction("Index");
            }
        }

        
        [HttpPost]
        public ActionResult Update(EquipmentAccessorieModel model)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterProfileCommand
                {
                    AllowUpdate = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para atualizar um registro em Inventário!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    //historical
                    Historical(model);


                    var command = MaintenanceEquipmentAccessorieCommand(model);

                    _equipmentAccessorieService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso!"));

                    return RedirectToAction("Index");
                }
                ErrorNotification("Não foi possível realizar alteração!");

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("O registro não pode ser alterado!"));

                return RedirectToAction("Index");
            }
        }
        //Realizando histórico de alterações 


        private void Historical(EquipmentAccessorieModel model)
        {
            var command = new EquipmentAccessorieModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _equipmentAccessorieService.GetByID(model.EquipmentAccessorieID);

            command = LocalCommand.Value.ToModel();

            if (command.AssignToID != model.AssignToID)
            {
                string commandAssignToID = _userService.GetUserNameByID(Convert.ToInt32(command.AssignToID));

                string modelAssignToID = _userService.GetUserNameByID(Convert.ToInt32(model.AssignToID));

                AddHistorical(commandAssignToID, modelAssignToID, "Responsável por executar a tarefa", model.EquipmentAccessorieID.ToString());
            }

            if (command.TypeID != model.TypeID) AddHistorical(model.TypeID, command.TypeID, "Tipo", model.TypeID.ToString(), true);
            //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.EquipmentAccessorieID.ToString());
            if (command.ModelNames != model.ModelNames) AddHistorical(command.ModelNames, model.ModelNames, "Nome do Modelo", model.EquipmentAccessorieID.ToString());
            if (command.SerialNumbers != model.SerialNumbers) AddHistorical(command.SerialNumbers, model.SerialNumbers, "Número de Série", model.EquipmentAccessorieID.ToString());
            if (command.AmountInvoicing != model.AmountInvoicing) AddHistorical(command.AmountInvoicing, model.AmountInvoicing, "Valor de Faturamento", model.EquipmentAccessorieID.ToString());
            if (command.StartDate != model.StartDate) AddHistorical(command.StartDate, model.StartDate, "Início Vigência", model.EquipmentAccessorieID.ToString());
            if (command.EndDate != model.EndDate) AddHistorical(command.EndDate, model.EndDate, "Término Vigência", model.EquipmentAccessorieID.ToString());

        }

        private void AddHistorical(string oldValue, string newValue, string fieldName, string recordID, bool isParameter = false)
        {
            var model = new HistoricalModel();

            if (isParameter)
            {
                oldValue = _parameterValueService.GetParameterValueByID(Convert.ToInt32(oldValue));
                newValue = _parameterValueService.GetParameterValueByID(Convert.ToInt32(newValue));
            }
            model.OldValue = oldValue;
            model.NewValue = newValue;
            model.SystemFeatureID = SystemFeatureID;
            model.RecordID = recordID;
            model.FieldName = fieldName;
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            var command = MaintenanceHistoricalCommand(model);

            _historicalService.Add(command);

        }

        private MaintenanceHistoricalCommand MaintenanceHistoricalCommand(HistoricalModel model)
        {
            MaintenanceHistoricalCommand command = new MaintenanceHistoricalCommand();

            command.HistoricalID = model.HistoricalID;
            command.SystemFeatureID = model.SystemFeatureID;
            command.RecordID = model.RecordID;
            command.OldValue = model.OldValue;
            command.NewValue = model.NewValue;
            command.FieldName = model.FieldName;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }


    }
}