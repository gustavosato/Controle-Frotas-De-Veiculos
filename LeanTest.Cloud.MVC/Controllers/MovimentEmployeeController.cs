using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.MovimentEmployees;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.MovimentEmployees;
using Lean.Test.Cloud.Domain.Entities.MovimentEmployees;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;
using Lean.Test.Cloud.MVC.Models.SystemParameter;
using Lean.Test.Cloud.MVC.Models.Users;




namespace Lean.Test.Cloud.MVC.Controllers
{
    public class MovimentEmployeeController : BaseController
    {
        private readonly IMovimentEmployeeService _movimentEmployeeService;
        private readonly ICustomerService _customerService;
        private readonly IProfilesService _profilesService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemParameterService _systemParameterService;



        public MovimentEmployeeController(IMovimentEmployeeService movimentEmployeeService,
                                    ICustomerService customerService,
                                    IProfilesService profilesService,
                                    IUserService userService,
                                    ISystemParameterService systemParameterService,
                                    IParameterValueService parameterValueService)

        {
            _userService = userService;
            _movimentEmployeeService = movimentEmployeeService;
            _profilesService = profilesService;
            _customerService = customerService;
            _parameterValueService = parameterValueService;
            _systemParameterService = systemParameterService;

        }

        private string SystemFeatureID = "313";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new MovimentEmployeeModel();
            var status = _parameterValueService.GetAllByParameterID("313301");
            var movimentEmployeeTypes = _parameterValueService.GetAllByParameterID("313300");
            var employees = _userService.GetAll(0);

            model.SearchLoadEmployees = employees.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadMovimentEmployeeTypes = movimentEmployeeTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(MovimentEmployeeModel model)
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
                    WarningNotification("Você não tem permissão para adicionar um registro em Movimentação de Funcionário!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var msg = ValidateApropriate(model);

                    if (msg == "")
                    {
                        var command = MaintenanceMovimentEmployeeCommand(model);

                        _movimentEmployeeService.Add(command);

                        SuccessNotification(string.Format("Registro realizado com sucesso!"));

                        return RedirectToAction("Index", "MovimentEmployee");
                    }
                    else
                    {
                        WarningNotification(msg);

                        return RedirectToAction("Index", "MovimentEmployee");

                    }
                }
             
                ErrorNotification(string.Format("Não foi possível realizar o registro!"));

                return RedirectToAction("Index", "MovimentEmployee");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar o registro!"));

                return RedirectToAction("Index", "MovimentEmployee");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, MovimentEmployeeModel model)
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
                var movimentEmployees = _movimentEmployeeService.GetAll(new FilterMovimentEmployeeCommand
                {
                    EmployeeID = model.SearchEmployeeID,
                    StatusID = model.SearchStatusID,
                    MovimentEmployeeTypeID = model.SearchMovimentEmployeeTypeID,
                    StartDate = model.SearchStartDate,
                    EndDate = model.SearchEndDate
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
            var model = new MovimentEmployeeModel();

            var status = _parameterValueService.GetAllByParameterID("313301");
            var movimentEmployeeTypes = _parameterValueService.GetAllByParameterID("313300");
            var employees = _userService.GetAll(0);

            model.LoadEmployees = employees.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadMovimentEmployeeTypes = movimentEmployeeTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.StatusID = "313301300";
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); ;

            return PartialView("Maintenance", model);
        }

        public ActionResult LoadActiveDesactiveCustomer()
        {
            return PartialView("ActiveDesactiveCustomer");
        }

        private MaintenanceMovimentEmployeeCommand MaintenanceMovimentEmployeeCommand(MovimentEmployeeModel model)
        {
            MaintenanceMovimentEmployeeCommand command = new MaintenanceMovimentEmployeeCommand();

            command.MovimentEmployeeID = model.MovimentEmployeeID;
            command.EmployeeID = model.EmployeeID;
            command.StartDate = model.StartDate;
            command.EndDate = model.EndDate;
            command.StatusID = model.StatusID;
            command.MovimentEmployeeTypeID = model.MovimentEmployeeTypeID;
            command.ApprovedDate = model.ApprovedDate;
            command.ApprovedByID = model.ApprovedByID;
            command.Description = model.Description;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int movimentEmployeeID, string ActionName)
        {
            var model = new MovimentEmployeeModel();

            Result<MovimentEmployee> MovimentEmployee = _movimentEmployeeService.GetByID(movimentEmployeeID);

            if (MovimentEmployee.IsSuccess)
            {
                model = MovimentEmployee.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var status = _parameterValueService.GetAllByParameterID("313301");
                    var movimentEmployeeTypes = _parameterValueService.GetAllByParameterID("313300");

                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadMovimentEmployeeTypes = movimentEmployeeTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    var employees = _userService.GetAll(Convert.ToInt32(model.EmployeeID));
                    model.LoadEmployees = employees.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "MovimentEmployee");
        }

