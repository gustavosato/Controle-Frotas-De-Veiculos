using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Customers;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Customers;
using Lean.Test.Cloud.Domain.Entities.Customers;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;
using Lean.Test.Cloud.Domain.Command.CustomersUsers;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IExportManagerService _exportManagerService;
        private readonly IEncryptService _encryptService;
        private readonly IUserService _userService;
        private readonly IProfilesService _profilesService;
        private readonly ICustomerUserService _customerUserService;
        private readonly IParameterValueService _parameterValueService;

        public CustomerController(ICustomerService customerService,
                                    IExportManagerService exportManagerService,
                                    IEncryptService encryptService,
                                    IProfilesService profilesService,
                                    IUserService userService,
                                    IParameterValueService parameterValueService,
                                    ICustomerUserService customerUserService)
        {
            _customerService = customerService;
            _exportManagerService = exportManagerService;
            _encryptService = encryptService;
            _userService = userService;
            _profilesService = profilesService;
            _customerUserService = customerUserService;
            _parameterValueService = parameterValueService;
        }

        private string SystemFeatureID = "300";

        [HttpPost]
        public ActionResult GetAllAssociateCustomerByUserID(DataSourceRequest request, CustomerModel model)
        {
            model.UserID = Session["userAssociateID"].ToString();

            var customers = _customerService.GetAllAssociateCustomerByUserID(new FilterCustomerCommand
            {
                CustomerName = model.SearchCustomerName,
                UserID = model.UserID

            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = customers.Select(x =>
                {
                    var customersModel = x.ToModel();

                    return customersModel;
                }),
                Total = customers.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GetAllNoAssociateCustomerByUserID(DataSourceRequest request, CustomerModel model)
        {
            model.UserID = Session["userAssociateID"].ToString();

            var customers = _customerService.GetAllNoAssociateCustomerByUserID(new FilterCustomerCommand
            {
                CustomerName = model.SearchCustomerName,
                SegmentID = model.SearchSegmentID,
                TypeID = model.SearchTypeID,
                UserID = model.UserID
            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = customers.Select(x =>
                {
                    var customersModel = x.ToModel();

                    return customersModel;
                }),
                Total = customers.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new CustomerModel();

            var segments = _parameterValueService.GetAllByParameterID("300301");
            var types = _parameterValueService.GetAllByParameterID("300302");

            model.SearchLoadSegments = segments.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.SearchLoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(CustomerModel model)
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
                    WarningNotification("Você não tem permissão para adicionar uma empresa!");

                    return RedirectToAction("Index");
                }
                if (ModelState.IsValid)
                {

                    var command = MaintenanceCustomerCommand(model);

                    _customerService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso!"));

                    return RedirectToAction("Index", "Customer");

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
        public ActionResult GetAll(DataSourceRequest request, CustomerModel model)
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
                WarningNotification("Você não tem permissão para visualizar empresas!");

                return Json(gridModel);
            }
            else
            {
                var customers = _customerService.GetAll(new FilterCustomerCommand
                {
                    CustomerName = model.SearchCustomerName
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = customers.Select(x =>
                    {
                        var customersModel = x.ToModel();

                        return customersModel;
                    }),
                    Total = customers.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new CustomerModel();

            var segments = _parameterValueService.GetAllByParameterID("300301");
            var types = _parameterValueService.GetAllByParameterID("300302");

            model.LoadSegments = segments.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.IsActive = "False";
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        public ActionResult LoadActiveDesactiveCustomer()
        {
            return PartialView("ActiveDesactiveCustomer");
        }

        private MaintenanceCustomerCommand MaintenanceCustomerCommand(CustomerModel model)
        {
            MaintenanceCustomerCommand command = new MaintenanceCustomerCommand();

            command.CustomerID = model.CustomerID;
            command.CustomerName = model.CustomerName;
            command.SegmentID = model.SegmentID;
            command.TypeID = model.TypeID;
            command.Site = model.Site;
            command.Address = model.Address;
            command.Description = model.Description;
            command.IsActive = model.IsActive;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int customerID, string ActionName)
        {
                  
            var model = new CustomerModel();

            Result<Customer> customer = _customerService.GetByID(customerID);

            if (customer.IsSuccess)
            {
                model = customer.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var segments = _parameterValueService.GetAllByParameterID("300301");
                    var types = _parameterValueService.GetAllByParameterID("300302");

                    model.LoadSegments = segments.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadTypes = types.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);
                    model.Address = Server.HtmlDecode(model.Address);

                    return PartialView("Maintenance", model);
                }
                else if (ActionName == "ActiveDesactiveCustomer")
                {
                    return PartialView("ActiveDesactiveCustomer", model);
                }
                else
                //AllowApropriate
                {
                    return PartialView("AllowApropriate", model);
                }
            }
            return RedirectToAction("Index", "User");
        }

        public ActionResult Delete(int customerID)
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
                    WarningNotification("Você não tem permissão para excluir uma empresa!");

                    return RedirectToAction("Index");
                }

                if (customerID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir registro!"));
                    return Redirect("Index");
                }
                var model = new CustomerModel();
                Result<Customer> customer = _customerService.GetByID(customerID);
                if (customer.IsSuccess)
                {
                    model = customer.Value.ToModel();
                    _customerService.Delete(model.CustomerID);
                    SuccessNotification(string.Format("Registro da empresa excluído com sucesso!"));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                //ErrorNotification(ex.Message);
                ErrorNotification(string.Format("Existem outros registros associados a esta empresa. É necessário excluí-los."));
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(CustomerModel model)
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
                    WarningNotification("Você não tem permissão para atualizar uma empresa!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceCustomerCommand(model);

                    _customerService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso! "));

                    return RedirectToAction("Index");
                }

                ErrorNotification("Não foi possível realizar alteração!");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult ActiveDesactiveCustomer(CustomerModel model)
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
                    WarningNotification("Você não tem permissão para alterar o status de uma empresa!");

                    return RedirectToAction("Index");
                }

                var command = MaintenanceCustomerCommand(model);

                if (command.IsActive == "True")
                {
                    command.IsActive = "False";

                    _customerService.Update(command);

                    SuccessNotification(string.Format("Registro desativado com sucesso! "));

                    return RedirectToAction("Index");
                }
                else
                {
                    command.IsActive = "True";

                    _customerService.Update(command);

                    SuccessNotification(string.Format("Registro da empresa ativado com sucesso! "));

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);
                throw;
            }
        }

        public ActionResult UserAssociate(int customerID)
        {
            var model = new CustomerModel();
            model.CustomerID = customerID;
            Session["customerAssociateID"] = customerID;
            return PartialView("UserAssociate");
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
                WarningNotification("Você não tem permissão para desassociar um usuário!");

                return RedirectToAction("Index");
            }
            _customerUserService.Delete(Convert.ToInt16(Session["customerAssociateID"]), userID); ;

            return View();
        }

        public ActionResult AssociateUser(int userID)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para associar um usuário!");

                return RedirectToAction("Index");
            }

            var command = new MaintenanceCustomerUserCommand();

            command.CustomerID = Convert.ToInt16(Session["customerAssociateID"]);
            command.UserID = userID;

            _customerUserService.Add(command);

            return View();
        }

    }
}