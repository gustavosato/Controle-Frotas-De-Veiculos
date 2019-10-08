using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Vacancies;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using ControleVeiculos.Infrastructure.Mvc;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Entities.Vacancies;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Command.VacanciesResumes;
using ControleVeiculos.Domain.Command.Resumes;
using ControleVeiculos.MVC.Models.Resumes;
using ControleVeiculos.MVC.Models.Historicals;
using ControleVeiculos.Domain.Command.Historicals;

namespace ControleVeiculos.MVC.Controllers
{
    public class VacancieController : BaseController
    {
        private readonly IVacancieService _vacancieService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IProfilesService _profilesService;
        private readonly ICustomerService _customerService;
        private readonly IContactService _contactService;
        private readonly IVacancieResumeService _vacancieResumeService;
        private readonly IResumeService _resumeService;
        private readonly IHistoricalService _historicalService;

        public VacancieController(IVacancieService vacancieService,
                                IParameterValueService parameterValueService,
                                IProfilesService profilesService,
                                IUserService userService,
                                IContactService contactService,
                                IVacancieResumeService vacancieResumeService,
                                IResumeService resumeService,
                                IHistoricalService historicalService,
                                ICustomerService customerService)
        {
            _vacancieService = vacancieService;
            _parameterValueService = parameterValueService;
            _profilesService = profilesService;
            _userService = userService;
            _customerService = customerService;
            _contactService = contactService;
            _resumeService = resumeService;
            _vacancieResumeService = vacancieResumeService;
            _historicalService = historicalService;
        }

        private string SystemFeatureID = "316";

