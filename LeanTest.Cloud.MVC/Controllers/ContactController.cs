using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;       
using Lean.Test.Cloud.MVC.Models.Contacts;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Contacts;
using Lean.Test.Cloud.Domain.Entities.Contacts;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;


namespace Lean.Test.Cloud.MVC.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IProfilesService _profilesService;
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;

        public ContactController(IContactService contactService, 
                                IParameterValueService parameterValueService,
                                IProfilesService profilesService,
                                IUserService userService,
                                ICustomerService customerService)
        {
            _contactService = contactService;
            _parameterValueService = parameterValueService;
            _userService = userService;
            _profilesService = profilesService;
            _customerService = customerService;
        }

        private string SystemFeatureID = "315";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new ContactModel();
            
            var functions = _parameterValueService.GetAllByParameterID("100100");
            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
            var users = _userService.GetAll(0);

            model.SearchLoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ContactModel model)
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
                    WarningNotification("Você não tem permissão para adicionar um contato!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceContactCommand(model);

                    _contactService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso!"));

                    return RedirectToAction("Index", "Contact");

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
        public ActionResult GetAll(DataSourceRequest request, ContactModel model)
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
                WarningNotification("Você não tem permissão para visualizar contatos!");

                return Json(gridModel);
            }
            else
            {
                var contacts = _contactService.GetAll(new FilterContactCommand
                {
                    ContactName = model.SearchContactName,
                    Email = model.SearchEmail,
                    CustomerID = model.SearchCustomerID,
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = contacts.Select(x =>
                    {
                        var contactsModel = x.ToModel();

                        return contactsModel;
                    }),
                    Total = contacts.TotalCount
                };

                return Json(gridModel);
            }
        }
        public ActionResult New()
        {
            var model = new ContactModel();


            var functions = _parameterValueService.GetAllByParameterID("100100");
            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
            var users = _userService.GetAll(0);

            model.LoadFunction = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
            return PartialView("Maintenance", model);
        }


        private MaintenanceContactCommand MaintenanceContactCommand(ContactModel model)
        {
            MaintenanceContactCommand command = new MaintenanceContactCommand();
                            
            command.ContactID = model.ContactID;
            command.ContactName = model.ContactName;
            command.Email = model.Email;
            command.CellNumber = model.CellNumber;
            command.TelNumber = model.TelNumber;
            command.FunctionID = model.FunctionID;
            command.CustomerID = model.CustomerID;
            command.Description = model.Description;
            command.Feature = model.Feature;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 

            return command;
        }

        public ActionResult GetByID(int contactID, string ActionName)
        {
            var model = new ContactModel();

            Result<Contact> contact = _contactService.GetByID(contactID);

            if (contact.IsSuccess)
            {
                model = contact.Value.ToModel();
                
                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {

                    var functions = _parameterValueService.GetAllByParameterID("100100");
                    var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), model.CustomerID);
                    var users = _userService.GetAll(0);

                    model.LoadFunction = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadCustomer = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();

                    model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);
                    model.Feature = Server.HtmlDecode(model.Feature);

                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "Contact");
        }

        public ActionResult Delete(int contactID)
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
                    WarningNotification("Você não tem permissão para excluir um contato!");

                    return RedirectToAction("Index");
                }

                if (contactID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído!"));
                    return Redirect("Index");
                }
                var model = new ContactModel();

                Result<Contact> contact = _contactService.GetByID(contactID);

                if (contact.IsSuccess)
                {
                    model = contact.Value.ToModel();


                    _contactService.Delete(model.ContactID);

                    SuccessNotification(string.Format("Registro excluído com sucesso!"));

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
        public ActionResult Update(ContactModel model)
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
                    WarningNotification("Você não tem permissão para atualizar um contato!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceContactCommand(model);

                    _contactService.Update(command);

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