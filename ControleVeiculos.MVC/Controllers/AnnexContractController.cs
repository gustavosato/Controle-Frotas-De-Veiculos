using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.AnnexContracts;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.AnnexContracts;
using Lean.Test.Cloud.Domain.Entities.AnnexContracts;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;


namespace Lean.Test.Cloud.MVC.Controllers
{
    public class AnnexContractController : BaseController
    {
        private readonly IAnnexContractService _annexContractService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IPipelineService _pipelineService;
        private readonly IProfilesService _profilesService;
        private readonly IContractService _contractService;

        public AnnexContractController(IAnnexContractService annexContractService,
                                       ICustomerService customerService,
                                       IPipelineService pipelineService,
                                       IProfilesService profilesService,
                                       IUserService userService,
                                       IParameterValueService parameterValueService,
                                       IContractService contractService)
        {
            _userService = userService;
            _annexContractService = annexContractService;
            _pipelineService = pipelineService;
            _customerService = customerService;
            _profilesService = profilesService;
            _parameterValueService = parameterValueService;
            _contractService = contractService;
        }

        private string SystemFeatureID = "324";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new AnnexContractModel();
            var extencionPeriods = _parameterValueService.GetAllByParameterID("324300");
            var contracts = _contractService.GetAll(0);

            model.SearchLoadExtencionPeriod = extencionPeriods.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadContract = contracts.Select(x => new SelectListItem() { Text = x.oportunityID.ToString(), Value = x.contractID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AnnexContractModel model, string sourceController)
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
                    WarningNotification("Você não tem permissão para adicionar um anexo de contrato!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    if (model.ContractID == "0")
                    {
                        WarningNotification(string.Format("Registro não pode ser realizado!", model.Summary));

                        if (sourceController == "Contract")
                        {
                            return RedirectToAction("Index", "Contract");
                        }
                        return View();
                    }

                    var command = MaintenanceAnnexContractCommand(model);

                    _annexContractService.Add(command);

                    SuccessNotification(string.Format("Anexo incluído com sucesso! "));

                    if (sourceController == "Contract")
                    {
                        return RedirectToAction("Index", "Contract");
                    }
                    return RedirectToAction("Index", "AnnexContract");
                }

                ErrorNotification(string.Format("Não foi possível incluir o anexo, campo obrigatório não preenchido!"));

                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }
                return RedirectToAction("Index", "AnnexContract");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Erro ao incluir o anexo. "));

                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }

                return RedirectToAction("Index", "AnnexContract");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, AnnexContractModel model)
        {
            var annexContracts = _annexContractService.GetAll(new FilterAnnexContractCommand
            {
                ExtencionPeriodID = model.SearchExtencionPeriodID,
                StartDate = model.SearchStartDate,
                ContractID = model.SearchContractID,
                EndDate = model.SearchEndDate, 
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = annexContracts.Select(x =>
                {
                    var annexContractModel = x.ToModel();

                    return annexContractModel;
                }),

                Total = annexContracts.TotalCount
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
                WarningNotification("Você não tem permissão para visualizar os registros de anexos de contrato!");

                return Json(gridModel);
            }
            else
            {
                var annexContracts = _annexContractService.GetAll(new FilterAnnexContractCommand
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
            var model = new AnnexContractModel();

            var extencionPeriods = _parameterValueService.GetAllByParameterID("324300");
            var contracts = _contractService.GetAll(0);
            var oportunity = _pipelineService.GetAllCodeByCustomerID(contractID);

            model.LoadExtencionPeriod = extencionPeriods.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadContract = contracts.Select(x => new SelectListItem() { Text = x.oportunityID.ToString(), Value = x.contractID.ToString() }).ToList();
            model.LoadOportunityID = oportunity.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();

            if (contractID == null) contractID = "0";
            model.ContractID = contractID;
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); ;

            return PartialView("Maintenance", model);
        }

        private MaintenanceAnnexContractCommand MaintenanceAnnexContractCommand(AnnexContractModel model)
        {
            MaintenanceAnnexContractCommand command = new MaintenanceAnnexContractCommand();

            command.AnnexID = model.AnnexID;
            command.ContractID = model.ContractID;
            command.OportunityID = model.OportunityID;
            command.Summary = model.Summary;
            command.AnnexObject = model.AnnexObject;
            command.StartDate = model.StartDate;
            command.EndDate = model.EndDate;
            command.ExtencionPeriodID = model.ExtencionPeriodID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int annexID, string ActionName)
        {
            var model = new AnnexContractModel();

            Result<AnnexContract> AnnexContract = _annexContractService.GetByID(annexID);

            if (AnnexContract.IsSuccess)
            {
                model = AnnexContract.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var extencionPeriods = _parameterValueService.GetAllByParameterID("324300");
                    var oportunity = _pipelineService.GetAllCodeByCustomerID(model.ContractID);

                    model.LoadExtencionPeriod = extencionPeriods.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadOportunityID = oportunity.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "AnnexContract");
        }

        public ActionResult Delete(int annexID, string sourceController)
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
                    WarningNotification("Você não tem permissão para excluir um anexo de contrato!");

                    return RedirectToAction("Index");
                }
                if (annexID == 0)
                {
                    ErrorNotification(string.Format("O anexo não pode ser excluído! "));

                    if (sourceController == "Contract")
                    {
                        return RedirectToAction("Index", "Contract");
                    }
                    return Redirect("Index");
                }
                var model = new AnnexContractModel();

                Result<AnnexContract> annexContract = _annexContractService.GetByID(annexID);

                if (annexContract.IsSuccess)
                {
                    model = annexContract.Value.ToModel();

                    _annexContractService.Delete(model.AnnexID);

                    SuccessNotification(string.Format("Anexo excluído com sucesso! "));

                    if (sourceController == "Contract")
                    {
                        return RedirectToAction("Index", "Contract");
                    }
                    return RedirectToAction("Index");
                }

                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                WarningNotification(string.Format("Erro ao excluir anexo."));

                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }
                return RedirectToAction("Index");
            }
        }
       
        [HttpPost]
        public ActionResult Update(AnnexContractModel model, string sourceController)
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
                    WarningNotification("Você não tem permissão para atualizar um anexo de contrato!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceAnnexContractCommand(model);

                    _annexContractService.Update(command);

                    SuccessNotification(string.Format("Anexo atualizado com sucesso! "));

                    if (sourceController == "Contract")
                    {
                        return RedirectToAction("Index", "Contract");
                    }
                    return RedirectToAction("Index");

                }

                ErrorNotification("Não foi possível salvar a atualização!");

                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Erro ao atualizar o anexo. "));

                if (sourceController == "Contract")
                {
                    return RedirectToAction("Index", "Contract");
                }
                return RedirectToAction("Index");
            }
        }
    }
}