        public ActionResult ResumeAssociate(int vacancieID)
        {
            var model = new VacancieModel();
            model.VacancieID = vacancieID;
            Session["vacancieAssociateID"] = vacancieID;
            return PartialView("ResumeAssociate");
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new VacancieModel();

            var vacanciesTypes = _parameterValueService.GetAllByParameterID("100100");
            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
            var externalApplicants = _contactService.GetAll(0, Convert.ToInt32(Session["customerID"]));
            var contractTypes = _parameterValueService.GetAllByParameterID("316300");
            var validitys = _parameterValueService.GetAllByParameterID("316301");
            var status = _parameterValueService.GetAllByParameterID("316302");
            var users = _userService.GetAll(0);


            model.SearchLoadVacanciesType = vacanciesTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.SearchLoadInternalApplicant = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadExternalApplicant = externalApplicants.Select(x => new SelectListItem() { Text = x.contactName.ToString(), Value = x.contactID.ToString() }).ToList();
            model.SearchLoadAssignTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.SearchLoadContractType = contractTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadValidity = validitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(VacancieModel model)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowAdd = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para adicionar um registro em Vagas!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceVacancieCommand(model);

                    _vacancieService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "Vacancie");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro! "));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro! ", model.Summary));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, VacancieModel model)
        {
            var gridModel = new DataSourceResult();

            //permissions
            if (_profilesService.GetAllow(new FilterAbastecimentoCommand
            {
                AllowView = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para visualizar os registros de apropriação de horas!");

                return Json(gridModel);
            }
            else
            {
                var vacancies = _vacancieService.GetAll(new FilterFuncionarioCommand
                {
                    Summary = model.SearchSummary,
                    VacanciesTypeID = model.SearchVacanciesTypeID,
                    CustomerID = model.SearchCustomerID,
                    ContractTypeID = model.SearchContractTypeID,
                    ValidityID = model.SearchValidityID,
                    StatusID = model.SearchStatusID,
                    WorkPlace = model.SearchWorkPlace,
                    CreatedByID = model.SearchCreatedByID,

                }, request.Page - 1, request.PageSize);

                gridModel = new DataSourceResult
                {
                    Data = vacancies.Select(x =>
                    {
                        var vacanciesModel = x.ToModel();

                        return vacanciesModel;
                    }),
                    Total = vacancies.TotalCount
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        public ActionResult GetAllAssociateVacancieByResumeID(DataSourceRequest request, ResumeModel model)

        {
            model.VacancieID = Session["vacancieAssociateID"].ToString();

            var vacancies = _vacancieResumeService.GetAllAssociateVacancieByResumeID(new FilterResumeCommand
            {
                Summary = model.SearchSummary,
                VacancieID = model.VacancieID
            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = vacancies.Select(x =>
                {
                    var vacanciesModel = x.ToModel();

                    return vacanciesModel;
                }),
                Total = vacancies.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GetAllNoAssociateVacancieByResumeID(DataSourceRequest request, ResumeModel model)
        {
            model.VacancieID = Session["vacancieAssociateID"].ToString();

            var vacancies = _vacancieResumeService.GetAllNoAssociateVacancieByResumeID(new FilterResumeCommand
            {
                Summary = model.SearchSummary,
                VacancieID = model.VacancieID
            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = vacancies.Select(x =>
                {
                    var vacanciesModel = x.ToModel();

                    return vacanciesModel;
                }),
                Total = vacancies.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new VacancieModel();
            var vacanciesTypes = _parameterValueService.GetAllByParameterID("100100");
            var users = _userService.GetAll(0);
            var externalApplicants = _contactService.GetAll(0, Convert.ToInt32(Session["customerID"]));
            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
            var contractTypes = _parameterValueService.GetAllByParameterID("316300");
            var validitys = _parameterValueService.GetAllByParameterID("316301");
            var status = _parameterValueService.GetAllByParameterID("316302");


            model.LoadVacanciesType = vacanciesTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
            model.LoadInternalApplicant = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadExternalApplicant = externalApplicants.Select(x => new SelectListItem() { Text = x.contactName.ToString(), Value = x.contactID.ToString() }).ToList();
            model.LoadAssignTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadContractType = contractTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadValidity = validitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        public ActionResult DisassociateResume(int resumeID)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterAbastecimentoCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para desassociar um grupo!");

                return View();
            }
            else
            {
                _vacancieResumeService.Delete(Convert.ToInt16(Session["vacancieAssociateID"]), resumeID);

                return View();
            }
        }

        public ActionResult AssociateResume(int resumeID)
        {
            var command = new MaintenanceVacancieResumeCommand();
            //permissions
            if (_profilesService.GetAllow(new FilterAbastecimentoCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para associar um grupo!");

                return View();
            }
            else
            {
                command = new MaintenanceVacancieResumeCommand();

                command.VacancieID = Convert.ToInt16(Session["vacancieAssociateID"]);
                command.ResumeID = resumeID;

                _vacancieResumeService.Add(command);

                return View();
            }
        }

        private MaintenanceVacancieCommand MaintenanceVacancieCommand(VacancieModel model)
        {
            MaintenanceVacancieCommand command = new MaintenanceVacancieCommand();

            command.VacancieID = model.VacancieID;
            command.Summary = model.Summary;
            command.VacanciesTypeID = model.VacanciesTypeID;
            command.Description = model.Description;
            command.CustomerID = model.CustomerID;
            command.InternalApplicantID = model.InternalApplicantID;
            command.ExternalApplicantID = model.ExternalApplicantID;
            command.AssignToID = model.AssignToID;
            command.ContractTypeID = model.ContractTypeID;
            command.StatusID = model.StatusID;
            command.ValidityID = model.ValidityID;
            command.OpeningDate = model.OpeningDate;
            command.ClosingDate = model.ClosingDate;
            command.ExpectedStartDate = model.ExpectedStartDate;
            if (model.MaximumValue != null) command.MaximumValue = model.MaximumValue.Replace("R$", "").Replace(".", "").Replace(" ", "");
            if (model.ClosedValue != null) command.ClosedValue = model.ClosedValue.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.WorkPlace = model.WorkPlace;
            command.ResumeSelectedID = model.ResumeSelectedID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int vacancieID, string ActionName)
        {
            var model = new VacancieModel();

            Result<Vacancie> vacancie = _vacancieService.GetByID(vacancieID);

            if (vacancie.IsSuccess)
            {
                model = vacancie.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {


                    var vacanciesTypes = _parameterValueService.GetAllByParameterID("100100");
                    var contacts = _contactService.GetAll(0, Convert.ToInt32(Session["customerID"]));
                    var users = _userService.GetAll(0);
                    var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
                    var contractTypes = _parameterValueService.GetAllByParameterID("316300");
                    var validitys = _parameterValueService.GetAllByParameterID("316301");
                    var status = _parameterValueService.GetAllByParameterID("316302");
                    var resumeSelected = _vacancieResumeService.GetAllAssociateVacancieByResumeID(vacancieID.ToString(), Convert.ToString(Session["resumeID"]));


                    model.LoadVacanciesType = vacanciesTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadContractType = contractTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadValidity = validitys.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadStatus = status.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadExternalApplicant = contacts.Select(x => new SelectListItem() { Text = x.contactName.ToString(), Value = x.contactID.ToString() }).ToList();
                    model.LoadInternalApplicant = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                    model.LoadAssignTo = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                    model.LoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();
                    model.LoadResumeSelected = resumeSelected.Select(x => new SelectListItem() { Text = x.summary.ToString(), Value = x.resumeID.ToString() }).ToList();

                    model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
                    model.Description = Server.HtmlDecode(model.Description);


                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "Vacancie");
        }

        [HttpPost]
        public ActionResult Update(VacancieModel model)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowUpdate = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para atualizar um registro em Vagas!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    //historical
                    Historical(model);

                    var command = MaintenanceVacancieCommand(model);

                    _vacancieService.Update(command);

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

        //Realizando histórico de alterações 
        private void Historical(VacancieModel model)
        {
            var command = new VacancieModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _vacancieService.GetByID(model.VacancieID);

            command = LocalCommand.Value.ToModel();

            if (command.Summary != model.Summary) AddHistorical(command.Summary, model.Summary, "Sumário", model.VacancieID.ToString());
            if (command.VacanciesTypeID != model.VacanciesTypeID) AddHistorical(command.VacanciesTypeID, model.VacanciesTypeID, "Tipo de Vaga", model.VacancieID.ToString(), true);
            //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.Description.ToString());
            if (command.ContractTypeID != model.ContractTypeID) AddHistorical(command.ContractTypeID, model.ContractTypeID, "Tipo de Contratação", model.VacancieID.ToString(), true);
            if (command.ValidityID != model.ValidityID) AddHistorical(command.ValidityID, model.ValidityID, "Vigência", model.VacancieID.ToString(), true);
            if (command.StatusID != model.StatusID) AddHistorical(command.StatusID, model.StatusID, "Status", model.VacancieID.ToString(), true);
            
            if (command.InternalApplicantID != model.InternalApplicantID)
            {
                string commandInternalApplicantID = _userService.GetUserNameByID(Convert.ToInt32(command.InternalApplicantID));

                string modelInternalApplicantID = _userService.GetUserNameByID(Convert.ToInt32(model.InternalApplicantID));

                AddHistorical(commandInternalApplicantID, modelInternalApplicantID, "Solicitante Interno", model.VacancieID.ToString());
            }

            if (command.ExternalApplicantID != model.ExternalApplicantID)
            {
                string commandExternalApplicantID = _userService.GetUserNameByID(Convert.ToInt32(command.ExternalApplicantID));

                string modelExternalApplicantID = _userService.GetUserNameByID(Convert.ToInt32(model.ExternalApplicantID));

                AddHistorical(commandExternalApplicantID, modelExternalApplicantID, "Solicitante Externo", model.VacancieID.ToString());
            }

            if (command.CustomerID != model.CustomerID)
            {
                string commandCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(command.CustomerID));

                string modelCustomerID = _customerService.GetCustomerNameByID(Convert.ToInt32(model.CustomerID));

                AddHistorical(commandCustomerID, modelCustomerID, "Empresa", model.VacancieID.ToString());
            }

            if (command.AssignToID != model.AssignToID)
            {
                string commandAssignToID = _userService.GetUserNameByID(Convert.ToInt32(command.AssignToID));

                string modelAssignToID = _userService.GetUserNameByID(Convert.ToInt32(model.AssignToID));

                AddHistorical(commandAssignToID, modelAssignToID, "Responsável por executar a tarefa", model.VacancieID.ToString());
            }

            if (command.OpeningDate != model.OpeningDate) AddHistorical(command.OpeningDate, model.OpeningDate, "Data da Abertura", model.VacancieID.ToString());
            if (command.ClosingDate != model.ClosingDate) AddHistorical(command.ClosingDate, model.ClosingDate, "Limite para Fechamento", model.VacancieID.ToString());
            if (command.ExpectedStartDate != model.ExpectedStartDate) AddHistorical(command.ExpectedStartDate, model.ExpectedStartDate, "Início Previsto", model.VacancieID.ToString());
            if (command.MaximumValue != model.MaximumValue) AddHistorical(command.MaximumValue, model.MaximumValue, "Valor Máximo", model.VacancieID.ToString());
            if (command.ClosedValue != model.ClosingDate) AddHistorical(command.ClosedValue, model.ClosedValue, "Valor Fechado", model.VacancieID.ToString());
            if (command.WorkPlace != model.WorkPlace) AddHistorical(command.WorkPlace, model.WorkPlace, "Local de Trabalho", model.VacancieID.ToString());
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

        public ActionResult Delete(int vacancieID)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowDelete = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para excluir um registro em vagas!");

                    return RedirectToAction("Index");
                }

                if (vacancieID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new VacancieModel();

                Result<Vacancie> vacancie = _vacancieService.GetByID(vacancieID);

                if (vacancie.IsSuccess)
                {
                    model = vacancie.Value.ToModel();


                    _vacancieService.Delete(model.VacancieID);

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
    }
}