using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Tasks;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Tasks;
using Lean.Test.Cloud.Domain.Entities.Tasks;
using Lean.Test.Cloud.Domain;
using System.Web;
using System.IO;
using Lean.Test.Cloud.MVC.Models.Attachments;
using Lean.Test.Cloud.Domain.Command.Attachments;
using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.MVC.Models.Historicals;



namespace Lean.Test.Cloud.MVC.Controllers
{
    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IUserService _userService1;
        private readonly ICustomerService _customerService;
        private readonly IAttachmentService _attachmentService;
        private readonly IProfilesService _profilesService;
        private readonly IDemandService _demandService;
        private readonly IHistoricalService _historicalService;


        public TaskController(ITaskService taskService,
                                IParameterValueService parameterValueService,
                                IUserService userService,
                                IUserService userService1,
                                ICustomerService customerService,
                                IAttachmentService attachmentService,
                                IProfilesService profilesService,
                                IDemandService demandService,
                                IHistoricalService historicalService)
        {
            _taskService = taskService;
            _parameterValueService = parameterValueService;
            _userService = userService;
            _userService1 = userService1;
            _customerService = customerService;
            _attachmentService = attachmentService;
            _profilesService = profilesService;
            _demandService = demandService;
            _historicalService = historicalService;
        }

        private string SystemFeatureID = "205";

        public JsonResult GetDemands(string customerID)
        {
            var demand = _demandService.GetAllByCustomerID(customerID);

            return Json(demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList());
        }

        public ActionResult New()
        {
            var model = new TaskModel();


            var demands = _demandService.GetAllByCustomerID(Session["customerID"].ToString());
            var status = _parameterValueService.GetAllByParameterID("301303");
            var users = _userService.GetAll(0);


            model.LoadAssignTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadDemand = demands.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();
            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }
                   
        [HttpPost]
        public ActionResult Add(TaskModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar uma tarefa!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    model.CustomerID = Session["customerID"].ToString();

                    var command = MaintenanceTaskCommand(model);

                    string recordID = _taskService.Add(command);

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

                        SuccessNotification(string.Format("Tarefa criada com sucesso! "));

                        return RedirectToAction("Index");
                    }

                    SuccessNotification(string.Format("Tarefa criada com sucesso, sem anexo! "));

