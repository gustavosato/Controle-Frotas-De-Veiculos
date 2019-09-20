using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;       
using ControleVeiculos.MVC.Models.Defects;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Defects;
using ControleVeiculos.Domain.Entities.Defects;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using System.Web;
using System.IO;
using ControleVeiculos.MVC.Models.Attachments;
using ControleVeiculos.Domain.Command.Attachments;
using ControleVeiculos.MVC.Models.Historicals;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain.Command.Historicals;

namespace ControleVeiculos.MVC.Controllers
{
    public class DefectController : BaseController
    {
        private readonly IDefectService _defectService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IApplicationSystemService _applicationSystemService;
        private readonly IAttachmentService _attachmentService;
        private readonly IProfilesService _profilesService;
        private readonly IHistoricalService _historicalService;
        private readonly IFeatureService _featureService;

        public DefectController(IDefectService defectService, 
                                IParameterValueService parameterValueService,
                                IAttachmentService attachmentService,
                                IApplicationSystemService applicationSystemService,
                                IHistoricalService historicalService,
                                IProfilesService profilesService,
                                IFeatureService featureService,
                                IUserService userService)
        {
            _defectService = defectService;
            _parameterValueService = parameterValueService;
            _attachmentService = attachmentService;
            _applicationSystemService = applicationSystemService;
            _historicalService = historicalService;
            _profilesService = profilesService;
            _featureService = featureService;
            _userService = userService;
        }

        private string SystemFeatureID = "212";

        public JsonResult GetFeatures(string applicationSystemID)
        {
            var demand = _featureService.GetAll(applicationSystemID);

            return Json(demand.Select(x => new SelectListItem() { Text = x.featureName.ToString(), Value = x.featureID.ToString() }).ToList());
        }

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new DefectModel();

