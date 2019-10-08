using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.MVC.Models.SystemParameter;
using ControleVeiculos.MVC.Models.Users;
using System.Globalization;
using System.Web;

namespace ControleVeiculos.MVC.Controllers
{
    public class TimeReleaseController : BaseController
    {
        private readonly ITimeReleaseService _timeReleaseService;
        private readonly IExportManagerService _exportManagerService;
        private readonly IEncryptService _encryptService;
        private readonly IDemandService _demandService;
        private readonly IUserService _userService;
        private readonly IParameterValueService _parameterValueService;
        private readonly ICustomerService _customerService;
        private readonly IHistoricalService _historicalService;
        private readonly ISystemParameterService _systemParameterService;
        private readonly IAttachmentService _attachmentService;
        private readonly IProfilesService _profilesService;
        private readonly IDashboardService _dashboardService;
        private readonly IDailyLogService _dailyLogService;

        public TimeReleaseController(ITimeReleaseService timeReleaseService,
                                    IExportManagerService exportManagerService,
                                    IEncryptService encryptService,
                                    IDemandService demandService,
                                    IUserService userService,
                                    IParameterValueService parameterValueService,
                                    ICustomerService customerService,
                                    IHistoricalService historicalService,
                                    IAttachmentService attachmentService,
                                    IProfilesService profilesService,
                                    ISystemParameterService systemParameterService,
                                    IDashboardService dashboardService,
                                    IDailyLogService dailyLogService)
        {
            _timeReleaseService = timeReleaseService;
            _exportManagerService = exportManagerService;
            _encryptService = encryptService;
            _demandService = demandService;
            _userService = userService;
            _parameterValueService = parameterValueService;
            _historicalService = historicalService;
            _customerService = customerService;
            _attachmentService = attachmentService;
            _systemParameterService = systemParameterService;
            _profilesService = profilesService;
            _dashboardService = dashboardService;
            _dailyLogService = dailyLogService;
        }

        private string SystemFeatureID = "203";

        public JsonResult GetDemands(string customerID, bool isActive)
        {
            var demand = _demandService.GetAll(customerID, new FilterDemandCommand { IsActive = isActive });

            return Json(demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList());
        }

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new TimeReleaseModel();

            string userID = Session["userID"].ToString();

            var createdBy = _userService.GetAll(0);

            var customers = _customerService.GetAll(new FilterCustomerCommand { });

            model.SearchLoadCreateds = createdBy.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            model.SearchLoadCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();

            model.SearchIsApproved = false;

            return View(model);
        }

        public ActionResult Report()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new TimeReleaseModel();

            model.RegisterDate = DateTime.Today.ToString("dd/MM/yyyy");

            DateTime date = DateTime.ParseExact(model.RegisterDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var startDate = new DateTime(date.Year, date.Month, 1);

            var endDate = startDate.AddMonths(1).AddDays(-1);

            model.SearchStartDateReport = Convert.ToDateTime(startDate).ToString("dd/MM/yyyy");

            model.SearchEndDateReport = Convert.ToDateTime(endDate).ToString("dd/MM/yyyy");

            return PartialView("Report", model);
        }

        [HttpPost]
        public ActionResult Add(TimeReleaseModel model, string sourceController)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowAdd = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para adicionar uma apropriação de horas!");
                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var msg = ValidateApropriate(model);