                    return RedirectToAction("Index");
                }

                ErrorNotification(string.Format("Não foi possível criar a tarefa!"));

                return RedirectToAction("Index");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível criar a tarefa!"));

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int taskID)
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
                    WarningNotification("Você não tem permissão para excluir uma tarefa!");

                    return RedirectToAction("Index");
                }

                if (taskID == 0)
                {
                    ErrorNotification(string.Format("Registro de tarefa não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new TaskModel();

                Result<Task> task = _taskService.GetByID(taskID);

                if (task.IsSuccess)
                {
                    model = task.Value.ToModel();


                    _taskService.Delete(model.TaskID);

                    _historicalService.Delete(SystemFeatureID, taskID);

                    _attachmentService.Delete(SystemFeatureID, taskID);

                    SuccessNotification(string.Format("Registo de tarefa excluído com sucesso!"));

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

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new TaskModel();

            var assignTos = _userService.GetAll(0);
            var demands = _demandService.GetAllByCustomerID(Convert.ToString(Session["customerID"])); 
            var status = _parameterValueService.GetAllByParameterID("301303");
            var createdBy = _userService.GetAll(0);

            model.SearchLoadAssignTo = assignTos.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadCreatedBy = createdBy.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();


            return View(model);
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, TaskModel model)
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
                WarningNotification("Você não tem permissão para visualizar registros em Suporte!");

                return Json(gridModel);
            }
            else
            {
                var tasks = _taskService.GetAll(new FilterTaskCommand
                {
                    Summary = model.SearchSummary,
                    AssignToID = model.SearchAssignToID,
                    CreatedByID = model.SearchCreatedByID,
                    StatusID = model.SearchStatusID,
                }, request.Page - 1, request.PageSize);


                 gridModel = new DataSourceResult
                {
                    Data = tasks.Select(x =>
                    {
                        var tasksModel = x.ToModel();

                        return tasksModel;
                    }),
                    Total = tasks.TotalCount
                };

                return Json(gridModel);
            }
        }
        [HttpPost]
        public ActionResult GetAllByDemandID(DataSourceRequest request, string recordID)
        {
            var tasks = _taskService.GetAll(new FilterTaskCommand
            {
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = tasks.Select(x =>
                {
                    var tasksModel = x.ToModel();

                    return tasksModel;
                }),
                Total = tasks.TotalCount
            };
            return Json(gridModel);
        }


        public ActionResult GetByID(int taskID, string ActionName)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new TaskModel();

            Result<Task> task = _taskService.GetByID(taskID);

            if (task.IsSuccess)
            {
                model = task.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var demands = _demandService.GetAllByCustomerID(Convert.ToString(Session["customerID"]));
                    var status = _parameterValueService.GetAllByParameterID("301303");

                    var users = _userService.GetAll(Convert.ToInt32(model.AssignToID));
                    model.LoadAssignTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    var users1 = _userService.GetAll(Convert.ToInt32(model.CreatedByID));
                    model.LoadCreatedBy = users1.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    model.LoadDemand = demands.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();
                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }
            }

            return RedirectToAction("Index", "Task");
        }
            
                  

        private MaintenanceTaskCommand MaintenanceTaskCommand(TaskModel model)
        {
            MaintenanceTaskCommand command = new MaintenanceTaskCommand();

            command.TaskID = model.TaskID;
            command.Summary = model.Summary;
            command.Description = model.Description;
            command.AssignToID = model.AssignToID;
            command.DemandID = model.DemandID;
            command.CustomerID = model.CustomerID;
            //command.ProgressPercentage = model.ProgressPercentage.Replace("%", "").Replace(".", "").Replace(" ", "");
            command.StatusID = model.StatusID;
            command.TargetDate = model.TargetDate;
            command.ClosingDate = model.ClosingDate;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult StatusChange(int taskID)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                    WarningNotification("Você não tem permissão para alterar o status de uma tarefa!");

                    return RedirectToAction("Index");
                }

                if (Convert.ToString(Session["isAdmin"]) == "True")
                {
                    Result<Task> task = _taskService.GetByID(taskID);

                   TaskModel model = task.Value.ToModel();
                     
                    var command = MaintenanceTaskCommand(model);
                   
                    //Se status for diferente de "Concluída"
                    if (command.StatusID == "301303300" || 
                        command.StatusID == "301303301" || 
                        command.StatusID == "301303303" || 
                        command.StatusID == "301303304" || 
                        command.StatusID == "301303305")
                    {
                        command.StatusID = "301303302";
                        //command.ClosingDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                       _taskService.Update(command);
                        Historical(model);
                        SuccessNotification(string.Format("Registro alterado com sucesso! "));
                        return View();
                    }
                    else
                    {
                        command.StatusID = "301303301";
                        command.ClosingDate = "";
                        _taskService.Update(command);
                        SuccessNotification(string.Format("Registro alterado com sucesso! "));
                        return View();
                    }

                }


                WarningNotification("Você não pode alterar o status da tarefa criada por você mesmo!");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);

                throw;
            }
        }

        [HttpPost]
        public ActionResult Update(TaskModel model, HttpPostedFileBase file)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                    WarningNotification("Você não tem permissão para atualizar uma tarefa!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    Historical(model);
                    

                    var command = MaintenanceTaskCommand(model);

                    _taskService.Update(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.TaskID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = "";
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.TaskID.ToString();
                        attachmentModel.SizeFile = size;
                        attachmentModel.SystemFeatureID = SystemFeatureID;
                        attachmentModel.CreatedByID = Convert.ToString(Session["userID"]);
                        attachmentModel.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                        var localCommand = MaintenanceAttachmentCommand(attachmentModel);

                        _attachmentService.Add(localCommand);
                    }
                    SuccessNotification(string.Format("Registro atualizado com sucesso!"));

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

        private void Historical(TaskModel model)
        {
            model.CustomerID = Session["customerID"].ToString();

            var command = new TaskModel();
            var modelHistorical = new HistoricalModel();

            var LocalCommand = _taskService.GetByID(model.TaskID);

            command = LocalCommand.Value.ToModel();

            if (command.DemandID != model.DemandID)
            {
               string commandDemandID = _demandService.GetDemandNameByID(Convert.ToInt32(command.DemandID));

               string modelDemandID = _demandService.GetDemandNameByID(Convert.ToInt32(model.DemandID));

               AddHistorical(commandDemandID, modelDemandID, "Demanda", model.TaskID.ToString());
            }

            if (command.Summary != model.Summary) AddHistorical(command.Summary, model.Summary, "Sumário", model.TaskID.ToString());
            if (command.StatusID != model.StatusID) AddHistorical(command.StatusID, model.StatusID, "Status", model.TaskID.ToString(), true);
            if (command.TargetDate != model.TargetDate) AddHistorical(command.TargetDate, model.TargetDate, "Data Alvo", model.TaskID.ToString());

            if (command.AssignToID != model.AssignToID)
            {
                string commandAssignToID = _userService.GetUserNameByID(Convert.ToInt32(command.AssignToID));

                string modelAssignToID = _userService.GetUserNameByID(Convert.ToInt32(model.AssignToID));

                AddHistorical(commandAssignToID, modelAssignToID, "Responsável por executar a tarefa", model.TaskID.ToString());
            }

            if (command.CreationDate != model.CreationDate) AddHistorical(command.CreationDate, model.CreationDate, "Data de criação da tarefa", model.TaskID.ToString());
            //if (command.ClosingDate != model.ClosingDate) AddHistorical(command.ClosingDate, model.ClosingDate, "Data de conclusão da tarefa", model.TaskID.ToString());
            //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.TaskID.ToString());
            //if (command.ProgressPercentage != model.ProgressPercentage.Replace("%", "")) AddHistorical(command.ProgressPercentage, model.ProgressPercentage.Replace("%", ""), "Progresso", model.TaskID.ToString());

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

        public ActionResult Kanban()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var gridModel = new DataSourceResult();
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowView = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para visualizar o Kanban!");

                return RedirectToAction("Index");

            }
           
            var model = new TaskModel();

            var assignTos = _userService.GetAll(0);
            var demands = _demandService.GetAllByCustomerID(Convert.ToString(Session["customerID"]));
            var status = _parameterValueService.GetAllByParameterID("301303");
            var createdBy = _userService.GetAll(0);

            model.SearchLoadAssignTo = assignTos.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadCreatedBy = createdBy.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            return View(model);
        }

        
        public ActionResult GeAlltKanban(TaskModel model)
        {
            var tasks = _taskService.GetAllKanban(new FilterTaskCommand
            {
                Summary = model.SearchSummary,
                AssignToID = model.SearchAssignToID,
                CreatedByID = model.SearchCreatedByID,
                StatusID = model.SearchStatusID,
            });

            return Json(tasks);
        }

        public ActionResult UpdateKanban(int taskID, string statusID)
        {
            Result<Task> task = _taskService.GetByID(taskID);
            TaskModel model = task.Value.ToModel();
            var command = MaintenanceTaskCommand(model);
                    
            command.StatusID = statusID;
            command.ClosingDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Historical(model);
            _taskService.Update(command);

            return Json(command);
        }
    }
}