        public ActionResult Delete(int movimentEmployeeID)
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
                    WarningNotification("Você não tem permissão para excluir um registro em Movimentação de Funcionário!");

                    return RedirectToAction("Index");
                }
                if (movimentEmployeeID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluido! "));
                    return Redirect("Index");
                }
                var model = new MovimentEmployeeModel();

                Result<MovimentEmployee> movimentEmployee = _movimentEmployeeService.GetByID(movimentEmployeeID);

                if (movimentEmployee.IsSuccess)
                {
                    model = movimentEmployee.Value.ToModel();

                    _movimentEmployeeService.Delete(model.MovimentEmployeeID);

                    SuccessNotification(string.Format("Registro excluido com sucesso! "));

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

        public ActionResult StatusChange(int movimentEmployeeID)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterProfileCommand
                {
                    AllowChangeStatus = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para alterar o status de um registro em Movimentação de Funcionário!");

                    return RedirectToAction("Index");
                }


                if (Convert.ToString(Session["isAdmin"]) == "True")
                {
                    Result<MovimentEmployee> movimentEmployee = _movimentEmployeeService.GetByID(movimentEmployeeID);

                    MovimentEmployeeModel model = movimentEmployee.Value.ToModel();

                    var command = MaintenanceMovimentEmployeeCommand(model);

                    if (command.StatusID == "313301300")
                    {
                        command.StatusID = "313301301";
                        command.ApprovedByID = Convert.ToString(Session["userID"]);
                        command.ApprovedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); ;

                        _movimentEmployeeService.Update(command);

                        SuccessNotification(string.Format("Status alterado com sucesso! "));

                        return View();

                    }
                    else
                    {
                        command.StatusID = "313301300";
                        command.ApprovedByID = Convert.ToString(Session["userID"]);
                        command.ApprovedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); ;

                        _movimentEmployeeService.Update(command);

                        SuccessNotification(string.Format("Status alterado com sucesso! "));

                        return View();
                    }
                }

                WarningNotification("Você não tem permissão para aprovação! ");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);

                throw;
            }
        }

        private string ValidateApropriate(MovimentEmployeeModel movimentEmployee)
        {
            //register between startWork en endWork to registerDate
            if (!string.IsNullOrEmpty(_movimentEmployeeService.GetApropriateByRangeTime(movimentEmployee.MovimentEmployeeID, Convert.ToInt32(movimentEmployee.EmployeeID), movimentEmployee.StartDate, movimentEmployee.EndDate)))
            {
                return string.Format("Existem um ou mais registros para este(s) dia(s). Por favor verifique seus lançamentos! ");
            }
            return "";
        }

        [HttpPost]
        public ActionResult Update(MovimentEmployeeModel model)
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
                    WarningNotification("Você não tem permissão para atualizar um registro em Movimentação de Funcionário!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var msg = ValidateApropriate(model);

                    if (msg == "")
                    {
                        var command = MaintenanceMovimentEmployeeCommand(model);

                        _movimentEmployeeService.Update(command);

                        SuccessNotification(string.Format("Registro atualizado com sucesso!"));

                        return RedirectToAction("Index", "MovimentEmployee");
                    }
                    else
                    {
                        //WarningNotification(msg);
                        WarningNotification(string.Format("Não foi possível salvar a atualização. Existem um ou mais registros para este(s) dia(s)!"));


                        return RedirectToAction("Index", "MovimentEmployee");

                    }
                }

                ErrorNotification(string.Format("Não foi possível salvar a atualização!"));

                return RedirectToAction("Index", "MovimentEmployee");
            }

            catch (Exception ex)
            {
                ErrorNotification(ex.Message);

                throw;
            }
        }

                          
    }
}