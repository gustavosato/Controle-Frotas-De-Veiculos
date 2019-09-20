using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.DailyLogs;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.DailyLogs;
using ControleVeiculos.Domain.Entities.DailyLogs;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using System.Web;
using System.IO;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.MVC.Models.Attachments;
using ControleVeiculos.Domain.Command.Attachments;

namespace ControleVeiculos.MVC.Controllers
{
    public class DailyLogController : BaseController
    {
        private readonly IDailyLogService _dailyLogService;
        private readonly IUserService _userService;
        private readonly IDemandService _demandService;
        private readonly IProfilesService _profilesService;
        private readonly IAttachmentService _attachmentService;

        public DailyLogController(IDailyLogService dailyLogService,
                                    IUserService userService,
                                    IProfilesService profilesService,
                                    IAttachmentService attachmentService,
                                    IDemandService demandService)
        {
            _dailyLogService = dailyLogService;
            _userService = userService;
            _attachmentService = attachmentService;
            _profilesService = profilesService;
            _demandService = demandService;
        }

        string SystemFeatureID = "201";

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new DailyLogModel();

            string userID = Session["userID"].ToString();

            var demand = _demandService.GetAllByCustomerID(Session["customerID"].ToString());

            var createdBy = _userService.GetAll(0);

            model.SearchLoadCreateds = createdBy.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            model.SearchLoadDemands = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

            model.Description = Server.HtmlDecode(model.Description);

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(DailyLogModel model,  HttpPostedFileBase file, string sourceController)
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
                    WarningNotification("Você não tem permissão para adicionar um evento!");

                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceDailyLogCommand(model);