            var status = _parameterValueService.GetAllByParameterID("212200");
            var severitys = _parameterValueService.GetAllByParameterID("212201");
            var prioritys = _parameterValueService.GetAllByParameterID("212202");
            var defectTypes = _parameterValueService.GetAllByParameterID("212203");
            var resolutionTypes = _parameterValueService.GetAllByParameterID("212204");
            var users = _userService.GetAll(0);
            var applications = _applicationSystemService.GetAll(Convert.ToInt32(Session["customerID"]));

            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadSeverity = severitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadPriority = prioritys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadType= defectTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadResolution = resolutionTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadAssingTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadApplicationSystems = applications.Select(x => new SelectListItem() { Text = x.applicationSystemName.ToString(), Value = x.applicationSystemID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(DefectModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar um defeito!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceDefectCommand(model);

                    string recordID = _defectService.Add(command);

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

                    return RedirectToAction("Index");
                }
                ErrorNotification(string.Format("Não foi possível realizar registro"));

                return RedirectToAction("Index");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, DefectModel model)
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
                WarningNotification("Você não tem permissão para visualizar os defeitos!");

                return Json(gridModel);
            }
            else
            {
                var defects = _defectService.GetAll(new FilterDefectCommand
                {
                    Summary = model.SearchSummary,
                    StatusID = model.SearchStatusID,
                    SeverityID = model.SearchSeverityID,
                    PriorityID = model.SearchPriorityID,
                    AssingToID = model.SearchAssingToID,
                    CreatedByID = model.SearchCreatedByID
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = defects.Select(x =>
                    {
                        var defectsModel = x.ToModel();

                        return defectsModel;
                    }),
                    Total = defects.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new DefectModel();

            var status = _parameterValueService.GetAllByParameterID("212200");
            var severitys = _parameterValueService.GetAllByParameterID("212201");
            var prioritys = _parameterValueService.GetAllByParameterID("212202");
            var defectTypes = _parameterValueService.GetAllByParameterID("212203");
            var resolutionTypes = _parameterValueService.GetAllByParameterID("212204");
            var users = _userService.GetAll(0);
            var applications = _applicationSystemService.GetAll(Convert.ToInt32(Session["customerID"]));


            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadSeverity = severitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadPriority = prioritys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadType = defectTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadResolution = resolutionTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadAssingTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadApplicationSystems = applications.Select(x => new SelectListItem() { Text = x.applicationSystemName.ToString(), Value = x.applicationSystemID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
            return PartialView("Maintenance", model);
        }


        private MaintenanceDefectCommand MaintenanceDefectCommand(DefectModel model)
        {
            MaintenanceDefectCommand command = new MaintenanceDefectCommand();
                            
            command.DefectID = model.DefectID;
            command.Summary = model.Summary;
            command.Description = model.Description;
            command.StatusID = model.StatusID;
            command.SeverityID = model.SeverityID;
            command.PriorityID = model.PriorityID;
            command.AssingToID = model.AssingToID;
            command.TypeID = model.TypeID;
            command.ResolutionID = model.ResolutionID;
            command.Resolution = model.Resolution;
            command.ResolutionDate = model.ResolutionDate;
            command.ApplicationSystemID = model.ApplicationSystemID;
            command.FeatureID = model.FeatureID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 

            return command;
        }

        public ActionResult GetByID(int defectID, string ActionName)
        {
            var model = new DefectModel();

            Result<Defect> defect = _defectService.GetByID(defectID);

            if (defect.IsSuccess)
            {
                model = defect.Value.ToModel();
                
                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var status = _parameterValueService.GetAllByParameterID("212200");
                    var severitys = _parameterValueService.GetAllByParameterID("212201");
                    var prioritys = _parameterValueService.GetAllByParameterID("212202");
                    var defectTypes = _parameterValueService.GetAllByParameterID("212203");
                    var resolutionTypes = _parameterValueService.GetAllByParameterID("212204");
                    var applications = _applicationSystemService.GetAll(Convert.ToInt32(Session["customerID"]));

                    var users = _userService.GetAll(Convert.ToInt32(model.AssingToID));
                    model.LoadAssingTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    var users1 = _userService.GetAll(Convert.ToInt32(model.CreatedByID));
                    model.LoadCreatedBy = users1.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadSeverity = severitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadPriority = prioritys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadType = defectTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadResolution = resolutionTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    model.LoadApplicationSystems = applications.Select(x => new SelectListItem() { Text = x.applicationSystemName.ToString(), Value = x.applicationSystemID.ToString() }).ToList();

                    var features = _featureService.GetAll(model.ApplicationSystemID);
                    model.LoadFeatures = features.Select(x => new SelectListItem() { Text = x.featureName.ToString(), Value = x.featureID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    model.Resolution = Server.HtmlDecode(model.Resolution);

                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "Defect");
        }

        public ActionResult Delete(int defectID)
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
                    WarningNotification("Você não tem permissão para excluir um defeito!");

                    return RedirectToAction("Index");
                }

                if (defectID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir registro!"));
                    return Redirect("Index");
                }
                var model = new DefectModel();

                Result<Defect> defect = _defectService.GetByID(defectID);

                if (defect.IsSuccess)
                {
                    model = defect.Value.ToModel();

                    _defectService.Delete(model.DefectID);

                    _historicalService.Delete(SystemFeatureID, defectID);

                    _attachmentService.Delete(SystemFeatureID, defectID);

                    SuccessNotification(string.Format("Registro excluido com sucesso! Defeito: {0}", model.Summary));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("O registro contêm outros associados, exclua primeiro as funcionalidades.");

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(DefectModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para atualizar um defeito!");

                    return RedirectToAction("Index");
                }


                if (ModelState.IsValid)
                {
                    //historical
                    Historical(model);

                    var command = MaintenanceDefectCommand(model);

                    _defectService.Update(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.DefectID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = "";
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.DefectID.ToString();
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

                ErrorNotification("Não foi possível salvar alteração!");

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

        //Realizando histórico de alterações 


        private void Historical(DefectModel model)
        {
            var command = new DefectModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _defectService.GetByID(model.DefectID);

            command = LocalCommand.Value.ToModel();

            if (command.ApplicationSystemID != model.ApplicationSystemID)
            {
                string commandApplicationSystemID = _applicationSystemService.GetApplicationSystemNameByID(Convert.ToInt32(command.ApplicationSystemID));

                string modelApplicationSystemID = _applicationSystemService.GetApplicationSystemNameByID(Convert.ToInt32(model.ApplicationSystemID));

                AddHistorical(commandApplicationSystemID, modelApplicationSystemID, "Aplicação", model.DefectID.ToString());
            }

            if (command.FeatureID != model.FeatureID)
            {
                string commandFeatureID = _featureService.GetFeatureNameByID(Convert.ToInt32(command.FeatureID));

                string modelFeatureID = _featureService.GetFeatureNameByID(Convert.ToInt32(model.FeatureID));

                AddHistorical(commandFeatureID, modelFeatureID, "Funcionalidade", model.DefectID.ToString());
            }

            if (command.AssingToID != model.AssingToID)
            {
                string commandAssingToID = _userService.GetUserNameByID(Convert.ToInt32(command.AssingToID));

                string modelAssingToID = _userService.GetUserNameByID(Convert.ToInt32(model.AssingToID));

                AddHistorical(commandAssingToID, modelAssingToID,  "Associar", model.DefectID.ToString());
            }

            if (command.CreatedByID != model.CreatedByID)
            {
                string commandCreatedByID = _userService.GetUserNameByID(Convert.ToInt32(command.CreatedByID));

                string modelCreatedByID = _userService.GetUserNameByID(Convert.ToInt32(model.CreatedByID));

                AddHistorical(modelCreatedByID, commandCreatedByID, "Criado", model.DefectID.ToString());
            }

            if (command.StatusID != model.StatusID) AddHistorical(command.StatusID, model.StatusID, "Status", model.DefectID.ToString(), true);
            if (command.SeverityID != model.SeverityID) AddHistorical(command.SeverityID, model.SeverityID, "Severidade", model.DefectID.ToString(), true);
            if (command.PriorityID != model.PriorityID) AddHistorical(command.PriorityID, model.PriorityID, "Prioridade", model.DefectID.ToString(), true);
            if (command.TypeID != model.TypeID) AddHistorical(command.TypeID, model.TypeID, "Tipo", model.DefectID.ToString(), true);
            if (command.ResolutionID != model.ResolutionID) AddHistorical(command.ResolutionID, model.ResolutionID, "Tipo de Resolução", model.DefectID.ToString(), true);
            if (command.ResolutionDate != model.ResolutionDate) AddHistorical(command.ResolutionDate, model.ResolutionDate, "Data de Resolução", model.DefectID.ToString());
            if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.DefectID.ToString());
            if (command.Summary != model.Summary) AddHistorical(command.Summary, model.Summary, "Sumário", model.DefectID.ToString());
            if (command.Resolution != model.Resolution) AddHistorical(command.Resolution, model.Resolution, "Resolução", model.DefectID.ToString());

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