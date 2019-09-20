using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Supports;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Supports;
using ControleVeiculos.Domain.Entities.Supports;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using System.Web;
using System.IO;
using ControleVeiculos.MVC.Models.Attachments;
using ControleVeiculos.Domain.Command.Attachments;
using ControleVeiculos.Domain.Command.Historicals;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.MVC.Models.Historicals;

namespace ControleVeiculos.MVC.Controllers
{
    public class SupportController : BaseController
    {
        private readonly ISupportService _supportService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IAttachmentService _attachmentService;
        private readonly IHistoricalService _historicalService;
        private readonly IFeatureService _featureService;
        private readonly IProfilesService _profilesService;
        private readonly ICustomerService _customerService;

        public SupportController(ISupportService supportService,
                                IParameterValueService parameterValueService,
                                IAttachmentService attachmentService,
                                IHistoricalService historicalService,
                                IFeatureService featureService,
                                IProfilesService profilesService,
                                IUserService userService,
                                ICustomerService customerService)
        {
            _supportService = supportService;
            _parameterValueService = parameterValueService;
            _attachmentService = attachmentService;
            _historicalService = historicalService;
            _featureService = featureService;
            _userService = userService;
            _profilesService = profilesService;
            _customerService = customerService;
        }

        private string SystemFeatureID = "112";

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new SupportModel();
            var status = _parameterValueService.GetAllByParameterID("112101");
            var severitys = _parameterValueService.GetAllByParameterID("112100");
            var prioritys = _parameterValueService.GetAllByParameterID("112102");
            var types = _parameterValueService.GetAllByParameterID("112103");
            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
            var users = _userService.GetAll(0);

            model.SearchLoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.SearchLoadPriority = prioritys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadSeverity = severitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadType = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadAssingTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SupportModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar um registro em suporte!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceSupportCommand(model);

                    string recordID = _supportService.Add(command);

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
        public ActionResult GetAll(DataSourceRequest request, SupportModel model)
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
                var supports = _supportService.GetAll(new FilterSupportCommand
                {
                    Summary = model.SearchSummary,
                    SeverityID = model.SearchSeverityID,
                    StatusID = model.SearchStatusID,
                    PriorityID = model.SearchPriorityID,
                    TypeID = model.SearchTypeID,
                    AssingToID = model.SearchAssingToID,
                    CreatedByID = model.SearchCreatedByID,
                    CustomerID = model.SearchCustomerID,
                }, request.Page - 1, request.PageSize);

                gridModel = new DataSourceResult
                {
                    Data = supports.Select(x =>
                    {
                        var supportsModel = x.ToModel();

                        return supportsModel;
                    }),
                    Total = supports.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new SupportModel();

            var status = _parameterValueService.GetAllByParameterID("112101");
            var severitys = _parameterValueService.GetAllByParameterID("112100");
            var prioritys = _parameterValueService.GetAllByParameterID("112102");
            var types = _parameterValueService.GetAllByParameterID("112103");
            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
            var users = _userService.GetAll(0);

            model.LoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.LoadPriority = prioritys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadSeverity = severitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadType = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadAssingTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            model.CustomerID =  Convert.ToString(Session["customerID"]);
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }


        private MaintenanceSupportCommand MaintenanceSupportCommand(SupportModel model)
        {
            MaintenanceSupportCommand command = new MaintenanceSupportCommand();

            command.SupportID = model.SupportID;
            command.Summary = model.Summary;
            command.Description = model.Description;
            command.SeverityID = model.SeverityID;
            command.StatusID = model.StatusID;
            command.PriorityID = model.PriorityID;
            command.TypeID = model.TypeID;
            command.AssingToID = model.AssingToID;
            command.ResolutionDate = model.ResolutionDate;
            command.CustomerID = model.CustomerID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int supportID, string ActionName)
        {
            var model = new SupportModel();

            Result<Support> support = _supportService.GetByID(supportID);

            if (support.IsSuccess)
            {
                model = support.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var status = _parameterValueService.GetAllByParameterID("112101");
                    var severitys = _parameterValueService.GetAllByParameterID("112100");
                    var prioritys = _parameterValueService.GetAllByParameterID("112102");
                    var types = _parameterValueService.GetAllByParameterID("112103");
                    var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
                    var users = _userService.GetAll(0);

                    model.LoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
                    model.LoadPriority = prioritys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadSeverity = severitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadType = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadAssingTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                    model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);

                }
            }
            return RedirectToAction("Index", "Support");
        }

        public ActionResult Delete(int supportID)
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
                    WarningNotification("Você não tem permissão para excluir um registro em Suporte!");

                    return RedirectToAction("Index");
                }

                if (supportID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir registro!"));
                    return Redirect("Index");
                }
                var model = new SupportModel();

                Result<Support> support = _supportService.GetByID(supportID);

                if (support.IsSuccess)
                {
                    model = support.Value.ToModel();

                    _supportService.Delete(model.SupportID);

                    _historicalService.Delete(SystemFeatureID, supportID);

                    _attachmentService.Delete(SystemFeatureID, supportID);

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
        public ActionResult Update(SupportModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para atualizar um registro em Suporte!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    //historical
                    Historical(model);

                    var command = MaintenanceSupportCommand(model);

                    _supportService.Update(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.SupportID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = "";
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.SupportID.ToString();
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


        private void Historical(SupportModel model)
        {
            var command = new SupportModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _supportService.GetByID(model.SupportID);

            command = LocalCommand.Value.ToModel();

            if (command.CustomerID != model.CustomerID)
            {
                string commandCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.CustomerID));

                string modelCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(model.CustomerID));

                AddHistorical(commandCustomerID, modelCustomerID, "Empresa", model.SupportID.ToString());
            }

            if (command.AssingToID != model.AssingToID)
            {
                string commandAssingToID = _userService.GetUserNameByID(Convert.ToInt32(command.AssingToID));

                string modelAssingToID = _userService.GetUserNameByID(Convert.ToInt32(model.AssingToID));

                AddHistorical(commandAssingToID, modelAssingToID,  "Associado", model.SupportID.ToString());
            }

            if (command.StatusID != model.StatusID) AddHistorical(command.StatusID, model.StatusID, "Status", model.SupportID.ToString(), true);
            if (command.TypeID != model.TypeID) AddHistorical(command.TypeID, model.TypeID, "Tipo", model.SupportID.ToString(), true);
            if (command.ResolutionDate != model.ResolutionDate) AddHistorical(command.ResolutionDate, model.ResolutionDate, "Data de Resolução", model.SupportID.ToString());
            if (command.Summary != model.Summary) AddHistorical(command.Summary, model.Summary, "Sumário", model.SupportID.ToString());
            //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.SupportID.ToString());
            if (command.PriorityID != model.PriorityID) AddHistorical(command.PriorityID, model.PriorityID, "Prioridade", model.SupportID.ToString(), true);
            if (command.SeverityID != model.SeverityID) AddHistorical(command.SeverityID, model.SeverityID, "Severidade", model.SupportID.ToString(), true);

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