                    string recordID = _dailyLogService.Add(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + recordID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = fileName;
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = recordID;
                        attachmentModel.SizeFile = size;
                        attachmentModel.SystemFeatureID = SystemFeatureID;
                        attachmentModel.CreatedByID = Convert.ToString(Session["userID"]);
                        attachmentModel.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                        var localCommand = MaintenanceAttachmentCommand(attachmentModel);

                        _attachmentService.Add(localCommand);
                    }
                    SuccessNotification(string.Format("Registro realizado com sucesso!"));

                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }
                ErrorNotification(string.Format("Não foi possível realizar registro, verifique os campos obrigatórios!"));

                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, DailyLogModel model)
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
                WarningNotification("Você não tem permissão para visualizar os eventos!");

                return Json(gridModel);

            }
            else
            { 
            var dailyLogs = _dailyLogService.GetAll(new FilterDailyLogCommand
            {
                Description = model.SearchDescription,
                DemandID = model.SearchDemandID,
                CreatedByID = model.SearchCreatedID,
                UserID = Session["userID"].ToString(),
                customerID = Session["customerID"].ToString()
            }, request.Page - 1, request.PageSize);

            gridModel = new DataSourceResult
            {
                Data = dailyLogs.Select(x =>
                {
                    var dailyLogsModel = x.ToModel();

                    return dailyLogsModel;
                }),
                Total = dailyLogs.TotalCount
            };
            return Json(gridModel);
        }
    }

        [HttpPost]
        public ActionResult GetAllByDemandID(DataSourceRequest request, string recordID)
        {
            var dailyLogs = _dailyLogService.GetAll(new FilterDailyLogCommand
            {
                DemandID = recordID,
                customerID = Session["customerID"].ToString()
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = dailyLogs.Select(x =>
                {
                    var dailyLogsModel = x.ToModel();

                    return dailyLogsModel;
                }),
                Total = dailyLogs.TotalCount
            };
            return Json(gridModel);
        }

        public ActionResult New(string demandID)
        {
            var model = new DailyLogModel();

            string userID = Session["userID"].ToString();

            var demand = _demandService.GetAll(Session["customerID"].ToString(), demandID, Session["userID"].ToString());

            model.CreatedByID = Convert.ToString(Session["userID"]);

            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            model.LoadDemands = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

            model.DemandID = demandID;

            return PartialView("Maintenance", model);
        }

        private MaintenanceDailyLogCommand MaintenanceDailyLogCommand(DailyLogModel model)
        {
            MaintenanceDailyLogCommand command = new MaintenanceDailyLogCommand();

            command.DailyLogID = model.DailyLogID;
            command.DemandID = model.DemandID;
            command.Description = model.Description;
            command.IsInternal = model.IsInternal;
            command.CreatedByID =  model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int dailyLogID, string ActionName)
        {
            var model = new DailyLogModel();

            string userID = Session["userID"].ToString();

            Result<DailyLog> dailyLog = _dailyLogService.GetByID(dailyLogID);

            if (dailyLog.IsSuccess)
            {
                model = dailyLog.Value.ToModel();


                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var demand = _demandService.GetAll(Session["customerID"].ToString(), model.DemandID, Session["userID"].ToString());

                    model.LoadDemands = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
                else
                //AllowApropriate
                {
                    return PartialView("AllowApropriate", model);
                }
            }
            return RedirectToAction("Index", "User");
        }

        public ActionResult Delete(int dailyLogID, string sourceController)
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
                    WarningNotification("Você não tem permissão para excluir um evento!");

                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }

                if (dailyLogID == 0)
                {
                    ErrorNotification(string.Format(" Registro não pode ser excluído!"));

                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return Redirect("Index");
                }
                var model = new DailyLogModel();

                Result<DailyLog> dailyLog = _dailyLogService.GetByID(dailyLogID);

                if (dailyLog.IsSuccess)
                {
                    model = dailyLog.Value.ToModel();

                    if (model.CreatedByID != Convert.ToString(Session["userID"]))
                    {
                        WarningNotification(string.Format("Esse registro só pode ser excluído pelo usuário que o criou!"));

                        if (sourceController == "Demand")
                        {
                            return RedirectToAction("Index", "Demand");
                        }
                        return RedirectToAction("Index");
                    }

                    _dailyLogService.Delete(model.DailyLogID);

                    _attachmentService.Delete(SystemFeatureID, dailyLogID);

                    SuccessNotification(string.Format("Registro excluído com sucesso!"));

                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
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
        public ActionResult Update(DailyLogModel model, HttpPostedFileBase file, string sourceController)
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
                    WarningNotification("Você não tem permissão para atualizar um evento!");
                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }

                if (model.CreatedByID != Convert.ToString(Session["userID"]))
                {
                    WarningNotification(string.Format("Esse registro só pode ser alterado pelo usuário que o criou!"));
                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }
                if (ModelState.IsValid)
                {
                    var command = MaintenanceDailyLogCommand(model);

                    _dailyLogService.Update(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.DailyLogID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = "";
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.DailyLogID.ToString();
                        attachmentModel.SizeFile = size;
                        attachmentModel.SystemFeatureID = SystemFeatureID;
                        attachmentModel.CreatedByID = Convert.ToString(Session["userID"]);
                        attachmentModel.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                        var localCommand = MaintenanceAttachmentCommand(attachmentModel);

                        _attachmentService.Add(localCommand);
                    }
                    SuccessNotification(string.Format("Registro atualizado com sucesso! "));

                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }
                ErrorNotification("Não foi possível atualizar registro!");

                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ErrorNotification("Erro ao alterar registro! ");

                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index");
            }
        }

        private MaintenanceAttachmentCommand MaintenanceAttachmentCommand(AttachmentModel model)
        {
            MaintenanceAttachmentCommand command = new MaintenanceAttachmentCommand();

            command.AttachmentID = model.AttachmentID;
            command.FileName = model.FileName;
            command.SizeFile = model.SizeFile;
            command.PathFile = model.PathFile;
            command.BinaryFile = null;
            command.Description = model.Description;
            command.SystemFeatureID = model.SystemFeatureID;
            command.RecordID = model.RecordID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

    }
} 
    