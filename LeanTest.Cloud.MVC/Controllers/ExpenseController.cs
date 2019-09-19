using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Expenses;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Expenses;
using Lean.Test.Cloud.Domain.Entities.Expenses;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;
using System.Globalization;
using Lean.Test.Cloud.MVC.Models.SystemParameter;
using System.Web;
using System.IO;
using Lean.Test.Cloud.Domain.Command.Attachments;
using Lean.Test.Cloud.MVC.Models.Attachments;
using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.MVC.Models.Historicals;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain.Command.Demands;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class ExpenseController : BaseController
    {
        private readonly IExpenseService _expenseService;
        private readonly IDemandService _demandService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IUserService _userService1;
        private readonly ICustomerService _customerService;
        private readonly ISystemParameterService _systemParameterService;
        private readonly IProfilesService _profilesService;
        private readonly IAttachmentService _attachmentService;
        private readonly IHistoricalService _historicalService;

        public ExpenseController(IExpenseService expenseService,
                                    IUserService userService,
                                    IUserService userService1,
                                    ICustomerService customerService,
                                    IDemandService demandService,
                                    ISystemParameterService systemParameterService,
                                    IAttachmentService attachmentService,
                                    IProfilesService profilesService,
                                    IHistoricalService historicalService,
                                    IParameterValueService parameterValueService)
        {
            _expenseService = expenseService;
            _demandService = demandService;
            _parameterValueService = parameterValueService;
            _userService = userService;
            _userService1 = userService1;
            _customerService = customerService;
            _attachmentService = attachmentService;
            _profilesService = profilesService;
            _historicalService = historicalService;
            _systemParameterService = systemParameterService;
        }

        private string SystemFeatureID = "309";

        public JsonResult GetDemands(string customerID, bool isActive)
        {
            var demand = _demandService.GetAll(customerID, new FilterDemandCommand { IsActive = isActive });

            return Json(demand.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList());
        }

        public JsonResult GetKMValue()
        {
            var systemParameterModel = new SystemParameterModel();

            var kmValue = _systemParameterService.GetByID(4);

            systemParameterModel = kmValue.Value.ToModel();

            return Json(new { success = false, responseText = string.Format(systemParameterModel.ParamterValue) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ExpenseModel();

            string userID = Session["userID"].ToString();

            var status = _parameterValueService.GetAllByParameterID("309301");

            var typeExpense = _parameterValueService.GetAllByParameterID("309300");

            var Departments = _parameterValueService.GetAllByParameterID("100103");

            var createdBy = _userService.GetAll(0);

            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");

            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadTypeExpenses = typeExpense.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadCreateds = createdBy.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.SearchLoadDepartments = Departments.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        public ActionResult Report()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ExpenseModel();

            model.RegisterDate = DateTime.Today.ToString("dd/MM/yyyy");

            DateTime date = DateTime.ParseExact(model.RegisterDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var startDate = new DateTime(date.Year, date.Month, 1);

            var endDate = startDate.AddMonths(1).AddDays(-1);

            model.SearchStartDateReport = Convert.ToDateTime(startDate).ToString("dd/MM/yyyy");

            model.SearchEndDateReport = Convert.ToDateTime(endDate).ToString("dd/MM/yyyy");

            return PartialView("Report", model);
        }

        [HttpPost]
        public ActionResult Add(ExpenseModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar uma despesa!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceExpenseCommand(model);

                    string recordID = _expenseService.Add(command);

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

                        return RedirectToAction("Index", "Expense");
                    }
                    SuccessNotification(string.Format("Registro realizado com sucesso, sem anexo!"));

                    return RedirectToAction("Index", "Expense");
                }
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Expense");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Expense");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ExpenseModel model)
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
                WarningNotification("Você não tem permissão para visualizar as despesas!");

                return Json(gridModel);

            }
            else
            {

                var expenses = _expenseService.GetAll(new FilterExpenseCommand
                {
                    CreatedByID = (Convert.ToString(Session["isAdmin"]) == "False") ? Convert.ToString(Session["userID"]) : model.SearchCreatedByID,
                    CustomerID = model.SearchCustomerID,
                    DepartmentID = model.SearchDepartmentID,
                    DemandID = model.SearchDemandID,
                    RegisterDateFrom = model.SearchRegisterDateFrom,
                    RegisterDateTo = model.SearchRegisterDateTo,
                    StatusID = model.SearchStatusID,
                    TypeExpenseID = model.SearchTypeExpenseID,
                    Description = model.SearchDescription
                }, request.Page - 1, request.PageSize);

                gridModel = new DataSourceResult
                {
                    Data = expenses.Select(x =>
                    {
                        var expensesModel = x.ToModel();

                        return expensesModel;
                    }),
                    Total = expenses.TotalCount
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        public ActionResult GetAllByDemandID(DataSourceRequest request, string recordID)
        {
            var expenses = _expenseService.GetAll(new FilterExpenseCommand
            {
                DemandID = recordID,
                customerID = Session["customerID"].ToString()
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = expenses.Select(x =>
                {
                    var expensesModel = x.ToModel();

                    return expensesModel;
                }),
                Total = expenses.TotalCount
            };
            return Json(gridModel);
        }




        public ActionResult GetTotalByUsers(DataSourceRequest request, ExpenseModel model)
        {
            var gridModel = new DataSourceResult();

            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowReportView = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para visualizar o relatório!");

                return Json(gridModel);
            }
            else
            {
                var timeReleases = _expenseService.GetTotalByUsers(new FilterExpenseCommand
                {
                    RegisterDateFrom = model.SearchStartDateReport,
                    RegisterDateTo = model.SearchEndDateReport
                }, request.Page - 1, request.PageSize);

                gridModel = new DataSourceResult
                {
                    Data = timeReleases.Select(x =>
                    {
                        var timeReleasesModel = x.ToModel();

                        return timeReleasesModel;
                    }),
                    Total = timeReleases.TotalCount
                };

                return Json(gridModel);
            }
        }
        public ActionResult New()
        {
            var model = new ExpenseModel();

            string userID = Session["userID"].ToString();

            var status = _parameterValueService.GetAllByParameterID("309301");

            var typeExpense = _parameterValueService.GetAllByParameterID("309300");

            var Departments = _parameterValueService.GetAllByParameterID("100103");

            var createdBy = _userService.GetAll(0);

            var approvedBy = _userService.GetAll(0);

            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");

            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadTypeExpenses = typeExpense.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadCreateds = createdBy.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadApproveds = approvedBy.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.LoadDepartments = Departments.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.Refundable = "True";
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        private MaintenanceExpenseCommand MaintenanceExpenseCommand(ExpenseModel model)
        {
            MaintenanceExpenseCommand command = new MaintenanceExpenseCommand();

            command.ExpenseID = model.ExpenseID;
            command.RegisterDate = model.RegisterDate;
            command.Refundable = model.Refundable;
            command.StatusID = model.StatusID;
            command.Kilometer = model.Kilometer;
            if (model.SubTotal != null) command.SubTotal = model.SubTotal.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.AmountExpense = model.AmountExpense.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.TypeExpenseID = model.TypeExpenseID;
            command.CustomerID = model.CustomerID;
            command.DepartmentID = model.DepartmentID;
            command.ApprovedByID = model.ApprovedByID;
            command.ApprovedDate = model.ApprovedDate;
            command.DemandID = model.DemandID;
            command.Description = model.Description;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int expenseID, string ActionName)
        {
            var model = new ExpenseModel();

            string userID = Session["userID"].ToString();

            var demand = _demandService.GetAll(Session["customerID"].ToString(), "0", Session["userID"].ToString());

            var status = _parameterValueService.GetAllByParameterID("309301");

            var typeExpense = _parameterValueService.GetAllByParameterID("309300");

            var Departments = _parameterValueService.GetAllByParameterID("100103");

            Result<Expense> expense = _expenseService.GetByID(expenseID);

            if (expense.IsSuccess)
            {
                model = expense.Value.ToModel();


                if (ActionName == "Delete")
                {
                    model.RegisterDate = model.RegisterDate;

                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadTypeExpenses = typeExpense.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), model.CustomerID);
                    model.LoadCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
                    model.LoadDepartments = Departments.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    var createdBy = _userService.GetAll(Convert.ToInt32(model.CreatedByID));
                    model.LoadCreateds = createdBy.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    var approvedBY = _userService.GetAll(Convert.ToInt32(model.ApprovedByID));
                    model.LoadApproveds = approvedBY.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    var demandByCustomer = _demandService.GetAllByCustomerID(model.CustomerID);

                    model.LoadDemands = demandByCustomer.Select(x => new SelectListItem() { Text = x.demandName.ToString(), Value = x.demandID.ToString() }).ToList();

                    model.AmountExpense = String.Format(new CultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(model.AmountExpense.Replace(",", ".")));

                    if (model.SubTotal != null) model.SubTotal = String.Format(new CultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(model.SubTotal.Replace(",", ".")));

                    model.RegisterDate = model.RegisterDate;

                    model.Refundable = "True";

                    if (model.ApprovedDate != null) model.ApprovedDate = model.ApprovedDate;

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
                else if (ActionName == "StatusChange")
                {
                    model.RegisterDate = model.RegisterDate;

                    return PartialView("StatusChange", model);
                }
            }
            return RedirectToAction("Index", "Expense");
        }

        public ActionResult Delete(int expenseID)
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
                    WarningNotification("Você não tem permissão para excluir uma despesa!");

                    return RedirectToAction("Index");
                }

                if (expenseID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído!"));

                    return Redirect("Index");
                }
                var model = new ExpenseModel();

                Result<Expense> expense = _expenseService.GetByID(expenseID);

                if (expense.IsSuccess)
                {
                    model = expense.Value.ToModel();

                    //validar se o registro percente ao usuario logado
                    if (model.CreatedByID != Convert.ToString(Session["userID"]))
                    {
                        WarningNotification(string.Format("Não é permitido excluir o registro de outro usuário!"));

                        return Redirect("Index");
                    }
                    _expenseService.Delete(model.ExpenseID);

                    _historicalService.Delete(SystemFeatureID, expenseID);

                    _attachmentService.Delete(SystemFeatureID, expenseID);

                    SuccessNotification(string.Format("Registro excluído com sucesso!"));

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
        public ActionResult Update(ExpenseModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para atualizar uma despesa!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    //historical
                    Historical(model);

                    var command = MaintenanceExpenseCommand(model);

                    if (command.StatusID == "309301301")
                    {
                        command.ApprovedByID = Convert.ToString(Session["userID"]);
                        command.ApprovedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    if (command.StatusID == "309301304")
                    {
                        command.ApprovedByID = null;
                        command.ApprovedDate = null;
                    }
                    if (Convert.ToString(Session["isAdmin"]) != "True")
                    {
                        if (model.CreatedByID != Convert.ToString(Session["userID"]))
                        {
                            WarningNotification(string.Format("Não é permitido atualizar registro de outro usuário!"));

                            return Redirect("Index");
                        }

                        if (model.StatusID == "309301301" || model.StatusID == "309301302") //approved
                        {
                            WarningNotification(string.Format("Não é permitido atualizar registro já aprovado!"));

                            return Redirect("Index");
                        }
                    }

                    //var command = MaintenanceExpenseCommand(model);

                    _expenseService.Update(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.ExpenseID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = model.Description + "\n\n" + "Empresa: " + model.CustomerID + " - Depesa: " + model.DemandID;
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.ExpenseID.ToString();
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

        public ActionResult StatusChange(int expenseID)
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
                    WarningNotification("Você não tem permissão para alterar o status de uma despesa!");

                    return RedirectToAction("Index");
                }

                if (Convert.ToString(Session["isAdmin"]) == "True")
                {
                    Result<Expense> expense = _expenseService.GetByID(expenseID);

                    ExpenseModel model = expense.Value.ToModel();

                    ExpenseModel localModel = expense.Value.ToModel();

                    if (model.CreatedByID != Session["userID"].ToString())
                    {

                        switch (localModel.StatusID)
                        {
                            case "309301300":
                                localModel.StatusID = "309301301";
                                break;

                            case "309301301":
                                localModel.StatusID = "309301302";
                                break;

                            case "309301302":
                                localModel.StatusID = "309301303";
                                break;

                            case "309301303":
                                localModel.StatusID = "309301304";
                                break;
                            default:
                                localModel.StatusID = "309301300";
                                break;
                        }
                      
                        var command = MaintenanceExpenseCommand(localModel);
                        if (command.StatusID == "309301301") 
                        {
                            command.ApprovedByID = Convert.ToString(Session["userID"]);
                            command.ApprovedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        }
                        if (command.StatusID == "309301304")
                        {
                            command.ApprovedByID = null;
                            command.ApprovedDate = null;
                        }
                        //string userID = Session["userID"].ToString();
                        //var approvedBY = _userService.GetAll(Convert.ToInt32(model.ApprovedByID));
                        //model.LoadApproveds = approvedBY.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();


                        //if (localModel.StatusID == "309301301") //approved
                        //{
                        //    localModel.ApprovedByID = Convert.ToString(Session["userID"]);
                        //    localModel.ApprovedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        //}

                        Historical(localModel);
                 
                        _expenseService.Update(command);

                        SuccessNotification(string.Format("Registro alterado com sucesso!"));

                        return View();
                    }
                    else
                    {
                        WarningNotification(string.Format("Você não pode aprovar seu próprio lançamento, contate seu gestor! "));

                        return RedirectToAction("Index");
                    }
                }
                WarningNotification("Você não tem permissão para aprovação de registro!");

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Erro ao atualizar registro, tente novamente!"));

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

        //Realizando histórico de alterações 


        private void Historical(ExpenseModel model)
        {
            var command = new ExpenseModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _expenseService.GetByID(model.ExpenseID);

            command = LocalCommand.Value.ToModel();

            if (command.CustomerID != model.CustomerID)
            {
                string commandCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.CustomerID));

                string modelCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(model.CustomerID));

                AddHistorical(commandCustomerID, modelCustomerID, "Empresa", model.ExpenseID.ToString());
            }


            //Ajustar Demanda
            if (command.DemandID != model.DemandID)
            {
                string commandDemandID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.CustomerID));

                string modelDemandID = _customerService.GetCustomerNameByID(Convert.ToInt32(model.CustomerID));

                AddHistorical(commandDemandID, modelDemandID, "Demanda", model.ExpenseID.ToString());
            }

            if (command.ApprovedByID != model.ApprovedByID)
            {
                string commandApprovedByID = _userService.GetUserNameByID(Convert.ToInt32(command.ApprovedByID));

                string modelApprovedByID = _userService.GetUserNameByID(Convert.ToInt32(model.ApprovedByID));

                AddHistorical(modelApprovedByID, commandApprovedByID, "Aprovado por", model.ExpenseID.ToString());
            }

            if (command.StatusID != model.StatusID) AddHistorical(command.StatusID,model.StatusID, "Status", model.ExpenseID.ToString(), true);
            if (command.TypeExpenseID != model.TypeExpenseID) AddHistorical(command.TypeExpenseID, model.TypeExpenseID, "Tipo de Despesa", model.ExpenseID.ToString(), true);
            if (command.RegisterDate != model.RegisterDate) AddHistorical(command.RegisterDate, model.RegisterDate, "Data de Registro", model.ExpenseID.ToString());
            //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.ExpenseID.ToString());
            if (command.DepartmentID != model.DepartmentID) AddHistorical(command.DepartmentID, model.DepartmentID, "Departamento", model.ExpenseID.ToString(), true);
            if (command.SubTotal != model.SubTotal.Replace("R$ ", "")) AddHistorical(command.SubTotal, model.SubTotal.Replace("R$ ", ""), "Valor", model.ExpenseID.ToString());
            if (command.Kilometer != model.Kilometer) AddHistorical(command.Kilometer, model.Kilometer, "Kilometragem", model.ExpenseID.ToString());
            if (command.AmountExpense != model.AmountExpense.Replace("R$ ", "")) AddHistorical(command.AmountExpense, model.AmountExpense.Replace("R$ ", ""), "Valor Total", model.ExpenseID.ToString());
            if (command.ApprovedDate != model.ApprovedDate) AddHistorical(model.ApprovedDate,command.ApprovedDate, "Data de Aprovação", model.ExpenseID.ToString());


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