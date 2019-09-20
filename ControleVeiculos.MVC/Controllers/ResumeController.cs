using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;       
using ControleVeiculos.MVC.Models.Resumes;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Resumes;
using ControleVeiculos.Domain.Entities.Resumes;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using System.Web;
using System.IO;
using ControleVeiculos.MVC.Models.Attachments;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain.Command.Attachments;
using ControleVeiculos.MVC.Models.Vacancies;
using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Command.ResumeVacancies;

namespace ControleVeiculos.MVC.Controllers
{
    public class ResumeController : BaseController
    {
        private readonly IResumeService _resumeService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IProfilesService _profilesService;
        private readonly IUserService _userService;
        private readonly IAttachmentService _attachmentService;
        private readonly IResumeVacancieService _resumeVacancieService;


        public ResumeController(IResumeService resumeService, 
                                IParameterValueService parameterValueService,
                                IProfilesService profilesService,
                                IAttachmentService attachmentService,
                                IResumeVacancieService resumeVacancieService,
                                IUserService userService)
                       
        {
            _resumeService = resumeService;
            _parameterValueService = parameterValueService;
            _profilesService = profilesService;
            _attachmentService = attachmentService;
            _userService = userService;
            _resumeVacancieService = resumeVacancieService;

        }

        private string SystemFeatureID = "317";
        
        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new ResumeModel();

            var function = _parameterValueService.GetAllByParameterID("100100");
            var functionLevel = _parameterValueService.GetAllByParameterID("100101");
            var statusRh = _parameterValueService.GetAllByParameterID("317301");
            var statusManager = _parameterValueService.GetAllByParameterID("317302");
            var statusClient = _parameterValueService.GetAllByParameterID("317303");
            var contractType = _parameterValueService.GetAllByParameterID("316300");


            model.SearchLoadFunction = function.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadFunctionLevel = functionLevel.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadStatusRh = statusRh.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadStatusManager = statusManager.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadStatusClient = statusClient.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadContractType = contractType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ResumeModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar um currículo!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                   var command = MaintenanceResumeCommand(model);

                    string recordID = _resumeService.Add(command);

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
                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index");
                }
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ResumeModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registros de currículos!");

