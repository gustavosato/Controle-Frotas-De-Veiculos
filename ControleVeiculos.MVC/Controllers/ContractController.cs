    using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Contracts;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Contracts;
using Lean.Test.Cloud.Domain.Entities.Contracts;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;


namespace Lean.Test.Cloud.MVC.Controllers
{
    public class ContractController : BaseController
    {
        private readonly IContractService _contractService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IProfilesService _profilesService;
        private readonly IPipelineService _pipelineService;

        public ContractController(IContractService contractService,
                                  ICustomerService customerService,
                                  IProfilesService profilesService,
                                  IUserService userService,
                                  IParameterValueService parameterValueService,
                                  IPipelineService pipelineService)
        {
            _userService = userService;
            _contractService = contractService;
            _customerService = customerService;
            _parameterValueService = parameterValueService;
            _profilesService = profilesService;
            _pipelineService = pipelineService;
        }

        private string SystemFeatureID = "310";

        public JsonResult GetOportunitys(string contractingCustomerID)
        {
            var oportunity = _pipelineService.GetAllCodeByCustomerID(contractingCustomerID);

            return Json(oportunity.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList());
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new ContractModel();
            var contractTypes = _parameterValueService.GetAllByParameterID("310300");

            var contractorCustomers = _customerService.GetAllGroupCompanies("0");
            var contractingCustomers = _customerService.GetAllNoGroupCompanies("0");

            var periodValiditys = _parameterValueService.GetAllByParameterID("310301");
            var extencions = _parameterValueService.GetAllByParameterID("310302");
            var extencionPeriods = _parameterValueService.GetAllByParameterID("310303");
            var resetModalitys = _parameterValueService.GetAllByParameterID("310304");
            var oportunitys = _pipelineService.GetAllCodeByCustomerID("0");

            model.SearchLoadContractType = contractTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadContractorCustomer = contractorCustomers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.SearchLoadContractingCustomer = contractingCustomers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.SearchLoadPeriodValidity = periodValiditys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadExtencion = extencions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadExtencionPeriod = extencionPeriods.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadResetModality = resetModalitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadOportunity = oportunitys.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();


            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ContractModel model)
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
                    WarningNotification("Você não tem permissão para adicionar um contrato!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceContractCommand(model);

                    _contractService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso!"));

                    return RedirectToAction("Index", "Contract");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Contract");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro"));

                return RedirectToAction("Index", "Contract");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ContractModel model)
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
                WarningNotification("Você não tem permissão para visualizar os contratos!");

                return Json(gridModel);
            }
            else
            {
                var contracts = _contractService.GetAll(new FilterContractCommand
                {
                    OportunityID = model.SearchOportunityID,
                    ContractTypeID = model.SearchContractTypeID,
                    ContractorCustomerID = model.SearchContractorCustomerID,
                    ContractingCustomerID = model.SearchContractingCustomerID,
                    StartDate = model.SearchStartDate,
                    EndDate = model.SearchEndDate,
                    PeriodValidityID = model.SearchPeriodValidityID,
                    ExtencionID = model.SearchExtencionID,
                    ExtencionPeriodID = model.SearchExtencionPeriodID,
                    ResetModalityID = model.SearchResetModalityID

                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = contracts.Select(x =>
                    {
                        var contractModel = x.ToModel();

                        return contractModel;
                    }),
                    Total = contracts.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new ContractModel();
            var contractTypes = _parameterValueService.GetAllByParameterID("310300");
            var contractorCustomers = _customerService.GetAllGroupCompanies("0");
            var contractingCustomers = _customerService.GetAllNoGroupCompanies("0");
            var periodValiditys = _parameterValueService.GetAllByParameterID("310301");
            var extencions = _parameterValueService.GetAllByParameterID("310302");
            var extencionPeriods = _parameterValueService.GetAllByParameterID("310303");
            var resetModalitys = _parameterValueService.GetAllByParameterID("310304");

            model.LoadContractType = contractTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadContractorCustomer = contractorCustomers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.LoadContractingCustomer = contractingCustomers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.LoadPeriodValidity = periodValiditys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadExtencion = extencions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadExtencionPeriod = extencionPeriods.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadResetModality = resetModalitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        private MaintenanceContractCommand MaintenanceContractCommand(ContractModel model)
        {
            MaintenanceContractCommand command = new MaintenanceContractCommand();

            command.ContractID = model.ContractID;
            command.OportunityID = model.OportunityID;
            command.ContractTypeID = model.ContractTypeID;
            command.ContractorCustomerID = model.ContractorCustomerID;
            command.ContractingCustomerID = model.ContractingCustomerID;
            command.ObjectContract = model.ObjectContract;
            command.StartDate = model.StartDate;
            command.EndDate = model.EndDate;
            command.PeriodValidityID = model.PeriodValidityID;
            command.ExtencionID = model.ExtencionID;
            command.ExtencionPeriodID = model.ExtencionPeriodID;
            command.ResetModalityID = model.ResetModalityID;
            command.BillingCondition = model.BillingCondition;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int contractID, string ActionName)
        {
            var model = new ContractModel();

            Result<Contract> Contract = _contractService.GetByID(contractID);

            if (Contract.IsSuccess)
            {
                model = Contract.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var contractTypes = _parameterValueService.GetAllByParameterID("310300");
                    var contractorCustomers = _customerService.GetAllGroupCompanies(model.ContractorCustomerID);
                    var contractingCustomers = _customerService.GetAllNoGroupCompanies(model.ContractingCustomerID);
                    var periodValiditys = _parameterValueService.GetAllByParameterID("310301");
                    var extencions = _parameterValueService.GetAllByParameterID("310302");
                    var extencionPeriods = _parameterValueService.GetAllByParameterID("310303");
                    var resetModalitys = _parameterValueService.GetAllByParameterID("310304");
                    var oportunitys = _pipelineService.GetAllCodeByCustomerID("0");


                    model.LoadContractType = contractTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadContractorCustomer = contractorCustomers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
                    model.LoadContractingCustomer = contractingCustomers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
                    model.LoadPeriodValidity = periodValiditys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadExtencion = extencions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadExtencionPeriod = extencionPeriods.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadResetModality = resetModalitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadOportunity = oportunitys.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();

                    model.ObjectContract = Server.HtmlDecode(model.ObjectContract);


                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "Contract");
        }

        public ActionResult Delete(int contractID)
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
                    WarningNotification("Você não tem permissão para excluir um contrato!");

                    return RedirectToAction("Index");
                }

                if (contractID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new ContractModel();

                Result<Contract> contract = _contractService.GetByID(contractID);

                if (contract.IsSuccess)
                {
                    model = contract.Value.ToModel();

                    _contractService.Delete(model.ContractID);

                    SuccessNotification(string.Format("Registro excluído com sucesso!"));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("A aplicação contêm anexo(s) ou aditivo(s) associado(s), exclua primeiro o(s) registro(s) associado(s) a este contrato.");

                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        public ActionResult Update(ContractModel model)
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
                    WarningNotification("Você não tem permissão para atualizar um contrato!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceContractCommand(model);

                    _contractService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso! "));

                    return RedirectToAction("Index");
                }

                ErrorNotification("Não foi possível salvar atualização!");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);

                throw;
            }
        }
    }
}