using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.TestLogs;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.TestLogs;
using ControleVeiculos.Domain.Entities.TestLogs;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;


namespace ControleVeiculos.MVC.Controllers
{
    public class TestLogController : BaseController
    {
        private readonly ITestLogService _testLogService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IDemandService _demandService;


        public TestLogController(ITestLogService testLogService,
                                    ICustomerService customerService,
                                    IUserService userService,
                                    IDemandService demandService,
                                    IParameterValueService parameterValueService)
        {
            _userService = userService;
            _testLogService = testLogService;
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

            var model = new TestLogModel();
            var status = _parameterValueService.GetAllByParameterID("223202");


            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(TestLogModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceTestLogCommand(model);

                    _testLogService.Add(command);

                    SuccessNotification(string.Format("Log adicionado com sucesso! TestID: {0}.", model.LogID));

                    return RedirectToAction("Index", "TestLog");

                }

                ErrorNotification(string.Format("Não foi possível incluir novo log: {0}", model.LogID));

                return RedirectToAction("Index", "TestLog");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível incluir novo log: {0}", model.LogID));

                return RedirectToAction("Index", "TestLog");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, TestLogModel model)
        {
            var testLogs = _testLogService.GetAll(new FilterTestLogCommand
            {
                StatusID = model.SearchStatusID,
                
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = testLogs.Select(x =>
                {
                    var testLogModel = x.ToModel();

                    return testLogModel;
                }),
                Total = testLogs.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new TestLogModel();
            var status = _parameterValueService.GetAllByParameterID("223202");

            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 

            return PartialView("Maintenance", model);
        }
       
        private MaintenanceTestLogCommand MaintenanceTestLogCommand(TestLogModel model)
        {
            MaintenanceTestLogCommand command = new MaintenanceTestLogCommand();

            command.LogID = model.LogID;
            command.TestID = model.TestID;
            command.StatusID = model.StatusID;
            command.StepName = model.StepName;
            command.ExpectedResult = model.ExpectedResult;
            command.ActualResult = model.ActualResult;
            command.PathEvidence = model.PathEvidence;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
            return command;
        }

        public ActionResult GetByID(int logID, string ActionName)
        {
            var model = new TestLogModel();

            Result<TestLog> TestLog = _testLogService.GetByID(logID);

            if (TestLog.IsSuccess)
            {
                model = TestLog.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var status = _parameterValueService.GetAllByParameterID("223202");
                    
                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    
                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "TestLog");
        }

        public ActionResult Delete(int logID)
        {
            try
            {
                if (logID == 0)
                {
                    ErrorNotification(string.Format("O log selecionado não pode ser excluido! Log: {0} ", logID));
                    return Redirect("Index");
                }
                var model = new TestLogModel();

                Result<TestLog> testLog = _testLogService.GetByID(logID);

                if (testLog.IsSuccess)
                {
                    model = testLog.Value.ToModel();

                    _testLogService.Delete(model.LogID);

                    SuccessNotification(string.Format("Log excluido com sucesso! Log: {0}", model.LogID));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir o log, tente novamente.");

                return RedirectToAction("Index");
            }
        }

       
        [HttpPost]
        public ActionResult Update(TestLogModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceTestLogCommand(model);

                    _testLogService.Update(command);

                    SuccessNotification(string.Format("Log atualizado com sucesso! Log: {0}", model.LogID));

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