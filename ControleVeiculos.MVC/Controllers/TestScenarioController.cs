using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.TestScenarios;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.TestScenarios;
using ControleVeiculos.Domain.Entities.TestScenarios;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.TestScenarioFeatures;
using ControleVeiculos.Domain.Command.Features;
using ControleVeiculos.MVC.Models.Features;
using ControleVeiculos.MVC.Models.TestScenarioFeatures;

namespace ControleVeiculos.MVC.Controllers
{
    public class TestScenarioController : BaseController
    {
        private readonly ITestScenarioService _testScenarioService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ITestPackageService _testPackageService;
        private readonly IFeatureService _featureService;
        private readonly ITestScenarioFeatureService _testScenarioFeatureService;


        public TestScenarioController(ITestScenarioService testScenarioService,
                                    ICustomerService customerService,
                                    IUserService userService,
                                    ITestPackageService testPackageService,
                                    IParameterValueService parameterValueService,
                                    IFeatureService featureService,
                                    ITestScenarioFeatureService testScenarioFeatureService)
        {
            _userService = userService;
            _testScenarioService = testScenarioService;
            _customerService = customerService;
            _parameterValueService = parameterValueService;
            _testPackageService = testPackageService;
            _featureService = featureService;
            _testScenarioFeatureService = testScenarioFeatureService;
        }