                return Json(gridModel);
            }
            else
            {
                var resumes = _resumeService.GetAll(new FilterResumeCommand
                {
                    Summary = model.SearchSummary,
                    FunctionID = model.SearchFunctionID,
                    TimeExperience = model.SearchTimeExperience,
                    FunctionLevelID = model.SearchFunctionLevelID,
                    StatusRhID = model.SearchStatusRhID,
                    StatusManagerID = model.SearchStatusManagerID,
                    StatusClientID = model.SearchStatusClientID,
                    ContractTypeID = model.SearchContractTypeID,

                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = resumes.Select(x =>
                    {
                        var resumesModel = x.ToModel();

                        return resumesModel;
                    }),
                    Total = resumes.TotalCount
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        public ActionResult GetAllAssociateResumeByVacancieID(DataSourceRequest request, VacancieModel model)
        {
            if(Session["resumeAssociateID"] == null)
            {
                model.ResumeID = "0";
                //return View();
            }
            else
            { 
                model.ResumeID = Session["resumeAssociateID"].ToString();
            }
            var resumes = _resumeVacancieService.GetAllAssociateResumeByVacancieID(new FilterVacancieCommand
            {
                Summary = model.SearchSummary,
                ResumeID = model.ResumeID,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = resumes.Select(x =>
                {
                    var resumesModel = x.ToModel();

                    return resumesModel;
                }),
                Total = resumes.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GetAllNoAssociateResumeByVacancieID(DataSourceRequest request, VacancieModel model)
        {
            if (Session["resumeAssociateID"] == null)
            {
                model.ResumeID = "0";
                //return View();
            }
            else
            {
                model.ResumeID = Session["resumeAssociateID"].ToString();
            }
            var resumes = _resumeVacancieService.GetAllNoAssociateResumeByVacancieID(new FilterVacancieCommand
            {
                Summary = model.SearchSummary,
                ResumeID = model.ResumeID

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = resumes.Select(x =>
                {
                    var resumesModel = x.ToModel();

                    return resumesModel;
                }),
                Total = resumes.TotalCount
            };

            return Json(gridModel);
        }


        public ActionResult New()
        {
            var model = new ResumeModel();

            var gender = _parameterValueService.GetAllByParameterID("317300");
            var function = _parameterValueService.GetAllByParameterID("100100");
            var functionLevel = _parameterValueService.GetAllByParameterID("100101");
            var statusRh = _parameterValueService.GetAllByParameterID("317301");
            var statusManager = _parameterValueService.GetAllByParameterID("317302");
            var statusClient = _parameterValueService.GetAllByParameterID("317303");
            var contractType = _parameterValueService.GetAllByParameterID("316300");
            var maritalStatus = _parameterValueService.GetAllByParameterID("317304");


            model.LoadGender = gender.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadFunction = function.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadFunctionLevel = functionLevel.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadStatusRh = statusRh.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadStatusManager = statusManager.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadStatusClient = statusClient.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadContractType = contractType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadMaritalStatus = maritalStatus.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        private MaintenanceResumeCommand MaintenanceResumeCommand(ResumeModel model)
        {
            MaintenanceResumeCommand command = new MaintenanceResumeCommand();
                            
            command.ResumeID = model.ResumeID;
            command.Summary = model.Summary;
            command.FunctionID = model.FunctionID;
            command.Description = model.Description;
            command.GenderID = model.GenderID;
            command.Age = model.Age;
            command.TimeExperience = model.TimeExperience;
            command.FunctionLevelID = model.FunctionLevelID;
            command.StatusRhID = model.StatusRhID;
            command.ApprovedDateRh = model.ApprovedDateRh;
            command.StatusManagerID = model.StatusManagerID;
            command.ApprovedDateManager = model.ApprovedDateManager;
            command.StatusClientID = model.StatusClientID;
            command.ApprovedDateClient = model.ApprovedDateClient;
            command.ExpectedSalary = model.ExpectedSalary;
            command.ContractTypeID = model.ContractTypeID;
            command.IsEmployee = model.IsEmployee;
            command.WillingToTravel = model.WillingToTravel;
            command.MaritalStatusID = model.MaritalStatusID;
            command.HaveChildren = model.HaveChildren;
            command.IsSmoker = model.IsSmoker;
            command.AvailabilityToStart = model.AvailabilityToStart;
            command.Observation = model.Observation;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            command.ResultRh = model.ResultRh;
            command.ResultManager = model.ResultManager;
            command.ResultClient = model.ResultClient;

            return command;
        }

        public ActionResult GetByID(int resumeID, string ActionName)
        {
            var model = new ResumeModel();

            Result<Resume> resume = _resumeService.GetByID(resumeID);

            if (resume.IsSuccess)
            {
                model = resume.Value.ToModel();
                
                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    Session["resumeAssociateID"] = resumeID;
                    var gender = _parameterValueService.GetAllByParameterID("317300");
                    var function = _parameterValueService.GetAllByParameterID("100100");
                    var functionLevel = _parameterValueService.GetAllByParameterID("100101");
                    var statusRh = _parameterValueService.GetAllByParameterID("317301");
                    var statusManager = _parameterValueService.GetAllByParameterID("317302");
                    var statusClient = _parameterValueService.GetAllByParameterID("317303");
                    var contractType = _parameterValueService.GetAllByParameterID("316300");
                    var maritalStatus = _parameterValueService.GetAllByParameterID("317304");


                    model.LoadMaritalStatus = maritalStatus.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadGender = gender.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadFunction = function.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadFunctionLevel = functionLevel.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadStatusRh = statusRh.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadStatusManager = statusManager.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadStatusClient = statusClient.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadContractType = contractType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "Resume");
        }

        
        public ActionResult Delete(int resumeID)
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
                    WarningNotification("Você não tem permissão para excluir um currículo!");

                    return RedirectToAction("Index");
                }
                if (resumeID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new ResumeModel();

                Result<Resume> resume = _resumeService.GetByID(resumeID);

                if (resume.IsSuccess)
                {
                    model = resume.Value.ToModel();

                    _resumeService.Delete(model.ResumeID);

                    SuccessNotification(string.Format("Registro excluído com sucesso! "));

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
        

        [HttpPost]
        public ActionResult Update(ResumeModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para atualizar um currículo!");

                    return RedirectToAction("Index");
                }
                if (ModelState.IsValid)
                {

                    var command = MaintenanceResumeCommand(model);

                    _resumeService.Update(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.ResumeID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = "";
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.ResumeID.ToString();
                        attachmentModel.SizeFile = size;
                        attachmentModel.SystemFeatureID = SystemFeatureID;
                        attachmentModel.CreatedByID = Convert.ToString(Session["userID"]);
                        attachmentModel.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                        var localCommand = MaintenanceAttachmentCommand(attachmentModel);

                        _attachmentService.Add(localCommand);
                    }

                    SuccessNotification(string.Format("Registro atualizado com sucesso! "));

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

    }
}