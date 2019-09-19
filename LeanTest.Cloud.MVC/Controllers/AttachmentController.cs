    using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Attachments;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Attachments;
using Lean.Test.Cloud.Domain.Entities.Attachments;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Command.Profiles;
using System.Web;
using System.IO;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class AttachmentController : BaseController
    {
        private readonly IAttachmentService _attachmentService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly IProfilesService _profilesService;

        public AttachmentController(IAttachmentService attachmentService,
                                    IProfilesService profilesService,
                                    ISystemFeatureService systemFeatureService)
        {
            _attachmentService = attachmentService;
            _systemFeatureService = systemFeatureService;
            _profilesService = profilesService;
        }

        private string SystemFeatureID = "110";

        public FileResult Download(string attachmentID)
        {
            var model = new AttachmentModel();

            Result<Attachment> Attachment = _attachmentService.GetByID(Convert.ToInt32(attachmentID));

            model = Attachment.Value.ToModel();

            DirectoryInfo dirInfo = new DirectoryInfo(model.PathFile);

            string fileName = dirInfo.Name;

            return File(model.PathFile, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new AttachmentModel();

            var systemFeatures = _systemFeatureService.GetAll();

            model.SearchLoadSystemFeature = systemFeatures.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AttachmentModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar um anexo!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        model.SystemFeatureID = "110";

                        model.PathFile = path;

                        model.SizeFile = size;

                    }
                    var command = MaintenanceAttachmentCommand(model);

                    _attachmentService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "Attachment");
                }

                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Erro ao cadastrar registro!"));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, AttachmentModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registros de anexos!");

                return Json(gridModel);
            }
            else
            {
                var attachments = _attachmentService.GetAll(new FilterAttachmentCommand
                {
                    FileName = model.SearchFileName,
                    Description = model.SearchDescription,
                    RecordID = model.SearchRecordID,
                    SystemFeatureID = model.SearchSystemFeatureID,
                    CreatedByID = model.SearchCreatedByID

                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = attachments.Select(x =>
                    {
                        var attachmentModel = x.ToModel();

                        return attachmentModel;
                    }),
                    Total = attachments.TotalCount
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        public ActionResult GetAllByRecordID(DataSourceRequest request, string recordID, string systemFeatureID)
        {
            var attachments = _attachmentService.GetAll(new FilterAttachmentCommand
            {
                RecordID = recordID,
                SystemFeatureID = systemFeatureID,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = attachments.Select(x =>
                {
                    var attachmentModel = x.ToModel();

                    return attachmentModel;
                }),
                Total = attachments.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new AttachmentModel();

            var systemFeatures = _systemFeatureService.GetAll();
            model.LoadSystemFeature = systemFeatures.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
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

        public ActionResult GetByID(int attachmentID, string ActionName)
        {
            var model = new AttachmentModel();

            Result<Attachment> Attachment = _attachmentService.GetByID(attachmentID);

            if (Attachment.IsSuccess)
            {
                model = Attachment.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var systemFeatures = _systemFeatureService.GetAll();

                    model.LoadSystemFeature = systemFeatures.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }
            }

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int AttachmentID, string sourceController)
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
                    WarningNotification("Você não tem permissão para excluir um anexo!");

                    return RedirectToAction("Index");
                }

                if (AttachmentID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));

                    return Redirect("Index");
                }
                var model = new AttachmentModel();

                Result<Attachment> attachment = _attachmentService.GetByID(AttachmentID);

                if (attachment.IsSuccess)
                {
                    model = attachment.Value.ToModel();

                    _attachmentService.Delete(model.AttachmentID);

                    var file = new FileInfo(model.PathFile);

                    file.Delete();

                    SuccessNotification(string.Format("Registro excluído com sucesso!"));

                    if (!string.IsNullOrEmpty(sourceController))
                    {
                        return RedirectToAction("Index", sourceController);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                WarningNotification("Erro ao excluir o arquivo, tente novamente.");

                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(AttachmentModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para atualizar um anexo!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var fileName = Path.GetFileName(file.FileName);

                        var path = Path.Combine(newPath, fileName);

                        var size = file.ContentLength / 1024;

                        file.SaveAs(path);

                        model.SystemFeatureID = "110";

                        model.PathFile = path;

                        model.SizeFile = Convert.ToString(size) + "KB";
                    }

                    var command = MaintenanceAttachmentCommand(model);

                    _attachmentService.Update(command);

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