        public ActionResult FeatureAssociate(int testScenarioID)
        {
            var model = new TestScenarioFeatureModel();
            model.TestScenarioFeatureID = testScenarioID;
            Session["testScenarioID"] = testScenarioID;
            return PartialView("FeatureAssociate");
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new TestScenarioModel();
            var testType = _parameterValueService.GetAllByParameterID("223200");
            var executionType = _parameterValueService.GetAllByParameterID("223201");
            var status = _parameterValueService.GetAllByParameterID("223202");


            model.SearchLoadTestType = testType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadExecutionType = executionType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.StartExecution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(TestScenarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceTestScenarioCommand(model);

                    _testScenarioService.Add(command);

                    SuccessNotification(string.Format("Cenário adicionado com sucesso! Cenário: {0}.", model.TestScenario));

                    return RedirectToAction("Index", "TestScenario");

                }

                ErrorNotification(string.Format("Não foi possível incluir novo cenário: {0}", model.TestScenario));

                return RedirectToAction("Index", "TestScenario");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível incluir novo cenário: {0}", model.TestScenario));

                return RedirectToAction("Index", "TestScenario");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, TestScenarioModel model)
        {
            var testScenarios = _testScenarioService.GetAll(new FilterTestScenarioCommand
            {
                TestTypeID = model.SearchTestTypeID,
                ExecutionTypeID = model.SearchExecutionTypeID,
                StatusID = model.SearchStatusID,
                TestScenario = model.SearchTestScenario

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = testScenarios.Select(x =>
                {
                    var testScenarioModel = x.ToModel();

                    return testScenarioModel;
                }),
                Total = testScenarios.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GetAllAssociateTestScenarioByFeatureID(DataSourceRequest request, TestScenarioFeatureModel model)
        {
            model.TestScenarioFeatureID = Convert.ToInt32(Session["testScenarioID"]);

            var testScenarios = _testScenarioFeatureService.GetAllAssociateTestScenarioByFeatureID(new FilterTestScenarioFeatureCommand
            {
                FeatureName = model.SearchFeatureName,
                TestScenarioID = model.TestScenarioFeatureID,
                CustomerID = Session["customerID"].ToString(),
                ExecutionOrder = model.ExecutionOrder,
                IsLoop = model.IsLoop,
                ToolsTestID = model.ToolsTestID
            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = testScenarios.Select(x =>
                {
                    var testScenariosModel = x.ToModel();

                    return testScenariosModel;
                }),
                Total = testScenarios.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GetAllNoAssociateTestScenarioByFeatureID(DataSourceRequest request, TestScenarioFeatureModel model)
        {
            model.TestScenarioFeatureID = Convert.ToInt32(Session["testScenarioID"]);

            var testScenarios = _testScenarioFeatureService.GetAllNoAssociateTestScenarioByFeatureID(new FilterTestScenarioFeatureCommand
            {
                FeatureName = model.SearchFeatureName,
                TestScenarioID = model.TestScenarioFeatureID,
                CustomerID = Session["customerID"].ToString(),
                ExecutionOrder = model.ExecutionOrder,
                IsLoop = model.IsLoop,
                ToolsTestID = model.ToolsTestID
            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = testScenarios.Select(x =>
                {
                    var testScenarioFeaturesModel = x.ToModel();

                    return testScenarioFeaturesModel;
                }),
                Total = testScenarios.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new TestScenarioModel();
            var testType = _parameterValueService.GetAllByParameterID("223200");
            var executionType = _parameterValueService.GetAllByParameterID("223201");
            var status = _parameterValueService.GetAllByParameterID("223202");


            model.LoadTestType = testType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadExecutionType = executionType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            model.StartExecution = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
            model.EndExecution = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        public ActionResult DisassociateFeature(int testScenarioFeatureID)
        {
            _testScenarioFeatureService.Delete(testScenarioFeatureID);

            return View();
        }

        public ActionResult AssociateFeature(int featureID)
        {
            var command = new MaintenanceTestScenarioFeatureCommand();

            command.TestScenarioID = Convert.ToInt16(Session["testScenarioID"]);
            command.FeatureID = featureID;

            _testScenarioFeatureService.Add(command);

            return View();
        }


        private MaintenanceTestScenarioCommand MaintenanceTestScenarioCommand(TestScenarioModel model)
        {
            MaintenanceTestScenarioCommand command = new MaintenanceTestScenarioCommand();

            command.TestScenarioID = model.TestScenarioID;
            command.TestScenario = model.TestScenario;
            command.Description = model.Description;
            command.StatusID = model.StatusID;
            command.ExecutionOrder = model.ExecutionOrder;
            command.StartExecution = model.StartExecution;
            command.EndExecution = model.EndExecution;
            command.TimeExecution = model.TimeExecution;
            command.TestTypeID = model.TestTypeID;
            command.ExecutionTypeID = model.ExecutionTypeID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            command.TestPackageID = model.TestPackageID;

            return command;
        }

        public ActionResult GetByID(int testScenarioID, string ActionName)
        {
            var model = new TestScenarioModel();

            Result<TestScenario> TestScenario = _testScenarioService.GetByID(testScenarioID);

            if (TestScenario.IsSuccess)
            {
                model = TestScenario.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var testType = _parameterValueService.GetAllByParameterID("223200");
                    var executionType = _parameterValueService.GetAllByParameterID("223201");
                    var status = _parameterValueService.GetAllByParameterID("223202");

                    model.LoadTestType = testType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadExecutionType = executionType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

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

            return RedirectToAction("Index", "TestScenario");
        }

        public ActionResult Delete(int testScenarioID)
        {
            try
            {
                if (testScenarioID == 0)
                {
                    ErrorNotification(string.Format("O cenário selecionado não pode ser excluido!" ));
                    return Redirect("Index");
                }
                var model = new TestScenarioModel();

                Result<TestScenario> testScenario = _testScenarioService.GetByID(testScenarioID);

                if (testScenario.IsSuccess)
                {
                    model = testScenario.Value.ToModel();

                    _testScenarioService.Delete(model.TestScenarioID);

                    SuccessNotification(string.Format("Cenário excluido com sucesso! Cenário: {0}", model.TestScenario));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir o cenário, tente novamente.");

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Update(TestScenarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceTestScenarioCommand(model);

                    _testScenarioService.Update(command);

                    SuccessNotification(string.Format("Cenário atualizado com sucesso! Cenário: {0}", model.TestScenario));

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