using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Licenses;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Licenses;
using Lean.Test.Cloud.Domain.Entities.Licenses;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;


namespace Lean.Test.Cloud.MVC.Controllers
{
    public class LicenseController : BaseController
    {
        private readonly ILicenseService _licenseService;
        private readonly IProfilesService _profilesService;
        private readonly ILicenseGeneratorService _licenseGeneratorService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;


        public LicenseController(ILicenseService licenseService, 
                                    ILicenseGeneratorService licenseGeneratorService,
                                    ICustomerService customerService, 
                                    IParameterValueService parameterValueService,
                                    IProfilesService profilesService,
                                    IUserService userService)
        {
            _licenseService = licenseService;
            _licenseGeneratorService = licenseGeneratorService;
            _customerService = customerService;
            _parameterValueService = parameterValueService;
            _profilesService = profilesService;
            _userService = userService;
        }

        private string SystemFeatureID = "213";

        public ActionResult Index()
        {
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowAdd = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para adicionar um registro em Licenças!");

                return RedirectToAction("Index");
            }

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new LicenseModel();

            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
            var createds = _userService.GetAll(0);

            model.LoadSCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName, Value = x.customerID.ToString() }).ToList();
            model.LoadCreatedBys = createds.Select(x => new SelectListItem() { Text = x.userName, Value = x.userID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(LicenseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceLicenseCommand(model);

                    _licenseService.Add(command);

                    SuccessNotification(string.Format("Licença criada com sucesso! "));

                    return RedirectToAction("Index", "License");
                }

                ErrorNotification(string.Format("Não foi possível realizar registro em licença!"));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível criar a licença! "));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, LicenseModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registros em Licenças!");

                return Json(gridModel);
            }
            else
            {
                int userID = Convert.ToInt32(Session["userID"]);

                var licenses = _licenseService.GetAll(new FilterLicenseCommand
                {
                    LicenseCode = model.LicenseCode,
                    CustomerID = model.CustomerID,
                    HostName = model.HostName,
                    CreatedByID = model.CreatedByID
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = licenses.Select(x =>
                    {
                        var licensesModel = x.ToModel();

                        return licensesModel;
                    }),
                    Total = licenses.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new LicenseModel();

            var typeLicenses = _parameterValueService.GetAllByParameterID("30");
            var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), "0");
            var approveds = _userService.GetAll(0);

            model.ExpirationDate = Convert.ToDateTime(DateTime.Today.AddMonths(3)).ToString("dd/MM/yyyy");
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            model.LoadLicenseTypes = typeLicenses.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadApprovedBys = approveds.Select(x => new SelectListItem() { Text = x.userName, Value = x.userID.ToString() }).ToList();
            model.LoadSCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName, Value = x.customerID.ToString() }).ToList();
            model.LoadCreatedBys= approveds.Select(x => new SelectListItem() { Text = x.userName, Value = x.userID.ToString() }).ToList();
            model.LoadApprovedBys = approveds.Select(x => new SelectListItem() { Text = x.userName, Value = x.userID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);

            return PartialView("Maintenance", model);
        }

        private MaintenanceLicenseCommand MaintenanceLicenseCommand(LicenseModel model)
        {
            MaintenanceLicenseCommand command = new MaintenanceLicenseCommand();

            command.LicenseID = model.LicenseID;
            command.LicenseCode = model.LicenseCode;
            command.Description = model.Description;
            command.LicenseTypeID = model.LicenseTypeID;
            command.License = model.License;
            command.ExpirationDate = model.ExpirationDate;
            command.ApprovedByID = model.ApprovedByID;
            command.ApprovedDate = model.ApprovedDate;
            command.HostName = model.HostName;
            command.MacAddress = model.MacAddress;
            command.CustomerID = model.CustomerID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int licenseID, string ActionName)
        {
            var model = new LicenseModel();

            var typeLicenses = _parameterValueService.GetAllByParameterID("30");

            Result<License> license = _licenseService.GetByID(licenseID);

            if (license.IsSuccess)
            {
                model = license.Value.ToModel();

                model.LoadLicenseTypes = typeLicenses.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                var customers = _customerService.GetAllAssociateCustomerByUserID(Convert.ToString(Session["userID"]), model.CustomerID);
                model.LoadSCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName, Value = x.customerID.ToString() }).ToList();

                var createds = _userService.GetAll(Convert.ToInt32(model.CreatedByID));
                model.LoadCreatedBys = createds.Select(x => new SelectListItem() { Text = x.userName, Value = x.userID.ToString() }).ToList();

                var approveds = _userService.GetAll(Convert.ToInt32(model.ApprovedByID));
                model.LoadApprovedBys = approveds.Select(x => new SelectListItem() { Text = x.userName, Value = x.userID.ToString() }).ToList();

                if (ActionName == "Delete")
                {
                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    return PartialView("Maintenance", model);
                }
                else if (ActionName == "ChangeStatus")
                {
                    return PartialView("ChangeStatus", model);
                }
            }
            return RedirectToAction("Index", "License");
        }

        public ActionResult Delete(int licenseID)
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
                    WarningNotification("Você não tem permissão para excluir um registro em Licença!");

                    return RedirectToAction("Index");
                }

                if (licenseID == 0)
                {
                    ErrorNotification(string.Format("Licença não pode ser excluída! "));

                    return Redirect("Index");
                }
                var model = new LicenseModel();

                Result<License> license = _licenseService.GetByID(licenseID);

                if (license.IsSuccess)
                {
                    model = license.Value.ToModel();

                    _licenseService.Delete(model.LicenseID);

                    SuccessNotification(string.Format("Licença excluida com sucesso! "));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("A licença contêm registros associados, exclua primeiro os registros.");

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(LicenseModel model)
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
                    WarningNotification("Você não tem permissão para atualizar um registro em Licença!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    if (Session["isAdmin"].ToString() != "False")
                    {
                        model.ExpirationDate = Convert.ToDateTime(DateTime.Today.AddMonths(3)).ToString("dd/MM/yyyy");
                    }
                    var command = MaintenanceLicenseCommand(model);

                    _licenseService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso! "));

                    return RedirectToAction("Index");
                }
                ErrorNotification("Não foi possível salvar atualização!");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                WarningNotification(ex.Message);

                throw;
            }
        }

        public ActionResult StatusChange(int licenseID)
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
                    WarningNotification("Você não tem permissão para alterar o status de um registro em Licenças!");

                    return RedirectToAction("Index");
                }

                if (Convert.ToString(Session["isAdmin"]) == "True")
                {
                    Result<License> license = _licenseService.GetByID(licenseID);

                    LicenseModel model = license.Value.ToModel();

                    var command = MaintenanceLicenseCommand(model);

                    if (command.License == null)
                    {
                        string Generatelicense = _licenseGeneratorService.Generate("00001", command.LicenseCode, command.ExpirationDate);

                        command.License = Generatelicense;
                        command.ApprovedByID = Convert.ToString(Session["userID"]);
                        command.ApprovedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                        _licenseService.Update(command);

                        SuccessNotification(string.Format("Licença gerada com sucesso! "));

                        return View();
                    }
                    else
                    {
                        command.License = null;
                        command.ApprovedByID = Convert.ToString(Session["userID"]);
                        command.ApprovedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                        _licenseService.Update(command);

                        SuccessNotification(string.Format("Registro alterado com sucesso! "));

                        return View();
                    }
                }
                WarningNotification("Você não tem permissão para alteração de registro!");

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