using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Demands;
using System;
using System.Collections.Generic;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Demands;
using Lean.Test.Cloud.Domain.Entities.Demands;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;
using Lean.Test.Cloud.MVC.Models.Users;
using Lean.Test.Cloud.Domain.Command.Users;
using Lean.Test.Cloud.Domain.Command.DemandsUsers;
using System.Web;
using System.IO;
using Lean.Test.Cloud.MVC.Models.Attachments;
using Lean.Test.Cloud.Domain.Command.Attachments;
using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.MVC.Models.Historicals;
using System.Globalization;
using Lean.Test.Cloud.Domain.Command.Profiles;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class DemandController : BaseController
    {
        private readonly IDemandService _demandService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IExportManagerService _exportManagerService;
        private readonly IUserService _userService;
        private readonly IDemandUserService _demandUserService;
        private readonly ICustomerService _customerService;
        private readonly IContactService _contactService;
        private readonly IPipelineService _pipelineService;
        private readonly IAttachmentService _attachmentService;
        private readonly IHistoricalService _historicalService;
        private readonly IProfilesService _profilesService;

        public DemandController(IDemandService demandService,
                                IExportManagerService exportManagerService,
                                IParameterValueService parameterValueService,
                                IUserService userService,
                                IDemandUserService demandUserService,
                                ICustomerService customerService,
                                IContactService contactService,
                                IHistoricalService historicalService,
                                IAttachmentService attachmentService,
                                IProfilesService profilesService,
                                IPipelineService pipelineService)
        {
            _demandService = demandService;
            _parameterValueService = parameterValueService;
            _exportManagerService = exportManagerService;
            _userService = userService;
            _demandUserService = demandUserService;
            _customerService = customerService;
            _contactService = contactService;
            _attachmentService = attachmentService;
            _pipelineService = pipelineService;
            _historicalService = historicalService;
            _profilesService = profilesService;
        }

        private string SystemFeatureID = "200";

        public JsonResult GetServiceOrder(string customerID)
        {
            var pipelines = _pipelineService.GetAllCodeByCustomerID(customerID);
            return Json(pipelines.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList());
        }

        public ActionResult New()
        {
            var model = new DemandModel();

            var status = _parameterValueService.GetAllByParameterID("200200");
            var types = _parameterValueService.GetAllByParameterID("200201");
            var services = _parameterValueService.GetAllByParameterID("200202");
            var users = _userService.GetAll(0);
            var contacts = _contactService.GetAll(0, Convert.ToInt32(Session["customerID"]));
            var pipelines = _pipelineService.GetAllCodeByCustomerID(Session["customerID"].ToString());

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadServices = services.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadAssingToTarget = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadResponsible = contacts.Select(x => new SelectListItem() { Text = x.contactName.ToString(), Value = x.contactID.ToString() }).ToList();
            model.LoadOportunity = pipelines.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();

            model.IsActive = false;
            return PartialView("Maintenance", model);
        }

        [HttpPost]
        public ActionResult Add(DemandModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar uma demanda!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceCommand(model);

                    int userID = Convert.ToInt32(Session["userID"]);

                    command.CustomerID = Session["customerID"].ToString();

                    string recordID = _demandService.Add(userID, command);

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

                        SuccessNotification(string.Format("Registro realizado com sucesso!"));

                        return RedirectToAction("Index");
                    }
                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index");
                }
                ErrorNotification("Não foi possível realizar registro!");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);

                throw;
            }
        }

        public ActionResult Delete(int demandID)
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
                    WarningNotification("Você não tem permissão para excluir uma demanda!");

                    return RedirectToAction("Index");
                }

                if (demandID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir registro! "));
                    return Redirect("Index");
                }
                var model = new DemandModel();
                Result<Demand> demand = _demandService.GetByID(demandID);
                if (demand.IsSuccess)
                {
                    model = demand.Value.ToModel();

                    _demandService.Delete(model.DemandID);

                    _historicalService.Delete(SystemFeatureID, demandID);

                    _attachmentService.Delete(SystemFeatureID, demandID);

                    SuccessNotification(string.Format("Registro excluído com sucesso! "));
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Existem outros registros associados a essa demanda, é necessário excluí-los."));
                return RedirectToAction("Index");
            }
        }

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new DemandModel();

            var status = _parameterValueService.GetAllByParameterID("200200");
            var types = _parameterValueService.GetAllByParameterID("200201");
            var services = _parameterValueService.GetAllByParameterID("200202");
            var users = _userService.GetAll(0);
            var contacts = _contactService.GetAll(0, Convert.ToInt32(Session["customerID"]));
            var createdBy = _userService.GetAll(0);

            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadServices = services.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadAssingToTarget = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadResponsible = contacts.Select(x => new SelectListItem() { Text = x.contactName.ToString(), Value = x.contactID.ToString() }).ToList();
            model.SearchLoadCreateds = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            return View(model);
        }

        public ActionResult Report()
        {
            var model = new DemandModel();

            model.SearchStartDateReport = DateTime.Today.ToString("dd/MM/yyyy");

            DateTime date = DateTime.ParseExact(model.SearchStartDateReport, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var startDate = new DateTime(date.Year, date.Month, 1);

            var endDate = startDate.AddMonths(1).AddDays(-1);

            model.SearchStartDateReport = Convert.ToDateTime(startDate).ToString("dd/MM/yyyy");

            model.SearchEndDateReport = Convert.ToDateTime(endDate).ToString("dd/MM/yyyy");

            return PartialView("Report", model);
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, DemandModel model)
        {
            string customerID = Convert.ToString(Session["customerID"]);
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowView = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para visualizar as demandas!");

                customerID = "0";
            }

            var demands = _demandService.GetAll(customerID, new FilterDemandCommand
            {
                DemandName = model.SearchDemandName,
                DemandCode = model.SearchDemandCode,
                ExternalCode = model.SearchExternalCode,
                StatusID = model.SearchStatusID,
                PlanningStartDate = model.SearchPlanningStartDate,
                PlanningEndDate = model.SearchPlanningEndDate,
                AssignToTargetID = model.SearchAssignToTargetID,
                ResponsibleID = model.SearchResponsibleID,
                ServiceID = model.SearchServiceID,
                TypeID = model.SearchTypeID
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = demands.Select(x =>
                {
                    var demandModel = x.ToModel();
                    return demandModel;
                }),
                Total = demands.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult GetAllByTimeReleaseByContact(DataSourceRequest request, DemandModel model)
        {
            string customerID = Convert.ToString(Session["customerID"]);

            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowReportView = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para visualizar o relatório!");

                customerID = "0";
            }
            if (Session["userID"] == null)
            {
                return PartialView("Report");
            }

            var demands = _demandService.GetAllByTimeReleaseByContact(customerID, new FilterDemandCommand
            {
                RegisterDateFrom = model.SearchStartDateReport,
                RegisterDateTo = model.SearchEndDateReport,
                ResponsibleID = model.SearchResponsibleID,
                CreatedByID = model.SearchCreatedByID
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = demands.Select(x =>
                {
                    var timeReleasesModel = x.ToModel();

                    return timeReleasesModel;
                }),
                Total = demands.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult GetByID(int demandID, string ActionName)
        {
            var model = new DemandModel();


            Result<Demand> demand = _demandService.GetByID(demandID);

            if (demand.IsSuccess)
            {
                model = demand.Value.ToModel();

                var users = _userService.GetAll(Convert.ToInt32(model.AssignToTargetID));
                model.LoadAssingToTarget = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();


                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else
                {
                    var Localmodel = new DemandModel();

                    var status = _parameterValueService.GetAllByParameterID("200200");
                    var types = _parameterValueService.GetAllByParameterID("200201");
                    var services = _parameterValueService.GetAllByParameterID("200202");
                    var contacts = _contactService.GetAll(Convert.ToInt32(model.ResponsibleID), Convert.ToInt32(model.CustomerID));
                    var pipelines = _pipelineService.GetAllCodeByCustomerID(Session["customerID"].ToString());


                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadServices = services.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadResponsible = contacts.Select(x => new SelectListItem() { Text = x.contactName.ToString(), Value = x.contactID.ToString() }).ToList();
                    model.LoadOportunity = pipelines.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();

                    model.TotalTime = _demandService.GetTotalHoursByDemandID(model.DemandID.ToString());

                    model.PlanningStartDate = model.PlanningStartDate;
                    model.PlanningEndDate = model.PlanningEndDate;
                    model.CreationDate = model.CreationDate;
                    model.TotalEffort = Convert.ToString(Convert.ToInt32(model.ManagementEffort) + Convert.ToInt32(model.PlanningEffort) + Convert.ToInt32(model.ExecutionEffort));

                    var ServiceOrdem = _pipelineService.GetAllCodeByCustomerID(model.CustomerID);
                    model.LoadOportunity = ServiceOrdem.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();

                    model.Descriptions = Server.HtmlDecode(model.Descriptions);
                    model.Scope = Server.HtmlDecode(model.Scope);

                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "Demand");
        }

        [HttpPost]
        public ActionResult Update(DemandModel model, HttpPostedFileBase file)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterProfileCommand
                {
                    AllowView = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para atualizar uma demanda!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    //historical
                    Historical(model);

                    //update demand
                    if (model.StatusID == "200200206")
                    {
                        model.IsActive = false;
                    }
                    var command = MaintenanceCommand(model);

                    _demandService.Update(command);

                    //attachment file
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.DemandID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = "";
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.DemandID.ToString();
                        attachmentModel.SizeFile = size;
                        attachmentModel.SystemFeatureID = SystemFeatureID;
                        attachmentModel.CreatedByID = Convert.ToString(Session["userID"]);
                        attachmentModel.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                        var localCommand = MaintenanceAttachmentCommand(attachmentModel);

                        _attachmentService.Add(localCommand);
                    }

                    SuccessNotification(string.Format("Registro alterado com sucesso! "));

                    return RedirectToAction("Index");
                }

                ErrorNotification("Não foi possível salvar registro!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult ExportXmlAll(DemandModel model)
        {
            try
            {
                string userID = Session["userID"].ToString();

                var demands = _demandService.GetAll(userID, new FilterDemandCommand
                {
                    DemandName = model.SearchDemandName,
                    DemandCode = model.SearchDemandCode,
                    ExternalCode = model.SearchExternalCode,
                    StatusID = model.SearchStatusID,
                    ServiceID = model.SearchServiceID,
                    TypeID = model.SearchTypeID
                });

                string xmlDemands = _exportManagerService.ExportDemandXml(demands);

                string fileName = string.Format("DemandsList-{0}.xml", Guid.NewGuid().ToString());

                return new XmlDownloadResult(xmlDemands, fileName);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc.Message);

                return RedirectToAction("Index");
            }
        }

        private MaintenanceDemandCommand MaintenanceCommand(DemandModel model)
        {
            MaintenanceDemandCommand command = new MaintenanceDemandCommand();

            command.DemandID = model.DemandID;
            command.DemandName = model.DemandName;
            command.TypeID = model.TypeID;
            command.StatusID = model.StatusID;
            command.ServiceID = model.ServiceID;
            command.Scope = model.Scope;
            command.ExternalCode = model.ExternalCode;
            command.DemandCode = model.DemandCode;
            command.ResponsibleID = model.ResponsibleID;
            command.AssignToTargetID = model.AssignToTargetID;
            command.PlanningStartDate = model.PlanningStartDate;
            command.PlanningEndDate = model.PlanningEndDate;
            command.ManagementEffort = model.ManagementEffort;
            command.PlanningEffort = model.PlanningEffort;
            command.ExecutionEffort = model.ExecutionEffort;
            command.Description = model.Descriptions;
            command.CustomerID = model.CustomerID;
            command.OportunityID = model.OportunityID;
            command.IsActive = model.IsActive;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult UserAssociate(int demandID)
        {
            var model = new DemandModel();
            model.DemandID = demandID;
            Session["demandAssociateID"] = demandID;
            return PartialView("UserAssociate");
        }

        [HttpPost]
        public ActionResult GetAllAssociateUserByDemandID(DataSourceRequest request, UserModel model)
        {

            model.DemandID = Session["demandAssociateID"].ToString();

            var users = _userService.GetAllAssociateUserByDemandID(new FilterUserCommand
            {
                UserName = model.SearchUserName,
                DemandID = model.DemandID
            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = users.Select(x =>
                {
                    var usersModel = x.ToModel();

                    return usersModel;
                }),
                Total = users.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GetAllNoAssociateUserByDemandID(DataSourceRequest request, UserModel model)
        {
            model.DemandID = Session["demandAssociateID"].ToString();

            var users = _userService.GetAllNoAssociateUserByDemandID(new FilterUserCommand
            {
                UserName = model.SearchUserName,
                DemandID = model.DemandID
            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = users.Select(x =>
                {
                    var usersModel = x.ToModel();

                    return usersModel;
                }),
                Total = users.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult DisassociateUser(int userID)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para desassociar usuários!");

                return View();
            }
            else
            {
                _demandUserService.Delete(Convert.ToInt16(Session["demandAssociateID"]), userID);

                return View();
            }
        }

        public ActionResult AssociateUser(int userID)
        {
            var command = new MaintenanceDemandUserCommand();
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para associar um usuário para uma demanda!");

                return View();
            }
            else
            {
                command = new MaintenanceDemandUserCommand();

                command.DemandID = Convert.ToInt16(Session["demandAssociateID"]);
                command.UserID = userID;

                _demandUserService.Add(command);

                return View();
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

        public ActionResult StatusChange(int demandID)
        {
            try
            {
                if (_profilesService.GetAllow(new FilterProfileCommand
                {
                    AllowChangeStatus = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para alterar o status de ativação da demanda!");

                    return RedirectToAction("Index");
                }
                Result<Demand> demand = _demandService.GetByID(demandID);

                DemandModel model = demand.Value.ToModel();

                var command = MaintenanceCommand(model);

                if (command.IsActive == true)
                {
                    command.IsActive = false;
                }
                else
                {
                    command.IsActive = true;
                }
                _demandService.Update(command);

                Historical(model);

                SuccessNotification(string.Format("Registro alterado com sucesso! "));

                return View();

            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);
                throw;
            }
        }

        private void Historical(DemandModel model)
        {
            var command = new DemandModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _demandService.GetByID(model.DemandID);

            command = LocalCommand.Value.ToModel();

            if (command.DemandName != model.DemandName) AddHistorical(command.DemandName, model.DemandName, "Demanda", model.DemandID.ToString());
            if (command.TypeID != model.TypeID) AddHistorical(command.TypeID, model.TypeID, "Tipo de Solicitação", model.DemandID.ToString(), true);
            if (command.StatusID != model.StatusID) AddHistorical(command.StatusID, model.StatusID, "Status", model.DemandID.ToString(), true);
            if (command.ServiceID != model.ServiceID) AddHistorical(command.ServiceID, model.ServiceID, "Service", model.DemandID.ToString(), true);
            if (command.ExternalCode != model.ExternalCode) AddHistorical(command.ExternalCode, model.ExternalCode, "Código Externo", model.DemandID.ToString());

            //Verificar Histórico do campo ativar (bool)
            if (command.IsActive != model.IsActive)
            {
                string status1;
                string status2;

                if (command.IsActive)
                {
                    status1 = "Inativo"; status2 = "Ativo";
                }
                else
                {
                    status1 = "Ativo"; status2 = "Inativo";
                }
                AddHistorical(status1, status2, "Status", model.DemandID.ToString());
            }

            if (command.ResponsibleID != model.ResponsibleID)
            {
                string commandResponsibleID = _contactService.GetContactNameByID(Convert.ToInt32(command.ResponsibleID));

                string modelResponsibleID = _contactService.GetContactNameByID(Convert.ToInt32(model.ResponsibleID));

                AddHistorical(commandResponsibleID, modelResponsibleID, "Responsável Externo", model.DemandID.ToString());
            }

            if (command.AssignToTargetID != model.AssignToTargetID)
            {
                string commandAssignToTargetID = _userService.GetUserNameByID(Convert.ToInt32(command.AssignToTargetID));

                string modelAssignToTargetID = _userService.GetUserNameByID(Convert.ToInt32(model.AssignToTargetID));

                AddHistorical(commandAssignToTargetID, modelAssignToTargetID, "Responsável Interno", model.DemandID.ToString());
            }

            if (command.PlanningStartDate != model.PlanningStartDate) AddHistorical(command.PlanningStartDate, model.PlanningStartDate, "Início Planejado", model.DemandID.ToString());
            if (command.PlanningEndDate != model.PlanningEndDate) AddHistorical(command.PlanningEndDate, model.PlanningEndDate, "Término Planejado", model.DemandID.ToString());
            if (command.ManagementEffort != model.ManagementEffort) AddHistorical(command.ManagementEffort, model.ManagementEffort, "Esforço de Gestão", model.DemandID.ToString());
            if (command.PlanningEffort != model.PlanningEffort) AddHistorical(command.PlanningEffort, model.PlanningEffort, "Esforço de Planejamento", model.DemandID.ToString());
            if (command.ExecutionEffort != model.ExecutionEffort) AddHistorical(command.ExecutionEffort, model.ExecutionEffort, "Esforço de Execução", model.DemandID.ToString());
            //if (command.Descriptions != model.Descriptions) AddHistorical(command.Descriptions, model.Descriptions, "Descrição", model.DemandID.ToString());

            if (command.CustomerID != model.CustomerID)
            {
                string commandCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.CustomerID));

                string modelCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(model.CustomerID));

                AddHistorical(commandCustomerID, modelCustomerID, "Empresa", model.DemandID.ToString());
            }
            if (command.OportunityID != model.OportunityID)
            {
                string commandOportunityID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.OportunityID));

                string modelOportunityID = _pipelineService.GetOportunityCodeByID(Convert.ToInt32(model.OportunityID));

                AddHistorical(commandOportunityID, modelOportunityID, "Ordem de Serviço", model.DemandID.ToString());
            }
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

        public ActionResult GetAllGantt(string customerID, DemandModel model)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowReportView = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para visualizar o relatório!");

                return RedirectToAction("Index");
            }

            var demands = _demandService.GetAll(customerID, new FilterDemandCommand
            {
                DemandName = model.SearchDemandName,
                DemandCode = model.SearchDemandCode,
                ExternalCode = model.SearchExternalCode,
                StatusID = model.SearchStatusID,
                PlanningStartDate = model.SearchPlanningStartDate,
                PlanningEndDate = model.SearchPlanningEndDate,
                AssignToTargetID = model.SearchAssignToTargetID,
                ResponsibleID = model.SearchResponsibleID,
                ServiceID = model.SearchServiceID,
                TypeID = model.SearchTypeID
            });

            return Json(demands);
        }

        public ActionResult Gantt()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new DemandModel();

            var status = _parameterValueService.GetAllByParameterID("200200");
            var types = _parameterValueService.GetAllByParameterID("200201");
            var services = _parameterValueService.GetAllByParameterID("200202");
            var users = _userService.GetAll(0);
            var contacts = _contactService.GetAll(0, Convert.ToInt32(Session["customerID"]));

            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadServices = services.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadAssingToTarget = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadResponsible = contacts.Select(x => new SelectListItem() { Text = x.contactName.ToString(), Value = x.contactID.ToString() }).ToList();

            return View(model);
        }
    }
}