                    if (msg == "")
                    {
                        var command = MaintenanceCommand(model);

                        _timeReleaseService.Add(command);

                        //add eventlog
                        var dailyLogModel = new DailyLogModel();

                        dailyLogModel.CreatedByID = command.CreatedByID;
                        dailyLogModel.CreationDate = command.CreationDate;
                        dailyLogModel.DemandID = command.DemandID;
                        dailyLogModel.Description = command.StartWork + " - " + command.EndWork + " " + command.Description;
                        dailyLogModel.IsInternal = true;

                        var dailyLogCommand = MaintenanceDailyLogCommand(dailyLogModel);

                        _dailyLogService.Add(dailyLogCommand);

                        SuccessNotification(string.Format("Registro realizado com sucesso! "));
                        if (sourceController == "Demand")
                        {
                            return RedirectToAction("Index", "Demand");
                        }
                        return RedirectToAction("Index", "TimeRelease");
                    }
                    else
                    {
                        WarningNotification(msg);
                        if (sourceController == "Demand")
                        {
                            return RedirectToAction("Index", "Demand");
                        }
                        return RedirectToAction("Index", "TimeRelease");
                    }
                }

                ErrorNotification(string.Format("Não foi possível realizar registro! "));
                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index", "TimeRelease");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro! "));
                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index", "TimeRelease");
            }
        }

        private MaintenanceDailyLogCommand MaintenanceDailyLogCommand(DailyLogModel model)
        {
            MaintenanceDailyLogCommand command = new MaintenanceDailyLogCommand();

            command.DailyLogID = model.DailyLogID;
            command.DemandID = model.DemandID;
            command.Description = model.Description;
            command.IsInternal = model.IsInternal;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult AbsenceRecordNew()
        {

            var model = new TimeReleaseModel();

            string userID = Session["userID"].ToString();
            var demand = _demandService.GetAll(Session["customerID"].ToString(), "0", Session["userID"].ToString());
            var activity = _parameterValueService.GetAllByParameterID("203200");

            model.LoadActivitys = activity.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadDemands = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

            var users = _userService.GetAll(0);

            model.LoadCollaborators = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            //model.CollaboratorID = userID;
            model.RegisterDate = Convert.ToDateTime(DateTime.Today).ToString("dd/MM/yyyy");
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");


            return PartialView("AbsenceRecord", model);
        }

        [HttpPost]
        public ActionResult AbsenceRecordAdd(TimeReleaseModel model)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterAbastecimentoCommand
            {
                AllowChangeStatus = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para adicionar registro de ausência");

                return RedirectToAction("Index");
            }

            if (Convert.ToString(Session["isAdmin"]) == "False")
            {
                WarningNotification(string.Format("Você não tem permissão para realizar registro! "));

                return RedirectToAction("Index", "TimeRelease");
            }

            if (_timeReleaseService.GetNotAbsence(Convert.ToInt32(model.CollaboratorID), model.RegisterDate) != "0")
            {
                WarningNotification(string.Format("Já existe(m) registro(s) para este dia. Procure seu gestor! "));

                return RedirectToAction("Index", "TimeRelease");
            }

            try
            {
                model.StartWork = "00:00";
                model.EndWork = "00:00";
                model.IsApproved = true;
                model.ApprovedByID = Session["userID"].ToString();
                model.ApprovedDate = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
                model.Description = Server.HtmlDecode(model.Description);

                model.CreatedByID = model.CollaboratorID;

                var command = MaintenanceCommand(model);

                _timeReleaseService.Add(command);

                SuccessNotification(string.Format("Registro realizado com sucesso! "));

                return RedirectToAction("Index", "TimeRelease");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi realizar registro! "));

                return RedirectToAction("Index", "TimeRelease");
            }
        }

        private string ValidateApropriate(TimeReleaseModel timeRelease)
        {
            var limitDay = _systemParameterService.GetByID(1);

            var LocalSystemParamtermodel = new SystemParameterModel();

            LocalSystemParamtermodel = limitDay.Value.ToModel();

            //regras de validacao de lancamentos de 
            //convert para o formato de data
            DateTime registerDate = DateTime.ParseExact(timeRelease.RegisterDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (registerDate < DateTime.Today.AddDays(-Convert.ToInt32(LocalSystemParamtermodel.ParamterValue)))
            {
                var localUser = _userService.GetByID(Convert.ToInt32(Session["userID"]));

                var localUserModel = new UserModel();

                localUserModel = localUser.Value.ToModel();

                DateTime updateRecordTo = DateTime.ParseExact(localUserModel.UpdateRecordTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DateTime releaseDateUpdateRecordTo = DateTime.ParseExact(localUserModel.ReleaseDateUpdateRecordTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (registerDate >= updateRecordTo)
                {
                    if (releaseDateUpdateRecordTo < DateTime.Today)
                    {
                        return string.Format("Para apropriação de horas em datas menores que {0}, você deve solicitar a liberação para o RH!",
                            DateTime.Today.AddDays(-Convert.ToInt32(LocalSystemParamtermodel.ParamterValue)).ToString("dd/MM/yyyy"));
                    }
                }
                else
                {
                    return string.Format("Para apropriação de horas em datas menores que {0}, você deve solicitar a liberação para o RH!", localUserModel.UpdateRecordTo);
                }
            }

            //regras de validação de demandas
            var demand = _demandService.GetByID(Convert.ToInt32(timeRelease.DemandID));

            var LocalDemand = new DemandModel();

            LocalDemand = demand.Value.ToModel();

            DateTime endDate = DateTime.ParseExact(LocalDemand.PlanningEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (endDate < DateTime.Today)
            {
                return string.Format("Não é possível realizar registro. A demanda associada contêm a data de término planejado inferior a data atual. Solicite a alteração da data término da demanda para seu gestor! ");
            }

            //quantity of appropriate hour on demand
            string totalTime = _demandService.GetTotalHoursByDemandID(timeRelease.DemandID);

            //Absence record of user
            if (_timeReleaseService.GetAbsence(Convert.ToInt32(Session["userID"]), timeRelease.RegisterDate) != "0")
            {
                return string.Format("Existe um registro para este dia! ");
            }

            //register between startWork en endWork to registerDate
            if (!string.IsNullOrEmpty(_timeReleaseService.GetApropriateByRangeTime(timeRelease.TimeReleaseID, Convert.ToInt32(timeRelease.CreatedByID), timeRelease.RegisterDate, timeRelease.StartWork, timeRelease.EndWork)))
            {
                return string.Format("Existem um ou mais registros para este período. Por favor verifique seus lançamentos! ");
            }
            return "";
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, TimeReleaseModel model)
        {
            var gridModel = new DataSourceResult();

            //permissions
            if (_profilesService.GetAllow(new FilterAbastecimentoCommand
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
                var timeReleases = _timeReleaseService.GetAll(new FilterTimeReleaseCommand
                {
                    StartDate = model.SearchStartDate,
                    EndDate = model.SearchEndDate,
                    DemandID = model.SearchDemandID,
                    CustomerID = model.SearchCustomerID,
                    //IsApproved = model.SearchIsApproved,
                    CreatedByID = (Convert.ToString(Session["isAdmin"]) == "False") ? Convert.ToString(Session["userID"]) : model.SearchCreatedByID,
                }, request.Page - 1, request.PageSize);

                gridModel = new DataSourceResult
                {
                    Data = timeReleases.Select(x =>
                    {
                        var timeReleasesModel = x.ToModel();

                        return timeReleasesModel;
                    }),
                    Total = timeReleases.TotalCount
                };

                return Json(gridModel);
            }
        }
        public ActionResult GetTotalByUsers(DataSourceRequest request, TimeReleaseModel model)
        {
            var gridModel = new DataSourceResult();

            //permissions
            if (_profilesService.GetAllow(new FilterAbastecimentoCommand
            {
                AllowReportView = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para visualizar o relatório!");

                model.SearchStartDateReport = "";
                model.SearchEndDateReport = "";

                return Json(gridModel);
            }

            else
            {
                var timeReleases = _timeReleaseService.GetTotalByUsersNoPage(new FilterTimeReleaseCommand
                {
                    StartDate = model.SearchStartDateReport,
                    EndDate = model.SearchEndDateReport
                });//, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = timeReleases.Select(x =>
                    {
                        var timeReleasesModel = x.ToModel();

                        return timeReleasesModel;
                    }),
                    //Total = timeReleases.TotalCount
                };

                return Json(gridModel);
            }
        }
        public ActionResult GetTotalData()
        {
            //var totalData = _dashboardService.timeRelaease();

            return null;
        }

        [HttpPost]
        public ActionResult GetAllByDemandID(DataSourceRequest request, string recordID)
        {

            var timeReleases = _timeReleaseService.GetAll(new FilterTimeReleaseCommand
            {
                DemandID = recordID,
                CreatedByID = (Convert.ToString(Session["isAdmin"]) == "False") ? Convert.ToString(Session["userID"]) : null,
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = timeReleases.Select(x =>
                {
                    var timeReleasesModel = x.ToModel();

                    return timeReleasesModel;
                }),
                Total = timeReleases.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New(string demandID)
        {
            var model = new TimeReleaseModel();

            string userID = Session["userID"].ToString();

            if (string.IsNullOrEmpty(demandID)) demandID = "0";

            var demand = _demandService.GetAll(Session["customerID"].ToString(), demandID, Session["userID"].ToString(), true);

            var activity = _parameterValueService.GetAllByParameterID("203200");

            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");

            model.LoadActivitys = activity.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.LoadDemands = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

            model.SearchLoadCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();

            model.IsApproved = false;

            model.CreatedByID = Convert.ToString(Session["userID"]);

            model.RegisterDate = Convert.ToDateTime(DateTime.Today).ToString("dd/MM/yyyy");

            model.CreationDate = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        private MaintenanceTimeReleaseCommand MaintenanceCommand(TimeReleaseModel model)
        {
            MaintenanceTimeReleaseCommand command = new MaintenanceTimeReleaseCommand();

            command.TimeReleaseID = model.TimeReleaseID;
            command.RegisterDate = model.RegisterDate;
            command.StartWork = model.StartWork;
            command.EndWork = model.EndWork;
            command.DemandID = model.DemandID;
            command.IsApproved = model.IsApproved;
            command.ActivityID = model.ActivityID;
            command.ApprovedByID = model.ApprovedByID;
            command.ApprovedDate = model.ApprovedDate;
            command.Description = model.Description;
            command.ReasonChange = model.ReasonChange;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int timeReleaseID, string ActionName)
        {
            var model = new TimeReleaseModel();

            string userID = Session["userID"].ToString();


            Result<TimeRelease> timeRelease = _timeReleaseService.GetByID(timeReleaseID);

            if (timeRelease.IsSuccess)
            {
                model = timeRelease.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    if (model.DemandID == null) model.DemandID = "0";

                    var demand = _demandService.GetAll(Session["customerID"].ToString(), model.DemandID, Session["userID"].ToString());

                    var activity = _parameterValueService.GetAllByParameterID("203200");

                    model.LoadActivitys = activity.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    model.LoadDemands = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

                    model.DayTotal = _timeReleaseService.GetTotalHours(model.CreatedByID, model.RegisterDate, model.RegisterDate);

                    DateTime date = DateTime.ParseExact(model.RegisterDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var startDate = new DateTime(date.Year, date.Month, 1);

                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    model.MounthTotal = _timeReleaseService.GetTotalHours(model.CreatedByID, Convert.ToDateTime(startDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(endDate).ToString("dd/MM/yyyy"));

                    model.TotalTime = (Convert.ToDateTime(model.EndWork) - Convert.ToDateTime(model.StartWork)).ToString().Substring(0, 5);

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }
            }

            return RedirectToAction("Index", "TimeRelease");
        }

        public ActionResult Delete(int timeReleaseID, string sourceController)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowDelete = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para excluir uma apropriação de horas!");
                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }

                if (timeReleaseID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído!"));
                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return Redirect("Index");
                }

                var model = new TimeReleaseModel();

                Result<TimeRelease> timeRelease = _timeReleaseService.GetByID(timeReleaseID);

                if (timeRelease.IsSuccess)
                {
                    model = timeRelease.Value.ToModel();
                    if (model.DemandID != null)
                    {
                        //validadr se o registro percente ao usuario logado
                        if (model.CreatedByID != Convert.ToString(Session["userID"]))
                        {
                            WarningNotification(string.Format("Não é permitido excluir o registro de outro usuário! "));
                            if (sourceController == "Demand")
                            {
                                return RedirectToAction("Index", "Demand");
                            }
                            return Redirect("Index");
                        }

                        _timeReleaseService.Delete(model.TimeReleaseID);

                        _historicalService.Delete(SystemFeatureID, timeReleaseID);

                        SuccessNotification(string.Format("Registro excluído com sucesso! "));
                        if (sourceController == "Demand")
                        {
                            return RedirectToAction("Index", "Demand");
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (Convert.ToString(Session["isAdmin"]) == "True")
                        {
                            _timeReleaseService.Delete(model.TimeReleaseID);

                            SuccessNotification(string.Format("Registro excluído com sucesso! "));
                            if (sourceController == "Demand")
                            {
                                return RedirectToAction("Index", "Demand");
                            }
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            WarningNotification(string.Format("Registro não pode ser excluído, contate seu gestor! "));
                            if (sourceController == "Demand")
                            {
                                return RedirectToAction("Index", "Demand");
                            }
                            return RedirectToAction("Index");

                        }
                    }
                }
                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                ErrorNotification(ex.Message);

                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(TimeReleaseModel model, HttpPostedFileBase file, string sourceController)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowUpdate = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para atualizar uma apropriação de horas!");
                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var msg = ValidateApropriate(model);

                    //historical
                    Historical(model);


                    //validadr se o registro percente ao usuario logado
                    if (model.CreatedByID != Convert.ToString(Session["userID"]))
                    {
                        WarningNotification(string.Format("Não é permitido atualizar o registro de outro usuário! "));
                        if (sourceController == "Demand")
                        {
                            return RedirectToAction("Index", "Demand");
                        }
                        return Redirect("Index");
                    }

                    if (model.IsApproved == true)
                    {
                        WarningNotification(string.Format("Não é permitido atualizar o registro já aprovado, contate seu gestor!"));
                        if (sourceController == "Demand")
                        {
                            return RedirectToAction("Index", "Demand");
                        }
                        return Redirect("Index");
                    }

                    if (msg == "")
                    {
                        var command = MaintenanceCommand(model);

                        _timeReleaseService.Update(command);

                        SuccessNotification(string.Format("Registro atualizado com sucesso! "));
                        if (sourceController == "Demand")
                        {
                            return RedirectToAction("Index", "Demand");
                        }
                        return RedirectToAction("Index", "TimeRelease");
                    }
                    else
                    {
                        //WarningNotification(msg);
                        WarningNotification(string.Format("Não foi possível salvar a atualização. Existem um ou mais registros para este período!"));

                        if (sourceController == "Demand")
                        {
                            return RedirectToAction("Index", "Demand");
                        }
                        return RedirectToAction("Index", "TimeRelease");

                    }
                }

                ErrorNotification(string.Format("Não foi possível realizar registro! "));
                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index", "TimeRelease");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro! "));
                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index", "TimeRelease");
            }
        }


        public ActionResult StatusChange(int timeReleaseID)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowChangeStatus = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para alterar o status de uma apropriação de horas!");

                    return RedirectToAction("Index");
                }

                if (Convert.ToString(Session["isAdmin"]) == "True")
                {
                    Result<TimeRelease> timeRelease = _timeReleaseService.GetByID(timeReleaseID);

                    TimeReleaseModel model = timeRelease.Value.ToModel();
                    if (model.CreatedByID != Session["userID"].ToString())
                    {
                        var command = MaintenanceCommand(model);

                        if (command.IsApproved == true)
                        {
                            command.IsApproved = false;
                            command.ApprovedByID = Convert.ToString(Session["userID"]);
                            command.ApprovedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                            _timeReleaseService.Update(command);

                            Historical(model);

                            SuccessNotification(string.Format("Registro alterado com sucesso! "));

                            return View();
                        }
                        else
                        {
                            command.IsApproved = true;
                            command.ApprovedByID = Convert.ToString(Session["userID"]);
                            command.ApprovedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                            _timeReleaseService.Update(command);

                            Historical(model);

                            SuccessNotification(string.Format("Registro alterado com sucesso! "));

                            return View();
                        }
                    }
                    else
                    {
                        WarningNotification(string.Format("Você não pode aprovar seu próprio lançamento, contate seu gestor! "));

                        return View();
                    }
                }
                WarningNotification("Você não tem permissão para aprovar registro! ");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);
                throw;
            }
        }

        //Realizando histórico de alterações 
        private void Historical(TimeReleaseModel model)
        {
            var command = new TimeReleaseModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _timeReleaseService.GetByID(model.TimeReleaseID);

            command = LocalCommand.Value.ToModel();

            if (command.DemandID != model.DemandID)
            {
                string commandDemandID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.DemandID));

                string modelDemandID = _userService.GetUserNameByID(Convert.ToInt32(model.DemandID));

                AddHistorical(commandDemandID, modelDemandID, "Demanda", model.TimeReleaseID.ToString());
            }
            if (command.ApprovedByID != model.ApprovedByID)
            {
                string commandApprovedByID = _userService.GetUserNameByID(Convert.ToInt32(command.ApprovedByID));

                string modelApprovedByID = _userService.GetUserNameByID(Convert.ToInt32(model.ApprovedByID));

                AddHistorical(modelApprovedByID, commandApprovedByID, "Aprovado por", model.TimeReleaseID.ToString());
            }

            if (command.RegisterDate != model.RegisterDate) AddHistorical(command.RegisterDate, model.RegisterDate, "Data de Registro", model.TimeReleaseID.ToString());
            if (command.StartWork != model.StartWork) AddHistorical(command.StartWork, model.StartWork, "Inicio do Trabalho", model.TimeReleaseID.ToString());
            if (command.EndWork != model.EndWork) AddHistorical(command.EndWork, model.EndWork, "Término do Trabalho", model.TimeReleaseID.ToString());

            //Verificar Histórico do campo aprovado (bool)
            if (command.IsApproved != model.IsApproved)
            {
                string status1;
                string status2;

                if (command.IsApproved)
                {
                    status1 = "Lançado"; status2 = "Aprovado";
                }
                else
                {
                    status1 = "Aprovado"; status2 = "Lançado";
                }
                AddHistorical(status1, status2, "Status", model.TimeReleaseID.ToString());
            }
            if (command.ActivityID != model.ActivityID) AddHistorical(command.ActivityID, model.ActivityID, "Atividade", model.TimeReleaseID.ToString(), true);
            //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.TimeReleaseID.ToString());
            if (command.ApprovedDate != model.ApprovedDate) AddHistorical(model.ApprovedDate, command.ApprovedDate, "Data de Aprovação", model.TimeReleaseID.ToString());
            if (command.ReasonChange != model.ReasonChange) AddHistorical(command.ReasonChange, model.ReasonChange, "Justificativas", model.TimeReleaseID.ToString());
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
