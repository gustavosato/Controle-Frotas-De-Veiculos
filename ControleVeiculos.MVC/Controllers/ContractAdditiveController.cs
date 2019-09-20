using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.ContractAdditives;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.ContractAdditives;
using ControleVeiculos.Domain.Entities.ContractAdditives;
using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Command.Profiles;
//using ControleVeiculos.MVC.Infrastructure.Mvc;

namespace ControleVeiculos.MVC.Controllers
{
    public class ContractAdditiveController : BaseController
    {
        private readonly IContractAdditiveService _contractAdditiveService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IProfilesService _profilesService;
        private readonly IPipelineService _pipelineService;

        public ContractAdditiveController(IContractAdditiveService contractAdditiveService,
                                    ICustomerService customerService,
                                    IUserService userService,
                                    IPipelineService pipelineService,
                                    IProfilesService profilesService,
                                    IParameterValueService parameterValueService)
        {
            _userService = userService;
            _contractAdditiveService = contractAdditiveService;
            _customerService = customerService;
            _pipelineService = pipelineService;
            _profilesService = profilesService;
            _parameterValueService = parameterValueService;
        }

        private string SystemFeatureID = "311";

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ContractAdditiveModel();
            var periodValiditys = _parameterValueService.GetAllByParameterID("311300");
            var extencions = _parameterValueService.GetAllByParameterID("311301");
            var extencionPeriods = _parameterValueService.GetAllByParameterID("311302");
            var resetModalitys = _parameterValueService.GetAllByParameterID("311303");

            model.SearchLoadPeriodValidity = periodValiditys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadExtencion = extencions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadExtencionPeriod = extencionPeriods.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadResetModality = resetModalitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ContractAdditiveModel model)
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
                    WarningNotification("Você não tem permissão para adicionar um aditivo de contrato!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceContractAdditiveCommand(model);

                    _contractAdditiveService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index");
                }
                ErrorNotification(string.Format("Não foi possível incluir o aditivo: {0}", model.ContractID));

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível incluir o aditivo: {0}", model.ContractID));

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ContractAdditiveModel model)
        {
            var contractAdditives = _contractAdditiveService.GetAll(new FilterContractAdditiveCommand
            {
                PeriodValidityID = model.SearchPeriodValidity,
                ExtencionID = model.SearchExtencion,
                StartDate = model.SearchStartDate,
                EndDate = model.SearchEndDate,
                ExtencionPeriodID = model.SearchExtencionPeriodID,
                ResetModalityID = model.SearchResetModalityID,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = contractAdditives.Select(x =>
                {
                    var contractAdditiveModel = x.ToModel();

                    return contractAdditiveModel;
                }),

                Total = contractAdditives.TotalCount
            };
            return Json(gridModel);
        }

        public ActionResult GetAllByContractID(DataSourceRequest request, string contractID)
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
                WarningNotification("Você não tem permissão para visualizar os registros de aditivos de contratos!");

                return Json(gridModel);
            }
            else
            {
                var annexContracts = _contractAdditiveService.GetAll(new FilterContractAdditiveCommand
                {
                    ContractID = contractID,
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = annexContracts.Select(x =>
                    {
                        var annexContractsModel = x.ToModel();

                        return annexContractsModel;
                    }),

                    Total = annexContracts.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New(string contractID)
        {
            var model = new ContractAdditiveModel();

            var periodValiditys = _parameterValueService.GetAllByParameterID("311300");
            var extencions = _parameterValueService.GetAllByParameterID("311301");
            var extencionPeriods = _parameterValueService.GetAllByParameterID("311302");
            var resetModalitys = _parameterValueService.GetAllByParameterID("311303");
            var oportunity = _pipelineService.GetAllCodeByCustomerID(contractID);

            model.LoadPeriodValidity = periodValiditys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadExtencion = extencions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadExtencionPeriod = extencionPeriods.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadResetModality = resetModalitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadOportunityID = oportunity.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();

            if (contractID == null) contractID = "0";
            model.ContractID = contractID;

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); ;

            return PartialView("Maintenance", model);
        }

        private MaintenanceContractAdditiveCommand MaintenanceContractAdditiveCommand(ContractAdditiveModel model)
        {
            MaintenanceContractAdditiveCommand command = new MaintenanceContractAdditiveCommand();

            command.AdditiveID = model.AdditiveID;
            command.ContractID = model.ContractID;
            command.AdditiveObject = model.AdditiveObject;
            command.StartDate = model.StartDate;
            command.EndDate = model.EndDate;
            command.PeriodValidityID = model.PeriodValidityID;
            command.ExtencionID = model.ExtencionID;
            command.ExtencionPeriodID = model.ExtencionPeriodID;
            command.ResetModalityID = model.ResetModalityID;
            command.BillingCondition = model.BillingCondition;
            command.OportunityID = model.OportunityID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int additiveID, string ActionName)
        {
            var model = new ContractAdditiveModel();

            Result<ContractAdditive> ContractAdditive = _contractAdditiveService.GetByID(additiveID);

            if (ContractAdditive.IsSuccess)
            {
                model = ContractAdditive.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var periodValiditys = _parameterValueService.GetAllByParameterID("311300");
                    var extencions = _parameterValueService.GetAllByParameterID("311301");
                    var extencionPeriods = _parameterValueService.GetAllByParameterID("311302");
                    var resetModalitys = _parameterValueService.GetAllByParameterID("311303");
                    var oportunity = _pipelineService.GetAllCodeByCustomerID(model.ContractID);

                    model.LoadPeriodValidity = periodValiditys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadExtencion = extencions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadExtencionPeriod = extencionPeriods.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadResetModality = resetModalitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadOportunityID = oportunity.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }
            }
            return RedirectToAction("Index", "ContractAdditive");
        }

        public ActionResult Delete(int additiveID, string sourceController)
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
                    WarningNotification("Você não tem permissão para excluir um aditivo de contrato!");

                    return RedirectToAction("Index");
                }

                if (additiveID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));

                    if (sourceController == "Contract")
                    {
                        return RedirectToAction("Index", "Contract");
                    }
                    return Redirect("Index");
                }
                var model = new ContractAdditiveModel();

                Result<ContractAdditive> contractAdditive = _contractAdditiveService.GetByID(additiveID);

                if (contractAdditive.IsSuccess)
                {
                    model = contractAdditive.Value.ToModel();

                    _contractAdditiveService.Delete(model.AdditiveID);

                    SuccessNotification(string.Format("Registro excluído com sucesso!"));

                    if (sourceController == "Contract")
                    {
                        return RedirectToAction("Index", "Contract");
                    }
                    return RedirectToAction("Index");
                }
                WarningNotification(string.Format("Erro ao excluir o registro."));

                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                WarningNotification(string.Format("Erro ao excluir registro."));

                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(ContractAdditiveModel model, string sourceController)
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
                    WarningNotification("Você não tem permissão para atualizar um aditivo de contrato!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceContractAdditiveCommand(model);

                    _contractAdditiveService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso! "));

                    if (sourceController == "Contract")
                    {
                        return RedirectToAction("Index", "Contract");
                    }

                    return RedirectToAction("Index");
                }
                ErrorNotification("Não foi possível salvar atualização!");

                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }
                return RedirectToAction("Index");
            }
        }
    }
}