using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.AccountingEntries;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.AccountingEntries;
using Lean.Test.Cloud.Domain.Entities.AccountingEntries;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;
using Lean.Test.Cloud.Domain.Entities.Demands;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using Lean.Test.Cloud.Domain.Command.Attachments;
using Lean.Test.Cloud.MVC.Models.Attachments;
using Lean.Test.Cloud.Domain.Command.Customers;
using Lean.Test.Cloud.Domain.Command.Demands;
using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.MVC.Models.Historicals;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class AccountingEntrieController : BaseController
    {
        private readonly IAccountingEntrieService _accountingEntrieService;
        private readonly IDemandService _demandService;
        private readonly IParameterValueService _parameterValueService;
        private readonly ICustomerService _customerService;
        private readonly IHistoricalService _historicalService;
        private readonly IProfilesService _profilesService;
        private readonly IAttachmentService _attachmentService;

        public AccountingEntrieController(IAccountingEntrieService accountingEntrieService,
                                    IDemandService demandService,
                                    IParameterValueService parameterValueService,
                                    IHistoricalService historicalService,
                                    IProfilesService profilesService,
                                    IAttachmentService attachmentService,
                                    ICustomerService customerService)

        {
            _accountingEntrieService = accountingEntrieService;
            _demandService = demandService;
            _parameterValueService = parameterValueService;
            _attachmentService = attachmentService;
            _historicalService = historicalService;
            _profilesService = profilesService;
            _customerService = customerService;
        }

        private string SystemFeatureID = "301";

        public JsonResult GetDemands(string customerID, bool isActive)
        {
            var demand = _demandService.GetAll(customerID, new FilterDemandCommand { });

            return Json(demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList());
        }

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new AccountingEntrieModel();

            string userID = Session["userID"].ToString();

            var status = _parameterValueService.GetAllByParameterID("301300");

            var customers = _customerService.GetAll(new FilterCustomerCommand { });

            model.LoadSearchStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.LoadSearchCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AccountingEntrieModel model, string sourceController, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar um registro em Faturamento de Projetos!");

                    if (sourceController == "Demand")
                    {
                        return RedirectToAction("Index", "Demand");
                    }
                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceAccountingEntrieCommand(model);

                    string recordID = _accountingEntrieService.Add(command);

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
                    return RedirectToAction("Index", "AccountingEntrie");

                }

                ErrorNotification(string.Format("Não foi possível criar fatura"));

                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index", "AccountingEntrie");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível criar a fatura"));

                if (sourceController == "Demand")
                {
                    return RedirectToAction("Index", "Demand");
                }
                return RedirectToAction("Index", "AccountingEntrie");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, AccountingEntrieModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registros de Faturamento de Projetos!");

                return Json(gridModel);
            }
            else
            {
                var accountingEntries = _accountingEntrieService.GetAll(new FilterAccountingEntrieCommand
                {
                    ClassID = model.ClassID,
                    CategoryID = model.CategoryID,
                    SubCategoryID = model.SubCategoryID,
                    AccountID = model.AccountID,
                    InvoiceNumber = model.SearchInvoiceNumber,
                    DocumentNumber = model.DocumentNumber,
                    CustomerID = model.SearchCustomerID,
                    DemandID = model.SearchDemandID,
                    EmployeeID = model.EmployeeID,
                    CompetitionStartDate = model.SearchCompetitionStartDate,
                    CompetitionEndDate = model.SearchCompetitionEndDate,
                    StartDueDate = model.StartDueDate,
                    EndDueDate = model.EndDueDate,
                    StartDateRealized = model.StartDateRealized,
                    EndDateRealized = model.EndDateRealized,
                    ValueToBeRealized = model.SearchValueToBeRealized,
                    RealizedValue = model.SearchRealizedValue,
                    StatusID = model.SearchStatusID
                }, request.Page - 1, request.PageSize);

                gridModel = new DataSourceResult
                {
                    Data = accountingEntries.Select(x =>
                    {
                        var accountingEntriesModel = x.ToModel();

                        return accountingEntriesModel;
                    }),
                    Total = accountingEntries.TotalCount
                };

                return Json(gridModel);
            }
        }
        public ActionResult New(string demandID)
        {
            var model = new AccountingEntrieModel();

            string userID = Session["userID"].ToString();

            var status = _parameterValueService.GetAllByParameterID("301300");

            //var customers = _customerService.GetAll(new FilterCustomerCommand { });
            var customers = _customerService.GetAll(new FilterCustomerCommand { IsActive = true, CustomerID = Session["customerID"].ToString() });


            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.LoadCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();

            model.CustomerID = Session["customerID"].ToString();

            var demand = _demandService.GetAll(model.CustomerID, new FilterDemandCommand { DemandID = demandID });

            model.LoadDemands = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

            model.DemandID = demandID;

            model.CreatedByID = Convert.ToString(Session["userID"]);

            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        private MaintenanceAccountingEntrieCommand MaintenanceAccountingEntrieCommand(AccountingEntrieModel model)
        {
            MaintenanceAccountingEntrieCommand command = new MaintenanceAccountingEntrieCommand();

            command.AccountingEntrieID = model.AccountingEntrieID;
            command.ClassID = model.ClassID;
            command.CategoryID = model.CategoryID;
            command.SubCategoryID = model.SubCategoryID;
            command.AccountID = model.AccountID;
            command.StatusID = model.StatusID;
            command.ValueToBeRealized = model.ValueToBeRealized.Replace("R$", "").Replace(".", "").Replace(" ","");
            command.CompetitionDate = model.CompetitionDate;
            if (model.RealizedValue != null) command.RealizedValue = model.RealizedValue.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.RealizedDate = model.RealizedDate;
            command.DueDate = model.DueDate;
            command.Interest = model.Interest;
            command.InvoiceNumber = model.InvoiceNumber;
            command.DocumentNumber = model.DocumentNumber;
            command.CustomerID = model.CustomerID;
            command.DemandID = model.DemandID;
            command.EmployeeID = model.EmployeeID;
            command.Description = model.Description;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int accountingEntrieID, string ActionName)
        {
            var model = new AccountingEntrieModel();

            string userID = Session["userID"].ToString();

            var status = _parameterValueService.GetAllByParameterID("301300");


            Result<AccountingEntrie> accountingEntrie = _accountingEntrieService.GetByID(accountingEntrieID);

            if (accountingEntrie.IsSuccess)
            {
                model = accountingEntrie.Value.ToModel();

                if (ActionName == "Delete")
                {

                    model.CompetitionDate = model.CompetitionDate;

                    model.ValueToBeRealized = String.Format(new CultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(model.ValueToBeRealized));

                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var customers = _customerService.GetAll(new FilterCustomerCommand { IsActive = true, CustomerID = model.CustomerID});

                    model.LoadCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();

                    var demand = _demandService.GetAll(model.CustomerID, new FilterDemandCommand { });

                    model.LoadDemands = demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    model.ValueToBeRealized = String.Format(new CultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(model.ValueToBeRealized.Replace(",", ".")));

                    if (model.RealizedValue != null) model.RealizedValue = String.Format(new CultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(model.RealizedValue.Replace(",", ".")));

                    model.CompetitionDate = model.CompetitionDate;

                    model.RealizedDate = model.RealizedDate;

                    model.DueDate = model.DueDate;

                    model.Description = Server.HtmlDecode(model.Description);
                                       
                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "AccountingEntrie");
        }

        public ActionResult Delete(int accountingEntrieID)
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
                    WarningNotification("Você não tem permissão para excluir um registro em Faturamentos de Projetos!");

                    return RedirectToAction("Index");
                }

                if (accountingEntrieID == 0)
                {
                    ErrorNotification(string.Format("O faturamento não pode ser excluído!"));

                    return Redirect("Index");
                }
                var model = new AccountingEntrieModel();
                Result<AccountingEntrie> accountingEntrie = _accountingEntrieService.GetByID(accountingEntrieID);
                if (accountingEntrie.IsSuccess)
                {
                    model = accountingEntrie.Value.ToModel();

                    _accountingEntrieService.Delete(model.AccountingEntrieID);

                    _historicalService.Delete(SystemFeatureID, accountingEntrieID);

                    _attachmentService.Delete(SystemFeatureID, accountingEntrieID);

                    SuccessNotification(string.Format("Faturamento excluido com sucesso!"));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(AccountingEntrieModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para atualizar um registro em Faturamentos de Projetos!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    //historical
                    Historical(model);

                    var command = MaintenanceAccountingEntrieCommand(model);

                    _accountingEntrieService.Update(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.AccountingEntrieID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = model.Description + "\n\n" + "Empresa: " + model.CustomerID + " - Lançamento: " + model.DemandID;
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.AccountingEntrieID.ToString();
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

        public ActionResult StatusChange(int accountingEntrieID)
        {
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
                    WarningNotification("Você não tem permissão para alterar o status de um registro em Faturamentos de Projetos!");

                    return RedirectToAction("Index");
                }

                if (Convert.ToString(Session["isAdmin"]) == "True")
                {
                    Result<AccountingEntrie> accountingEntrie = _accountingEntrieService.GetByID(accountingEntrieID);

                    AccountingEntrieModel model = accountingEntrie.Value.ToModel();

                    AccountingEntrieModel localModel = accountingEntrie.Value.ToModel();



                    switch (localModel.StatusID)
                    {
                        case "301300300":
                            localModel.StatusID = "301300301";
                            break;

                        case "301300301":
                            localModel.StatusID = "301300302";
                            break;

                        case "301300302":
                            localModel.StatusID = "301300303";
                            break;

                        case "301300303":
                            localModel.StatusID = "301300300";
                            break;
                        default:
                            localModel.StatusID = "301300300";
                            break;
                    }

                    var command = MaintenanceAccountingEntrieCommand(localModel);


                    Historical(localModel);

                    _accountingEntrieService.Update(command);
                    
                    SuccessNotification(string.Format("Status alterado com sucesso! "));

                        return View();
                }

                WarningNotification("Você não tem permissão para aprovação de lançamentos!");

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


        private void Historical(AccountingEntrieModel model)
        {
            var command = new AccountingEntrieModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _accountingEntrieService.GetByID(model.AccountingEntrieID);

            command = LocalCommand.Value.ToModel();

            if (command.CustomerID != model.CustomerID)
            {
                string commandCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.CustomerID));

                string modelCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(model.CustomerID));

                AddHistorical(commandCustomerID, modelCustomerID, "Empresa", model.AccountingEntrieID.ToString());
            }

            //Ajustar Demanda
            if (command.DemandID != model.DemandID)
            {
                string commandDemandID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.CustomerID));

                string modelDemandID = _customerService.GetCustomerNameByID(Convert.ToInt32(model.CustomerID));

                AddHistorical(commandDemandID, modelDemandID, "Demanda", model.AccountingEntrieID.ToString());
            }

            if (command.StatusID != model.StatusID) AddHistorical(command.StatusID, model.StatusID,  "Status", model.AccountingEntrieID.ToString(), true);
            if (command.ValueToBeRealized != model.ValueToBeRealized.Replace("R$ ", "")) AddHistorical(command.ValueToBeRealized, model.ValueToBeRealized.Replace("R$ ", ""), "Valor a ser realizado", model.AccountingEntrieID.ToString());
            if (command.CompetitionDate != model.CompetitionDate) AddHistorical(command.CompetitionDate, model.CompetitionDate, "Data da Competência", model.AccountingEntrieID.ToString());
            if (!string.IsNullOrEmpty(model.RealizedValue))
                {
                    if (command.RealizedValue != model.RealizedValue.Replace("R$ ", "")) AddHistorical(command.RealizedValue, model.RealizedValue.Replace("R$ ", ""), "Valor realizado", model.AccountingEntrieID.ToString());
                }
            if (command.RealizedDate != model.RealizedDate) AddHistorical(command.RealizedDate, model.RealizedDate, "Data Realizada", model.AccountingEntrieID.ToString());
            if (command.InvoiceNumber != model.InvoiceNumber) AddHistorical(command.InvoiceNumber, model.InvoiceNumber, "Número da Nota Fiscal", model.AccountingEntrieID.ToString());
            //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.AccountingEntrieID.ToString());
            if (command.DueDate != model.DueDate) AddHistorical(command.DueDate, model.DueDate, "Data de Vencimento", model.AccountingEntrieID.ToString());
            if (command.DocumentNumber != model.DocumentNumber) AddHistorical(command.DocumentNumber, model.DocumentNumber, "Número do Documento", model.AccountingEntrieID.ToString());
            if (command.Interest != model.Interest) AddHistorical(command.Interest, model.Interest, "Data de Vencimento", model.AccountingEntrieID.ToString());

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