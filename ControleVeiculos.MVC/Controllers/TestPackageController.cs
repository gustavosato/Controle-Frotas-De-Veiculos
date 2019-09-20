using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.TestPackages;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.TestPackages;
using ControleVeiculos.Domain.Entities.TestPackages;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;


namespace ControleVeiculos.MVC.Controllers
{
    public class TestPackageController : BaseController
    {
        private readonly ITestPackageService _testPackageService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IDemandService _demandService;


        public TestPackageController(ITestPackageService testPackageService,
                                    ICustomerService customerService,
                                    IUserService userService,
                                    IDemandService demandService,
                                    IParameterValueService parameterValueService)
        {
            _userService = userService;
            _testPackageService = testPackageService;
            _customerService = customerService;
            _parameterValueService = parameterValueService;
            _demandService = demandService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new TestPackageModel();
            var tecnology = _parameterValueService.GetAllByParameterID("38");
            var browser = _parameterValueService.GetAllByParameterID("39");
            var device = _parameterValueService.GetAllByParameterID("40");
            var platformName = _parameterValueService.GetAllByParameterID("41");
            var methodology = _parameterValueService.GetAllByParameterID("42");
            var status = _parameterValueService.GetAllByParameterID("223202");
            var demand = _demandService.GetAll(Convert.ToString(Session["customerID"]), "0", Session["userID"].ToString());


            model.SearchLoadTecnology = tecnology.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadBrowser = browser.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadDevice = device.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadPlatformName = platformName.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadMethodology = methodology.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadDemand = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();


            return View(model);
        }

        [HttpPost]
        public ActionResult Add(TestPackageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceTestPackageCommand(model);

                    _testPackageService.Add(command);

                    SuccessNotification(string.Format("Pacote adicionado com sucesso! Pacote: {0}.", model.PackageName));

                    return RedirectToAction("Index", "TestPackage");

                }

                ErrorNotification(string.Format("Não foi possível incluir novo pacote: {0}", model.PackageName));

                return RedirectToAction("Index", "TestPackage");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível incluir novo pacote: {0}", model.PackageName));

                return RedirectToAction("Index", "TestPackage");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, TestPackageModel model)
        {
            var testPackages = _testPackageService.GetAll(new FilterTestPackageCommand
            {
                TecnologyID = model.SearchTecnologyID,
                BrowserID = model.SearchBrowserID,
                DeviceID = model.SearchDeviceID,
                PlatformNameID = model.SearchPlatformNameID,
                MethodologyID = model.SearchMethodologyID,
                StatusID = model.SearchStatusID,
                DemandID = model.SearchDemandID,
                
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = testPackages.Select(x =>
                {
                    var testPackageModel = x.ToModel();

                    return testPackageModel;
                }),
                Total = testPackages.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new TestPackageModel();
            var tecnology = _parameterValueService.GetAllByParameterID("38");
            var browser = _parameterValueService.GetAllByParameterID("39");
            var device = _parameterValueService.GetAllByParameterID("40");
            var platformName = _parameterValueService.GetAllByParameterID("41");
            var methodology = _parameterValueService.GetAllByParameterID("42");
            var status = _parameterValueService.GetAllByParameterID("223202");
            var demand = _demandService.GetAll(Convert.ToString(Session["customerID"]), "0", Session["userID"].ToString());



            model.LoadTecnology = tecnology.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadBrowser = browser.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadDevice = device.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadPlatformName = platformName.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadMethodology = methodology.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadDemand = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();
            

            model.ExecutionSpeedy = "200";
            model.ResetApp = true;
            model.HighLight = true;
            model.HighLightOut = true;
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); ;

            return PartialView("Maintenance", model);
        }

        public ActionResult LoadActiveDesactiveCustomer()
        {
            return PartialView("ActiveDesactiveCustomer");
        }

        private MaintenanceTestPackageCommand MaintenanceTestPackageCommand(TestPackageModel model)
        {
            MaintenanceTestPackageCommand command = new MaintenanceTestPackageCommand();

            command.TestPackageID = model.TestPackageID;
            command.PackageName = model.PackageName;
            command.Description = model.Description;
            command.DemandID = model.DemandID;
            command.StatusID = model.StatusID;
            command.Release = model.Release;
            command.Cycle = model.Cycle;
            command.EmailsToSendReport = model.EmailsToSendReport;
            command.TecnologyID = model.TecnologyID;
            command.BrowserID = model.BrowserID;
            command.ExecutionSpeedy = model.ExecutionSpeedy;
            command.ResetApp = model.ResetApp;
            command.HighLight = model.HighLight;
            command.HighLightOut = model.HighLightOut;
            command.DeviceID = model.DeviceID;
            command.PlatformNameID = model.PlatformNameID;
            command.SendEmail = model.SendEmail;
            command.GenerateLog = model.GenerateLog;
            command.LogHtml = model.LogHtml;
            command.MethodologyID = model.MethodologyID;
            command.SolutionPath = model.SolutionPath;
            command.LeantestVariable = model.LeantestVariable;
            command.SaveEvidenceToExternalPath = model.SaveEvidenceToExternalPath;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
            return command;
        }

        public ActionResult GetByID(int testPackageID, string ActionName)
        {
            var model = new TestPackageModel();

            Result<TestPackage> TestPackage = _testPackageService.GetByID(testPackageID);

            if (TestPackage.IsSuccess)
            {
                model = TestPackage.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var tecnology = _parameterValueService.GetAllByParameterID("38");
                    var browser = _parameterValueService.GetAllByParameterID("39");
                    var device = _parameterValueService.GetAllByParameterID("40");
                    var platformName = _parameterValueService.GetAllByParameterID("41");
                    var methodology = _parameterValueService.GetAllByParameterID("42");
                    var status = _parameterValueService.GetAllByParameterID("223202");
                    var demand = _demandService.GetAll(Convert.ToString(Session["customerID"]), model.DemandID, Session["userID"].ToString());


                    model.LoadTecnology = tecnology.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadBrowser = browser.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadDevice = device.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadPlatformName = platformName.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadMethodology = methodology.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadDemand = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);
                               
                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "TestPackage");
        }

        public ActionResult Delete(int testPackageID)
        {
            try
            {
                if (testPackageID == 0)
                {
                    ErrorNotification(string.Format("O pacote selecionado não pode ser excluido! Aplicação ID : {0} ", testPackageID));
                    return Redirect("Index");
                }
                var model = new TestPackageModel();

                Result<TestPackage> testPackage = _testPackageService.GetByID(testPackageID);

                if (testPackage.IsSuccess)
                {
                    model = testPackage.Value.ToModel();

                    _testPackageService.Delete(model.TestPackageID);

                    SuccessNotification(string.Format("Pacote excluido com sucesso! Pacote: {0}", model.PackageName));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir o pacote, tente novamente.");

                return RedirectToAction("Index");
            }
        }

       
        [HttpPost]
        public ActionResult Update(TestPackageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceTestPackageCommand(model);

                    _testPackageService.Update(command);

                    SuccessNotification(string.Format("Pacote atualizado com sucesso! Pacote: {0}", model.PackageName));

                    return RedirectToAction("Index");
                }

                ErrorNotification("Não foi possível salvar a atualização!");

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