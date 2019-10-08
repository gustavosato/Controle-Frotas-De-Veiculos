using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.TestCases;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.TestCases;
using ControleVeiculos.Domain.Entities.TestCases;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;


namespace ControleVeiculos.MVC.Controllers
{
    public class TestCaseController : BaseController
    {
        private readonly ITestCaseService _testCaseService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IDemandService _demandService;
        private readonly ISystemFeatureService _systemFeatureService;


        public TestCaseController(ITestCaseService testCaseService,
                                    ICustomerService customerService,
                                    IUserService userService,
                                    IDemandService demandService,
                                    IParameterValueService parameterValueService,
                                    ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _testCaseService = testCaseService;
            _customerService = customerService;
            _parameterValueService = parameterValueService;
            _demandService = demandService;
            _systemFeatureService = systemFeatureService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new TestCaseModel();
            var status = _parameterValueService.GetAllByParameterID("223202");
            var feature = _systemFeatureService.GetAll();
            var testScenario = _parameterValueService.GetAllByParameterID("40");
            var flowTest = _parameterValueService.GetAllByParameterID("210200");
            var testType = _parameterValueService.GetAllByParameterID("223200");
            
            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
            model.SearchLoadTestScenario = testScenario.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadFlowTest = flowTest.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadTestType = testType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(TestCaseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceTestCaseCommand(model);

                    _testCaseService.Add(command);

                    SuccessNotification(string.Format("Teste adicionado com sucesso! Teste: {0}.", model.TestCase));

                    return RedirectToAction("Index", "TestCase");
                }

                ErrorNotification(string.Format("Não foi possível incluir novo teste devido ao preenchimento dos campos obrigatorios"));

                return RedirectToAction("Index", "TestCase");
            }

            catch (Exception ex)
            {
                ErrorNotification(string.Format("Não foi possível incluir novo teste: {0}, " + ex.Message.ToString(), model.TestCase));

                return RedirectToAction("Index", "TestCase");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, TestCaseModel model)
        {
            var testCases = _testCaseService.GetAll(new FilterManutencaoCommand
            {
                StatusID = model.SearchStatusID,
                FeatureID = model.SearchFeatureID,
                TestScenarioID = model.SearchTestScenarioID,
                FlowTestID = model.SearchFlowTestID,
                TestTypeID = model.SearchTestTypeID,
                TestCase = model.SearchTestCase,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = testCases.Select(x =>
                {
                    var testCaseModel = x.ToModel();

                    return testCaseModel;
                }),
                Total = testCases.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new TestCaseModel();
            var status = _parameterValueService.GetAllByParameterID("223202");
            var feature = _systemFeatureService.GetAll();
            var testScenario = _parameterValueService.GetAllByParameterID("40");
            var flowTest = _parameterValueService.GetAllByParameterID("210200");
            var testType = _parameterValueService.GetAllByParameterID("223200");

            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
            model.LoadTestScenario = testScenario.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadFlowTest = flowTest.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadTestType = testType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }
        
        private MaintenanceManutencaoCommand MaintenanceTestCaseCommand(TestCaseModel model)
        {
            MaintenanceManutencaoCommand command = new MaintenanceManutencaoCommand();

            command.TestCaseID = model.TestCaseID;
            command.StatusID = model.StatusID;
            command.TestCase = model.TestCase;
            command.Description = model.Description;
            command.Precondition = model.Precondition;
            command.ExpectedResult = model.ExpectedResult;
            command.FeatureID = model.FeatureID;
            command.TestScenarioID = model.TestScenarioID;
            command.ExecutionOrder = model.ExecutionOrder;
            command.FlowTestID = model.FlowTestID;
            command.StartExecution = model.StartExecution;
            command.EndExecution = model.EndExecution;
            command.TimeExecution = model.TimeExecution;
            command.Release = model.Release;
            command.Cycle = model.Cycle;
            command.TestTypeID = model.TestTypeID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
            return command;
        }

        public ActionResult GetByID(int testCaseID, string ActionName)
        {
            var model = new TestCaseModel();

            Result<TestCase> testCase = _testCaseService.GetByID(testCaseID);

            if (testCase.IsSuccess)
            {
                model = testCase.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var status = _parameterValueService.GetAllByParameterID("223202");
                    var feature = _systemFeatureService.GetAll();
                    var testScenario = _parameterValueService.GetAllByParameterID("40");
                    var flowTest = _parameterValueService.GetAllByParameterID("210200");
                    var testType = _parameterValueService.GetAllByParameterID("223200");

                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
                    model.LoadTestScenario = testScenario.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadFlowTest = flowTest.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadTestType = testType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);
                    model.StartExecution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    model.EndExecution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "TestCase");
        }

        public ActionResult Delete(int testCaseID)
        {
            try
            {
                if (testCaseID == 0)
                {
                    ErrorNotification(string.Format("O teste selecionado não pode ser excluido! TestID : {0} ", testCaseID));
                    return Redirect("Index");
                }
                var model = new TestCaseModel();

                Result<TestCase> testCase = _testCaseService.GetByID(testCaseID);

                if (testCase.IsSuccess)
                {
                    model = testCase.Value.ToModel();

                    _testCaseService.Delete(model.TestCaseID);

                    SuccessNotification(string.Format("Teste excluido com sucesso! Teste: {0}", model.TestCase));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir o teste, tente novamente.");

                return RedirectToAction("Index");
            }
        }

       
        [HttpPost]
        public ActionResult Update(TestCaseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceTestCaseCommand(model);

                    _testCaseService.Update(command);

                    SuccessNotification(string.Format("Teste atualizado com sucesso! Teste: {0}", model.TestCase));

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