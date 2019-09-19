using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;       
using Lean.Test.Cloud.MVC.Models.Pipelines;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Pipelines;
using Lean.Test.Cloud.Domain.Entities.Pipelines;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;
using System.Globalization;
using System.IO;
using Lean.Test.Cloud.MVC.Models.Attachments;
using Lean.Test.Cloud.Domain.Command.Attachments;
using System.Web;
using Lean.Test.Cloud.Domain.Command.Customers;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.MVC.Models.Historicals;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class PipelineController : BaseController
    {
        private readonly IPipelineService _pipelineService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly IProfilesService _profilesService;
        private readonly IHistoricalService _historicalService;
        private readonly IDemandService _demandService;

        private readonly IAttachmentService _attachmentService;

        public PipelineController(IPipelineService pipelineService,
                                IParameterValueService parameterValueService,
                                IUserService userService,
                                ICustomerService customerService,
                                IHistoricalService historicalService,
                                IProfilesService profilesService,
                                IAttachmentService attachmentService,
                                IDemandService demandService )
        {
            _pipelineService = pipelineService;
            _parameterValueService = parameterValueService;
            _userService = userService;
            _customerService = customerService;
            _attachmentService = attachmentService;
            _historicalService = historicalService;
            _profilesService = profilesService;
            _demandService = demandService;
        }

        private string SystemFeatureID = "320";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new PipelineModel();

            var customers = _customerService.GetAll(new FilterCustomerCommand { });

            var prioritys = _parameterValueService.GetAllByParameterID("320300");
            var fases = _parameterValueService.GetAllByParameterID("320301");
            var owners = _parameterValueService.GetAllByParameterID("31");
            var saleManagers = _parameterValueService.GetAllByParameterID("31");
            var preSales = _parameterValueService.GetAllByParameterID("31");
            var operationManagers = _parameterValueService.GetAllByParameterID("31");
            var types = _parameterValueService.GetAllByParameterID("320302");
            var costCenters = _parameterValueService.GetAllByParameterID("100103");
            var offers = _parameterValueService.GetAllByParameterID("320303");
            var status = _parameterValueService.GetAllByParameterID("320304");
            var users = _userService.GetAll(0);

            model.SearchLoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.SearchLoadPriority = prioritys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadFase = fases.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadOwner = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadSaleManager = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadPreSales = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadOperationManager = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadType = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadCostCenter = costCenters.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadOffer = offers.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(PipelineModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar um registro em Pipeline!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenancePipelineCommand(model);

                    string recordID = _pipelineService.Add(command);

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

                        return RedirectToAction("Index", "Pipeline");
                    }


                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "Pipeline");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, PipelineModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registro em Pipeline!");

                return Json(gridModel);
            }
            else
            {
                var pipelines = _pipelineService.GetAll(new FilterPipelineCommand
                {
                    CustomerID = model.SearchCustomerID,
                    PriorityID = model.SearchPriorityID,
                    FaseID = model.SearchFaseID,
                    OwnerID = model.SearchOwnerID,
                    SaleManagerID = model.SearchSaleManagerID,
                    PreSalesID = model.SearchPreSalesID,
                    OperationManagerID = model.SearchOperationManagerID,
                    TypeID = model.SearchTypeID,
                    CostCenterID = model.SearchCostCenterID,
                    OfferID = model.SearchOfferID,
                    StatusID = model.SearchStatusID

                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = pipelines.Select(x =>
                    {
                        var pipelinesModel = x.ToModel();

                        return pipelinesModel;
                    }),
                    Total = pipelines.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new PipelineModel();

            var customers = _customerService.GetAll(new FilterCustomerCommand { });

            var prioritys = _parameterValueService.GetAllByParameterID("320300");
            var fases = _parameterValueService.GetAllByParameterID("320301");
            var saleManagers = _parameterValueService.GetAllByParameterID("31");
            var types = _parameterValueService.GetAllByParameterID("320302");
            var costCenters = _parameterValueService.GetAllByParameterID("100103");
            var offers = _parameterValueService.GetAllByParameterID("320303");
            var status = _parameterValueService.GetAllByParameterID("320304");
            var frequency = _parameterValueService.GetAllByParameterID("320306");
            var approved = _userService.GetAll(0);
            var users = _userService.GetAll(0);

            model.LoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.LoadPriority = prioritys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadFase = fases.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadOwner = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadSaleManager = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadPreSales = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadOperationManager = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadType = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadCostCenter = costCenters.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadOffer = offers.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadFrequencyOfInteractions= frequency.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadApprovedBy = approved.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            model.Probability = "25%";
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
            return PartialView("Maintenance", model);
        }

        private MaintenancePipelineCommand MaintenancePipelineCommand(PipelineModel model)
        {
            MaintenancePipelineCommand command = new MaintenancePipelineCommand();
                            
            command.OportunityID = model.OportunityID;
            command.CustomerID = model.CustomerID;
            command.OportunityCode = model.OportunityCode;
            command.Description = model.Description;
            command.PriorityID = model.PriorityID;
            command.FaseID = model.FaseID;
            command.OwnerID = model.OwnerID;
            command.Summary = model.Summary;
            command.SaleManagerID = model.SaleManagerID;
            command.PreSalesID = model.PreSalesID;
            command.OperationManagerID = model.OperationManagerID;
            command.TypeID = model.TypeID;
            command.CostCenterID = model.CostCenterID;
            command.OfferID = model.OfferID;
            command.Sponsor = model.Sponsor;
            command.PowerSponsor = model.PowerSponsor;
            if (model.ExpectedValue != null) command.ExpectedValue = model.ExpectedValue.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.TargetDate = model.TargetDate;
            command.StatusID = model.StatusID;
            command.Probability = model.Probability;
            if (model.Billed != null) command.Billed = model.Billed.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.Comments = model.Comments;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            command.ClosingDate = model.ClosingDate;
            command.FrequencyOfInteractionID = model.FrequencyOfInteractionID;
            command.ApprovedByID = model.ApprovedByID;
            command.ApprovedDate = model.ApprovedDate;
            command.Quarter1 = model.Quarter1;
            command.Quarter2 = model.Quarter2;
            command.Quarter3 = model.Quarter3;
            command.Quarter4 = model.Quarter4;


            return command;
        }

        public ActionResult GetByID(int oportunityID, string ActionName)
        {
            var model = new PipelineModel();

            //var customer = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]));

            Result<Pipeline> pipeline = _pipelineService.GetByID(oportunityID);

            if (pipeline.IsSuccess)
            {
                model = pipeline.Value.ToModel();
                
                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var customers = _customerService.GetAll(new FilterCustomerCommand { });

                    var prioritys = _parameterValueService.GetAllByParameterID("320300");
                    var fases = _parameterValueService.GetAllByParameterID("320301");
                    var owners = _parameterValueService.GetAllByParameterID("31");
                    var saleManagers = _parameterValueService.GetAllByParameterID("31");
                    var preSales = _parameterValueService.GetAllByParameterID("31");
                    var operationManagers = _parameterValueService.GetAllByParameterID("31");
                    var types = _parameterValueService.GetAllByParameterID("320302");
                    var costCenters = _parameterValueService.GetAllByParameterID("100103");
                    var offers = _parameterValueService.GetAllByParameterID("320303");
                    var status = _parameterValueService.GetAllByParameterID("320304");
                    var frequency = _parameterValueService.GetAllByParameterID("320306");
                    var approved = _userService.GetAll(0);
                    var users = _userService.GetAll(0);

                    model.LoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
                    model.LoadPriority = prioritys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadFase = fases.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadOwner = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                    model.LoadSaleManager = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                    model.LoadPreSales = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                    model.LoadOperationManager = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                    model.LoadType = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadCostCenter = costCenters.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadOffer = offers.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadFrequencyOfInteractions= frequency.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadApprovedBy = approved.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    if (model.ExpectedValue != null) model.ExpectedValue = String.Format(new CultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(model.ExpectedValue.Replace(",", ".")));
                    if (model.Billed != null) model.Billed = String.Format(new CultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(model.Billed.Replace(",", ".")));

                    model.Description = Server.HtmlDecode(model.Description);
                    model.Comments = Server.HtmlDecode(model.Comments);

                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "Pipeline");
        }

        public ActionResult Delete(int oportunityID)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowDelete = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para excluir um registro em Pipeline !");

                return RedirectToAction("Index");
            }

            try
            {
                if (oportunityID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir registro! "));
                    return Redirect("Index");
                }
                var model = new PipelineModel();

                Result<Pipeline> pipeline = _pipelineService.GetByID(oportunityID);

                if (pipeline.IsSuccess)
                {
                    model = pipeline.Value.ToModel();

                    _pipelineService.Delete(model.OportunityID);

                    _historicalService.Delete(SystemFeatureID, oportunityID);

                    _attachmentService.Delete(SystemFeatureID, oportunityID);

                    SuccessNotification(string.Format("Registro excluido com sucesso! Pipeline : {0}", model.Description));

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
        public ActionResult Update(PipelineModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para atualizar um registro em Pipeline!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    //historical
                    Historical(model);

                    var command = MaintenancePipelineCommand(model);

                    _pipelineService.Update(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.OportunityID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = model.Description + "\n\n" + "Empresa: " + model.CustomerID + " - Oportunidade: " + model.OportunityCode;
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.OportunityID.ToString();
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

                ErrorNotification("Não foi possível salvar atualização!");

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


        private void Historical(PipelineModel model)
        {
            var command = new PipelineModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _pipelineService.GetByID(model.OportunityID);

            command = LocalCommand.Value.ToModel();

            if (command.CustomerID != model.CustomerID)
            {
                string commandCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.CustomerID));

                string modelCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(model.CustomerID));

                AddHistorical(commandCustomerID, modelCustomerID, "Empresa", model.OportunityID.ToString());
            }

            if (command.OwnerID != model.OwnerID)
            {
                string commandOwnerID = _userService.GetUserNameByID(Convert.ToInt32(command.OwnerID));

                string modelOwnerID = _userService.GetUserNameByID(Convert.ToInt32(model.OwnerID));

                AddHistorical(commandOwnerID, modelOwnerID,  "Proprietário", model.OportunityID.ToString());
            }

            if (command.PreSalesID != model.PreSalesID)
            {
                string commandPreSalesID = _userService.GetUserNameByID(Convert.ToInt32(command.PreSalesID));

                string modelPreSalesID = _userService.GetUserNameByID(Convert.ToInt32(model.PreSalesID));

                AddHistorical(commandPreSalesID, modelPreSalesID, "Pré Venda", model.OportunityID.ToString());
            }

            if (command.SaleManagerID != model.SaleManagerID)
            {
                string commandSaleManagerID = _userService.GetUserNameByID(Convert.ToInt32(command.SaleManagerID));

                string modelSaleManagerID = _userService.GetUserNameByID(Convert.ToInt32(model.SaleManagerID));

                AddHistorical(commandSaleManagerID, modelSaleManagerID,  "Gerente de Vendas", model.OportunityID.ToString());
            }

            if (command.ApprovedByID != model.ApprovedByID)
            {
                string commandApprovedByID = _userService.GetUserNameByID(Convert.ToInt32(command.ApprovedByID));

                string modelApprovedByID = _userService.GetUserNameByID(Convert.ToInt32(model.ApprovedByID));

                AddHistorical(commandApprovedByID, modelApprovedByID, "Aprovado por", model.OportunityID.ToString());
            }

            if (command.PriorityID != model.PriorityID) AddHistorical(command.PriorityID, model.PriorityID, "Prioridade", model.OportunityID.ToString(), true);
            if (command.FaseID != model.FaseID) AddHistorical(command.FaseID, model.FaseID, "Fase", model.OportunityID.ToString(), true);
            if (command.TypeID != model.TypeID) AddHistorical(command.TypeID, model.TypeID, "Tipo", model.OportunityID.ToString(), true);
            if (command.OfferID != model.OfferID) AddHistorical(command.OfferID, model.OfferID, "Oferta", model.OportunityID.ToString(), true);
            if (command.StatusID != model.StatusID) AddHistorical(command.StatusID, model.StatusID, "Status", model.OportunityID.ToString(), true);
            if (command.CostCenterID != model.CostCenterID) AddHistorical(command.CostCenterID, model.CostCenterID, "Centro de Custo", model.OportunityID.ToString(), true);
            if (command.FrequencyOfInteractionID != model.FrequencyOfInteractionID) AddHistorical(command.FrequencyOfInteractionID, model.FrequencyOfInteractionID, "Frequência das Interações", model.OportunityID.ToString(), true);

            if (command.TargetDate != model.TargetDate) AddHistorical(command.TargetDate, model.TargetDate, "Data Limite", model.OportunityID.ToString());
            //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.OportunityID.ToString());
            if (command.OportunityCode != model.OportunityCode) AddHistorical(command.OportunityCode, model.OportunityCode, "Código da Oportunidade", model.OportunityID.ToString());
            if (command.Comments != model.Comments) AddHistorical(command.Comments, model.Comments, "Comentários", model.OportunityID.ToString());
            if (command.PowerSponsor != model.PowerSponsor) AddHistorical(command.PowerSponsor, model.PowerSponsor, "PowerSponsor", model.OportunityID.ToString());
            if (command.Sponsor != model.Sponsor) AddHistorical(command.Sponsor, model.Sponsor, "Sponsor", model.OportunityID.ToString());
            if (command.ExpectedValue != model.ExpectedValue.Replace("R$ ", "")) AddHistorical(command.ExpectedValue, model.ExpectedValue.Replace("R$ ", ""), "Valor Esperado", model.OportunityID.ToString());
            if (command.ClosingDate != model.ClosingDate) AddHistorical(command.ClosingDate, model.ClosingDate, "Data Limite", model.OportunityID.ToString());
            if (command.Quarter1 != model.Quarter1) AddHistorical(command.Quarter1, model.Quarter1, "1º Trimestre", model.OportunityID.ToString());
            if (command.Quarter2 != model.Quarter2) AddHistorical(command.Quarter2, model.Quarter2, "2º Trimestre", model.OportunityID.ToString());
            if (command.Quarter3 != model.Quarter3) AddHistorical(command.Quarter3, model.Quarter3, "3º Trimestre", model.OportunityID.ToString());
            if (command.Quarter4 != model.Quarter4) AddHistorical(command.Quarter4, model.Quarter4, "4º Trimestre", model.OportunityID.ToString());

            if (!string.IsNullOrEmpty(model.Billed))
            {
                if (command.Billed != model.Billed.Replace("R$ ", "")) AddHistorical(command.Billed, model.Billed.Replace("R$ ", ""), "Valor Fechado", model.OportunityID.ToString());
            }
            if (command.Probability != model.Probability) AddHistorical(command.Probability, model.Probability, "Probabilidade", model.OportunityID.ToString());

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