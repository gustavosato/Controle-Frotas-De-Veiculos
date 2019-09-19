using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.PipelineEvents;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.PipelineEvents;
using Lean.Test.Cloud.Domain.Entities.PipelineEvents;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;
using Lean.Test.Cloud.MVC.Models.Historicals;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.MVC.Models.Pipelines;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class PipelineEventController : BaseController
    {
        private readonly IPipelineEventService _pipelineEventService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IProfilesService _profilesService;
        private readonly IHistoricalService _historicalService;
        private readonly IPipelineService _pipelineService;

        public PipelineEventController(IPipelineEventService pipelineEventService,
                                IParameterValueService parameterValueService,
                                IHistoricalService historicalService,
                                IProfilesService profilesService,
                                IUserService userService,
                                IPipelineService pipelineService)
        {
            _pipelineEventService = pipelineEventService;
            _parameterValueService = parameterValueService;
            _historicalService = historicalService;
            _profilesService = profilesService;
            _userService = userService;
            _pipelineService = pipelineService;
        }

        private string SystemFeatureID = "322";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new PipelineEventModel();

            var type = _parameterValueService.GetAllByParameterID("322300");
            var nextStep = _parameterValueService.GetAllByParameterID("322301");
            var oportunity = _pipelineService.GetAllCodeByCustomerID("0");
            var users = _userService.GetAll(0);
            
            model.SearchLoadType = type.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadNextStep = nextStep.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadOportunity = oportunity.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();
            model.SearchLoadCreateds = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(PipelineEventModel model, string sourceController)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowAdd = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para adicionar um evento em Pipeline!");

                return RedirectToAction("Index");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.OportunityID == "0") 
                    {
                        WarningNotification(string.Format("Não foi possível realizar registro!"));

                        if (sourceController == "Pipeline")
                        {
                            return RedirectToAction("Index", "Pipeline");
                        }
                        return View();
                    }
                    var command = MaintenancePipelineEventCommand(model);

                    _pipelineEventService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso!"));

                    if (sourceController == "Pipeline")
                    {
                        return RedirectToAction("Index", "Pipeline");
                    }
                    return RedirectToAction("Index", "PipelineEvent");
                }

                ErrorNotification(string.Format("Não foi possível realizar registro, campo 'Descrição' é obrigatório e não foi preenchido!"));

                if (sourceController == "Pipeline")
                {
                    return RedirectToAction("Index", "Pipeline");
                }
                return RedirectToAction("Index", "PipelineEvent");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Erro ao realizar registro!"));

                if (sourceController == "Pipeline")
                {
                    return RedirectToAction("Index", "Pipeline");
                }
                      return RedirectToAction("Index", "PipelineEvent");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, PipelineEventModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registro em Eventos Pipeline!");

                return Json(gridModel);
            }
            else
            {
                var pipelineEvents = _pipelineEventService.GetAll(new FilterPipelineEventCommand
                {
                    RegisterDate = model.SearchRegisterDate,
                    TypeID = model.SearchTypeID,
                    NextStepID = model.SearchNextStepID,
                    OportunityID = model.SearchOportunityID,
                    CreatedBy = model.SearchCreatedID
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = pipelineEvents.Select(x =>
                    {
                        var pipelineEventsModel = x.ToModel();

                        return pipelineEventsModel;
                    }),
                    Total = pipelineEvents.TotalCount
                };

                return Json(gridModel);
            }
        }
        public ActionResult GetAllByOportunityID(DataSourceRequest request, string recordID)
        {
            var pipelineEvents = _pipelineEventService.GetAll(new FilterPipelineEventCommand
            {
                OportunityID = recordID,
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = pipelineEvents.Select(x =>
                {
                    var pipelineEventsModel = x.ToModel();

                    return pipelineEventsModel;
                }),

                Total = pipelineEvents.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New(string oportunityID)
        {
            var model = new PipelineEventModel();

            var type = _parameterValueService.GetAllByParameterID("322300");
            var nextStep = _parameterValueService.GetAllByParameterID("322301");
            var oportunity = _pipelineService.GetAllCodeByCustomerID("0");
            var users = _userService.GetAll(0);

            model.LoadType = type.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.LoadNextStep = nextStep.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            
            model.LoadOportunity = oportunity.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();

            model.LoadCreateds = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            if (oportunityID == null) oportunityID = "0";

            model.OportunityID = oportunityID;

            //model.RegisterDate = Convert.ToDateTime(DateTime.Today).ToString("dd/MM/yyyy");

            //model.TargetDate = Convert.ToDateTime(DateTime.Today.AddDays(-7)).ToString("dd/MM/yyyy");

            model.CreatedByID = Convert.ToString(Session["userID"]);

            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

           return PartialView("Maintenance", model);
        }

        private MaintenancePipelineEventCommand MaintenancePipelineEventCommand(PipelineEventModel model)
        {
            MaintenancePipelineEventCommand command = new MaintenancePipelineEventCommand();

            command.SaleEventID = model.SaleEventID;
            command.RegisterDate = model.RegisterDate;
            command.TypeID = model.TypeID;
            command.NextStepID = model.NextStepID;
            command.TargetDate = model.TargetDate;
            command.Description = model.Description;
            command.OportunityID = model.OportunityID;            
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int saleEventID, string ActionName)
        {
            var model = new PipelineEventModel();

            Result<PipelineEvent> pipelineEvent = _pipelineEventService.GetByID(saleEventID);

            if (pipelineEvent.IsSuccess)
            {
                model = pipelineEvent.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var type = _parameterValueService.GetAllByParameterID("322300");
                    var nextStep = _parameterValueService.GetAllByParameterID("322301");
                    var oportunity = _pipelineService.GetAllCodeByCustomerID("0");
                    var users = _userService.GetAll(Convert.ToInt32(model.CreatedByID));
                                                         
                    model.LoadType = type.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadNextStep = nextStep.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadOportunity = oportunity.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();
                    model.LoadOportunity = oportunity.Select(x => new SelectListItem() { Text = x.oportunityCode.ToString(), Value = x.oportunityID.ToString() }).ToList();
                    model.LoadCreateds = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "PipelineEvent");
        }

        public ActionResult Delete(int saleEventID, string sourceController)
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
                    WarningNotification("Você não tem permissão para excluir um evento em Pipeline !");

                    return RedirectToAction("Index");
                }

                if (saleEventID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir registro! "));

                    return Redirect("Index");
                }

                var model = new PipelineEventModel();

                Result<PipelineEvent> pipelineEvent = _pipelineEventService.GetByID(saleEventID);

                if (pipelineEvent.IsSuccess)
                {
                    model = pipelineEvent.Value.ToModel();

                    if (model.CreatedByID != Convert.ToString(Session["userID"]))
                    {
                        WarningNotification(string.Format("Não é permitido excluir o registro de outro usuário"));

                        if (sourceController == "Pipeline")
                        {
                            return RedirectToAction("Index", "Pipeline");
                        }
                        return Redirect("Index");
                    }

                    _pipelineEventService.Delete(model.SaleEventID);

                    _historicalService.Delete(SystemFeatureID, saleEventID);

                    SuccessNotification(string.Format("Evento excluido com sucesso! Evento: {0}", model.Description));

                    if (sourceController == "Pipeline")
                    {
                        return RedirectToAction("Index", "Pipeline");
                    }
                    return RedirectToAction("Index");
                }
                if (sourceController == "Pipeline")
                {
                    return RedirectToAction("Index", "Pipeline");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                WarningNotification(string.Format("Erro ao excluir registro!"));

                if (sourceController == "Pipeline")
                {
                    return RedirectToAction("Index", "Pipeline");
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(PipelineEventModel model, string sourceController)
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
                    WarningNotification("Você não tem permissão para atualizar um evento em Pipeline!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    //historical
                    Historical(model);

                    if (model.CreatedByID != Convert.ToString(Session["userID"]))
                    {
                        WarningNotification(string.Format("Não é permitido atualizar o registro de outro usuário"));

                        if (sourceController == "Pipeline")
                        {
                            return RedirectToAction("Index", "Pipeline");
                        }
                        return Redirect("Index");
                    }

                    var command = MaintenancePipelineEventCommand(model);

                    _pipelineEventService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso!"));

                    if (sourceController == "Pipeline")
                    {
                        return RedirectToAction("Index", "Pipeline");
                    }
                    return RedirectToAction("Index");
                }

                ErrorNotification("Não foi possível atualizar registro!");

                if (sourceController == "Pipeline")
                {
                    return RedirectToAction("Index", "Pipeline");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Erro ao atualizar registro!"));

                if (sourceController == "Pipeline")
                {
                    return RedirectToAction("Index", "Pipeline");
                }
                return RedirectToAction("Index");
            }
        }

            //Realizando histórico de alterações 
            private void Historical(PipelineEventModel model)
            {
                var command = new PipelineEventModel();

                var modelHistorical = new HistoricalModel();

                var LocalCommand = _pipelineEventService.GetByID(model.SaleEventID);

                command = LocalCommand.Value.ToModel();

                if (command.OportunityID != model.OportunityID)
                {
                    string commandOportunityID = _pipelineService.GetOportunityCodeByID(Convert.ToInt32(command.OportunityID));

                    string modelOportunityID = _pipelineService.GetOportunityCodeByID(Convert.ToInt32(model.OportunityID));

                    AddHistorical(commandOportunityID, modelOportunityID, "Oportunidade", model.SaleEventID.ToString());
                }

                if (command.NextStepID != model.NextStepID) AddHistorical(command.NextStepID, model.NextStepID, "Próximo Passo", model.SaleEventID.ToString(), true);
                if (command.TypeID != model.TypeID) AddHistorical(command.TypeID, model.TypeID, "Tipo de Contrato", model.SaleEventID.ToString(), true);
                if (command.TargetDate != model.TargetDate) AddHistorical(command.TargetDate, model.TargetDate, "Data Alvo", model.SaleEventID.ToString());
                if (command.RegisterDate != model.RegisterDate) AddHistorical(command.RegisterDate, model.RegisterDate, "Data do Evento", model.SaleEventID.ToString());
                //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.SaleEventID.ToString());

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
