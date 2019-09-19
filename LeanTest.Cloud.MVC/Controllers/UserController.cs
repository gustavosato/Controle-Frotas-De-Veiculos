using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Users;
using Lean.Test.Cloud.Domain.Entities.Users;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;
using Lean.Test.Cloud.Domain.Command.CustomersUsers;
using System.Globalization;
using Lean.Test.Cloud.MVC.Models.SystemParameter;
using System.Web;
using System.IO;
using Lean.Test.Cloud.MVC.Models.Attachments;
using Lean.Test.Cloud.Domain.Command.Attachments;
using Newtonsoft.Json.Linq;
using System.Net;
using Mvc5_ReCaptcha.Models;
using Newtonsoft.Json;
using System.Web.Configuration;
using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.MVC.Models.Historicals;
using Lean.Test.Cloud.Domain.Command.Groups;
using Lean.Test.Cloud.MVC.Models.Groups;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain.Command.GroupsUsers;
using Lean.Test.Cloud.MVC.Models.Customers;
using Lean.Test.Cloud.Domain.Entities.Customers;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserService _userService1;
        private readonly ICustomerUserService _customerUserService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IExportManagerService _exportManagerService;
        private readonly IEncryptService _encryptService;
        private readonly IMailService _mailService;
        private readonly ISystemParameterService _systemParameterService;
        private readonly IStringUtilityService _stringUtilityService;
        private readonly IHistoricalService _historicalService;
        private readonly IAttachmentService _attachmentService;
        private readonly IProfilesService _profilesService;
        private readonly IGroupUserService _groupUserService;

        public UserController(IUserService userService,
                              IExportManagerService exportManagerService,
                              IParameterValueService parameterValueService,
                              IEncryptService encryptService,
                              IMailService mailService,
                              IUserService userService1,
                              IHistoricalService historicalService,
                              ICustomerService customerService,
                              ISystemParameterService systemParameterService,
                              IStringUtilityService stringUtilityService,
                              IAttachmentService attachmentService,
                              IProfilesService profilesService,
                              ICustomerUserService customerUser,
                              IGroupUserService groupUserService)

        {
            _userService = userService;
            _userService1 = userService1;
            _customerService = customerService;
            _customerUserService = customerUser;
            _parameterValueService = parameterValueService;
            _exportManagerService = exportManagerService;
            _encryptService = encryptService;
            _mailService = mailService;
            _systemParameterService = systemParameterService;
            _historicalService = historicalService;
            _attachmentService = attachmentService;
            _profilesService = profilesService;
            _stringUtilityService = stringUtilityService;
            _groupUserService = groupUserService;
        }

        private string SystemFeatureID = "100";

        public ActionResult CustomerAssociate(int userID)
        {
            var model = new UserModel();
            model.UserID = userID;
            Session["userAssociateID"] = userID;
            return PartialView("CustomerAssociate");
        }

        public ActionResult GroupAssociate(int userID)
        {
            var model = new UserModel();
            model.UserID = userID;
            Session["userAssociateID"] = userID;
            return PartialView("GroupAssociate");
        }

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Site");
            }

            var model = new UserModel();

            var Departmetns = _parameterValueService.GetAllByParameterID("4");
            var functions = _parameterValueService.GetAllByParameterID("1");

            model.LoadDepartments = Departmetns.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadFunctions = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddByUser(UserModel model)
        {
            try
            {
                Result<User> user;
                string password = _encryptService.GetHash(model.PasswordNew);

                var modelLocal = new UserModel();

                try
                {
                    user = _userService.GetByEmail(model.EmailNew);
                    modelLocal = user.Value.ToModel();
                }
                catch
                {
                    user = null;
                }

                if (user.IsSuccess)
                {
                    string isReCaptchaActivi = WebConfigurationManager.AppSettings["isReCaptchaActivi"];

                    if (isReCaptchaActivi == "true")
                    {
                        CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);

                        if (!response.Success)
                        {
                            //ViewBag.ErroCaptcha = "Google reCaptcha validação FALHOU !!! /n"
                            //    + response.ErrorMessage[0].ToString();
                            WarningNotification(string.Format("Favor validar reCAPTCHA !!!"));
                            return RedirectToAction("Index", "Site");
                        }
                    }
                    if (modelLocal == null)
                    {
                        model.Email = model.EmailNew;
                        model.Password = password;
                        model.FunctionID = "4";
                        model.FunctionLevelID = "12";
                        model.LevelClassificationID = "13";
                        model.DepartmentID = "16";
                        model.TotalCost = "0,00";
                        model.FirstAccess = "True";
                        model.SupervisorID = "2";
                        model.IsAdmin = false;
                        model.IsActive = false;
                        model.AccessToDate = Convert.ToDateTime(DateTime.Today.AddMonths(3)).ToString("dd/MM/yyyy");
                        model.UpdateRecordTo = Convert.ToDateTime(DateTime.Today.AddDays(-3)).ToString("dd/MM/yyyy");
                        model.ReleaseDateUpdateRecordTo = Convert.ToDateTime(DateTime.Today).ToString("dd/MM/yyyy");
                        model.StartJob = Convert.ToDateTime(DateTime.Today).ToString("dd/MM/yyyy");
                        model.EndJob = null;
                        model.DateOfBirth = null;
                        model.ContractTypeID = "89";
                        model.HourTypeID = "91";
                        model.IsEmployee = false;
                        model.CreatedByID = Convert.ToString(Session["userID"]);
                        model.CreationDate = Convert.ToString(DateTime.Now);

                        var command = MaintenanceUserCommand(model);

                        _userService.Add(command);

                        SuccessNotification(string.Format("Usuário criado com sucesso! Usuário: {0} - {1}. Aguarde a liberação do seu acesso pelo administrador.", model.UserName, model.Email));

                        return RedirectToAction("Index", "Site");
                    }
                    else
                    {
                        WarningNotification(string.Format("Este e-mail já está cadastrado! E-mail: {0}", model.Email));

                        return RedirectToAction("Index", "Site");
                    }
                }
                ErrorNotification(string.Format("Não foi possível criar o usuário : {0}", model.UserName));

                return RedirectToAction("Index", "Site");
            }
            catch
            {
                ErrorNotification(string.Format("Não foi possível criar o usuário : {0}", model.UserName));

                return RedirectToAction("Index", "Site");
            }

        }
        public static CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var user = new WebClient();
            var jsonResult = user.DownloadString(
                 string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                 secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());

        }

        [HttpPost]
        public ActionResult Add(UserModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para adicionar um usuário!");

                    return RedirectToAction("Index");
                }

                Result<User> user;

                string password = _stringUtilityService.RandomPassword(8);

                string passwordHash = _encryptService.GetHash(password);

                var modelLocal = new UserModel();

                try
                {
                    user = _userService.GetByEmail(model.Email);

                    modelLocal = user.Value.ToModel();
                }
                catch
                {
                    user = null;
                }

                if (user.IsSuccess)
                {
                    if (modelLocal == null)
                    {
                        model.Password = passwordHash;

                        model.FirstAccess = "true";

                        var command = MaintenanceUserCommand(model);

                        string recordID = _userService.Add(command);

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

                        string body = "Dados para acesso do novo usuário: \n \n Usuário: " + model.Email + " \n Senha: " + password;

                        var mailEmail = _systemParameterService.GetByID(2);

                        var passwordEmail = _systemParameterService.GetByID(3);

                        var mailEmailModel = new SystemParameterModel();

                        var passwordEmailModel = new SystemParameterModel();

                        mailEmailModel = mailEmail.Value.ToModel();

                        passwordEmailModel = passwordEmail.Value.ToModel();

                        var mail = _mailService.Send(mailEmailModel.ParamterValue, passwordEmailModel.ParamterValue, "support.leantest@rperformancegroup.com", model.Email, "RP Group LeanTest - Novo usuário", body, "");


                        SuccessNotification(string.Format("Usuário criado com sucesso! Usuário: {0} - {1}.", model.UserName, model.Email));

                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        WarningNotification(string.Format("Este e-mail já está cadastrado! E-mail: {0}", model.Email));

                        return RedirectToAction("Index", "User");
                    }
                }
                ErrorNotification(string.Format("Não foi possível criar o usuário : {0}", model.UserName));

                return RedirectToAction("Index", "User");
            }

            catch
            {
                ErrorNotification(string.Format("Não foi possível criar o usuário : {0}", model.UserName));

                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        public ActionResult GetUser(DataSourceRequest request, UserModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registros de apropriação de horas!");

                return Json(gridModel);
            }
            else
            {
                var users = _userService.GetAll(new FilterUserCommand
                {
                    UserName = model.SearchUserName,
                    Email = model.SearchEmail,
                    DepartmentID = model.SearchDepartmentID,
                    FunctionID = model.SearchFunctionID
                }, request.Page - 1, request.PageSize);

                gridModel = new DataSourceResult
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
        }
        [HttpPost]
        public ActionResult GetAllAssociateUserByCustomerID(DataSourceRequest request, UserModel model)
        {
            model.CustomerID = Session["customerAssociateID"].ToString();

            var users = _userService.GetAllAssociateUserByCustomerID(new FilterUserCommand
            {
                UserName = model.SearchUserName,
                CustomerID = model.CustomerID
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
        public ActionResult GetAllAssociateGroupByUserID(DataSourceRequest request, GroupModel model)

        {
            model.UserID = Session["userAssociateID"].ToString();

            var users = _groupUserService.GetAllAssociateGroupByUserID(new FilterGroupCommand
            {
                GroupName = model.SearchGroupName,
                UserID = model.UserID
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
        public ActionResult GetAllNoAssociateUserByCustomerID(DataSourceRequest request, UserModel model)
        {
            model.CustomerID = Session["customerAssociateID"].ToString();

            var users = _userService.GetAllNoAssociateUserByCustomerID(new FilterUserCommand
            {
                UserName = model.SearchUserName,
                CustomerID = model.CustomerID
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
        public ActionResult GetAllNoAssociateGroupByUserID(DataSourceRequest request, GroupModel model)
        {
            model.UserID = Session["userAssociateID"].ToString();

            var users = _groupUserService.GetAllNoAssociateGroupByUserID(new FilterGroupCommand
            {
                GroupName = model.SearchGroupName,
                UserID = model.UserID
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

        public ActionResult New()
        {
            var model = new UserModel();

            var Departmetns = _parameterValueService.GetAllByParameterID("4");
            var functions = _parameterValueService.GetAllByParameterID("1");
            var functionLevels = _parameterValueService.GetAllByParameterID("2");
            var levelClassifications = _parameterValueService.GetAllByParameterID("3");
            var contractTypes = _parameterValueService.GetAllByParameterID("19");
            var hourType = _parameterValueService.GetAllByParameterID("20");
            var typeBankAccount = _parameterValueService.GetAllByParameterID("29");
            var TypePersons = _parameterValueService.GetAllByParameterID("28");

            var supervisor = _userService1.GetAll(0);

            model.FirstAccess = "True";
            model.IsActive = true;
            model.UpdateRecordTo = Convert.ToDateTime(DateTime.Today.AddDays(-3)).ToString();
            model.ReleaseDateUpdateRecordTo = Convert.ToDateTime(DateTime.Today).ToString();
            model.AccessToDate = Convert.ToDateTime(DateTime.Today.AddMonths(3)).ToString();
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            model.LoadDepartments = Departmetns.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadFunctions = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadFunctionLevels = functionLevels.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadLevelClassifications = levelClassifications.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadContractTypes = contractTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadHourTypes = hourType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadSupervisors = supervisor.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadTypeBankAccounts = typeBankAccount.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadTypePersons = TypePersons.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return PartialView("Maintenance", model);
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

                return View();
            }
            else
            {
                _customerUserService.Delete(Convert.ToInt16(Session["customerAssociateID"]), userID);

                return View();
            }
        }

        public ActionResult DisassociateGroup(int groupID)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
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
                _groupUserService.Delete(groupID, Convert.ToInt16(Session["userAssociateID"]));

                return View();
            }
        }

        public ActionResult DisassociateCustomer(int customerID)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para desassociar uma empresa!");

                return View();
            }
            else {
                _customerUserService.Delete(customerID, Convert.ToInt16(Session["userAssociateID"]));

                return View();
            }
        }

        public ActionResult AssociateUser(int userID)
        {
            var command = new MaintenanceCustomerUserCommand();
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para associar um usuário!");

                return View();

            }
            else
            {
                command = new MaintenanceCustomerUserCommand();

                command.CustomerID = Convert.ToInt16(Session["customerAssociateID"]);
                command.UserID = userID;

                _customerUserService.Add(command);

                return View();
            }
        }

        public ActionResult AssociateGroup(int groupID)
        {
            var command = new MaintenanceGroupUserCommand();
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
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
                command = new MaintenanceGroupUserCommand();

                command.UserID = Convert.ToInt16(Session["userAssociateID"]);
                command.GroupID = groupID;

                _groupUserService.Add(command);

                return View();
            }
        }

        public ActionResult AssociateCustomer(int customerID)
        {
            var command = new MaintenanceCustomerUserCommand();
            //permissions
            if (_profilesService.GetAllow(new FilterProfileCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para associar uma empresa!");

                return View();
            }
            else
            {
                command = new MaintenanceCustomerUserCommand();

                command.UserID = Convert.ToInt16(Session["userAssociateID"]);
                command.CustomerID = customerID;

                _customerUserService.Add(command);

                return View();
            }
        }

        public JsonResult RememberPassword(string email)
        {
            string newPassword = _stringUtilityService.RandomPassword(12);

            var model = new UserModel();

            Result<User> localUser = _userService.GetByEmail(email);

            model = localUser.Value.ToModel();

            if (model != null)
            {
                string body = null;

                body = "Nova senha gerada: " + newPassword;
                model.Password = _encryptService.GetHash(newPassword);
                model.LastAccessDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                model.FirstAccess = "True";
                model.LastIPAccess = Server.HtmlEncode(Request.UserHostAddress);

                var command = MaintenanceUserCommand(model);

                _userService.Update(command);

                var mailEmail = _systemParameterService.GetByID(2);

                var passwordEmail = _systemParameterService.GetByID(3);

                var mailEmailModel = new SystemParameterModel();

                var passwordEmailModel = new SystemParameterModel();

                mailEmailModel = mailEmail.Value.ToModel();

                passwordEmailModel = passwordEmail.Value.ToModel();

                var mail = _mailService.Send(mailEmailModel.ParamterValue, passwordEmailModel.ParamterValue, "support.leantest@rperformancegroup.com", model.Email, "RP Group LeanTest - Recuperação de senha", body, "");

                return Json(new { success = false, responseText = string.Format("Sua nova senha foi enviada para o e-mail {0}", email) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, responseText = string.Format("Usuário não encontrado, e-mail {0}", email) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomers(string mail, string password)
        {
            var model = new UserModel();

            Result<User> localUser = _userService.GetByEmail(mail);

            if (localUser.IsSuccess)
            {
                model = localUser.Value.ToModel();

                if (model == null)
                {
                    if (!string.IsNullOrEmpty(mail))
                    {
                        return Json(new { success = false, responseText = string.Format("E-mail: {0}, não foi encontrado!", mail) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = string.Format("", mail) }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (string.IsNullOrEmpty(password))
                {
                    return Json(new { success = false, responseText = string.Format("A senha é obrigatória!") }, JsonRequestBehavior.AllowGet);
                }
                password = _encryptService.GetHash(password);
            }
            else
            {
                return Json(new { success = false, responseText = string.Format("E-mail não encontrado! E-mail: {0}", mail) }, JsonRequestBehavior.AllowGet);
            }

            if (model.Password == password)
            {
                if (model.IsActive == false)
                {
                    return Json(new { success = false, responseText = string.Format("Usuário inativo, procure o administrador! Usuário: {0}", model.UserName) }, JsonRequestBehavior.AllowGet);
                }

                if (model.FirstAccess == "True")
                {
                    return Json(new { success = false, responseText = string.Format("changePassword") }, JsonRequestBehavior.AllowGet);
                }

                DateTime accessToDate = DateTime.ParseExact(model.AccessToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (accessToDate < DateTime.Today)
                {
                    return Json(new { success = false, responseText = string.Format("Acesso expirado, procure o administrador! Usuário: {0}", model.UserName) }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, responseText = string.Format("Senha incorreta!") }, JsonRequestBehavior.AllowGet);
            }
            var customers = _customerService.GetAllAssociateCustomerByUserID(model.UserID.ToString(), "0");

            return Json(customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList());
        }

        public JsonResult UpdatePassword(string email, string password)
        {
            var model = new UserModel();

            Result<User> localUser = _userService.GetByEmail(email);

            model = localUser.Value.ToModel();

            if (model != null)
            {
                model.Password = _encryptService.GetHash(password);
                model.LastAccessDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                model.FirstAccess = "False";
                model.LastIPAccess = Server.HtmlEncode(Request.UserHostAddress);

                var command = MaintenanceUserCommand(model);

                _userService.Update(command);

                return Json(new { success = false, responseText = string.Format("Sua nova senha foi salva com sucesso!") }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, responseText = string.Format("Usuário não encontrado, e-mail {0}", email) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login(UserModel user)
        {
            var model = new UserModel();

            Result<User> localUser = _userService.GetByEmail(user.Email);

            model = localUser.Value.ToModel();

            Session["userID"] = model.UserID;

            string[] userName = model.UserName.Split(' ');

            //get customerName
            var customerModel = new CustomerModel();

            Result<Customer> localCustomer= _customerService.GetByID(Convert.ToInt32(user.CustomerID));

            customerModel = localCustomer.Value.ToModel();

            Session["userName"] = userName[0] + " - [" + customerModel.CustomerName + "]";

            Session["userFullName"] = model.UserName;

            Session["isAdmin"] = model.IsAdmin;

            Session["customerID"] = user.CustomerID;

            Session.Timeout = 60;

            model.LastAccessDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            if (!string.IsNullOrEmpty(user.PasswordNew))
            {
                model.Password = _encryptService.GetHash(user.PasswordNew);
            }
            model.FirstAccess = "False";

            model.LastIPAccess = Server.HtmlEncode(Request.UserHostAddress);

            var command = MaintenanceUserCommand(model);

            _userService.Update(command);

            return RedirectToAction("Index", "Demand");
        }

        public ActionResult LoadSignin()
        {
            var model = new UserModel();

            var customers = _customerService.GetAllAssociateCustomerByUserID("0", "0");

            model.LoadCustomers = customers.Select(x => new SelectListItem() { Text = x.customerName.ToString(), Value = x.customerID.ToString() }).ToList();

            return PartialView("Signin", model);
        }

        public ActionResult LoadSignup()
        {
            return PartialView("Signup");
        }

        public ActionResult Exit()
        {
            Session["userID"] = null;

            Session.Remove("userID");

            Session["userName"] = null;

            Session["isAdmin"] = false;

            Session["customerID"] = null;

            return RedirectToAction("Index", "Site");
        }

        private MaintenanceUserCommand MaintenanceUserCommand(UserModel model)
        {
            MaintenanceUserCommand command = new MaintenanceUserCommand();

            command.UserID = model.UserID;
            command.UserName = model.UserName;
            command.Password = model.Password;
            command.Email = model.Email;
            command.CellNumber = model.CellNumber;
            command.FunctionID = model.FunctionID;
            command.FunctionLevelID = model.FunctionLevelID;
            command.LevelClassificationID = model.LevelClassificationID;
            command.DepartmentID = model.DepartmentID;
            command.TotalCost = model.TotalCost;
            command.SupervisorID = model.SupervisorID;
            command.Description = model.Description;
            command.FirstAccess = model.FirstAccess;
            command.IsAdmin = model.IsAdmin;
            command.LastAccessDate = model.LastAccessDate;
            command.LastIPAccess = model.LastIPAccess;
            command.IsActive = model.IsActive;
            command.AccessToDate = model.AccessToDate;
            command.UpdateRecordTo = model.UpdateRecordTo;
            command.ReleaseDateUpdateRecordTo = model.ReleaseDateUpdateRecordTo;
            command.StartJob = model.StartJob;
            command.EndJob = model.EndJob;
            command.ContractTypeID = model.ContractTypeID;
            command.HourTypeID = model.HourTypeID;
            command.RG = model.RG;
            command.CPF = model.CPF;
            command.DateOfBirth = model.DateOfBirth;
            command.HomeAddress = model.HomeAddress;
            command.CEP = model.CEP;
            command.District = model.District;
            command.City = model.City;
            command.State = model.State;
            command.HomePhone = model.HomePhone;
            command.TypeBankAccountID = model.TypeBankAccount;
            command.TypePersonID = model.TypePerson;
            command.Agency = model.Agency;
            command.BankAccount = model.BankAccount;
            command.BankName = model.BankName;
            command.SocialReason = model.SocialReason;
            command.CNPJ = model.CNPJ;
            command.OptingSimple = model.OptingSimple;
            command.IsEmployee = model.IsEmployee;
            command.RegisteredCity = model.RegisteredCity;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int userID, string ActionName)
        {
            var model = new UserModel();

            Result<User> user = _userService.GetByID(userID);

            if (user.IsSuccess)
            {
                model = user.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var Departmetns = _parameterValueService.GetAllByParameterID("4");
                    var functions = _parameterValueService.GetAllByParameterID("1");
                    var functionLevels = _parameterValueService.GetAllByParameterID("2");
                    var levelClassifications = _parameterValueService.GetAllByParameterID("3");
                    var contractTypes = _parameterValueService.GetAllByParameterID("19");
                    var hourType = _parameterValueService.GetAllByParameterID("20");
                    var typeBankAccount = _parameterValueService.GetAllByParameterID("29");
                    var TypePersons = _parameterValueService.GetAllByParameterID("28");

                    model.LoadDepartments = Departmetns.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadFunctions = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadFunctionLevels = functionLevels.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadLevelClassifications = levelClassifications.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadContractTypes = contractTypes.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadHourTypes = hourType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadTypeBankAccounts = typeBankAccount.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadTypePersons = TypePersons.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    var supervisor = _userService1.GetAll(Convert.ToInt32(model.SupervisorID));
                    model.LoadSupervisors = supervisor.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
                else if (ActionName == "ChangeStatus")
                {
                    return PartialView("ChangeStatus", model);
                }
                else
                //AllowApropriate
                {
                    model.UpdateRecordTo = model.UpdateRecordTo;
                    model.ReleaseDateUpdateRecordTo = Convert.ToDateTime(DateTime.Today).ToString("dd/MM/yyyy");

                    return PartialView("AllowApropriate", model);
                }
            }
            return RedirectToAction("Index", "User");
        }

        public ActionResult Delete(int userID)
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
                    WarningNotification("Você não tem permissão para excluir um usuário!");

                    return RedirectToAction("Index");
                }

                if (userID == 0)
                {
                    ErrorNotification(string.Format("O usuário não pode ser excluído! "));

                    return Redirect("Index");
                }
                var model = new UserModel();

                Result<User> user = _userService.GetByID(userID);

                if (user.IsSuccess)
                {
                    model = user.Value.ToModel();

                    _userService.Delete(model.UserID);

                    _historicalService.Delete(SystemFeatureID, userID);

                    _attachmentService.Delete(SystemFeatureID, userID);

                    SuccessNotification(string.Format("Usuário excluido com sucesso!"));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ErrorNotification(string.Format("Existem outros registros associados a esse usuário, é necessário excluí-los."));
                return RedirectToAction("Index");
            }
        }

        public ActionResult ExportXmlAll(UserModel model)
        {
            try
            {
                var user = _userService.GetAll(new FilterUserCommand
                {
                    UserName = model.SearchUserName,
                    Email = model.SearchEmail,
                    DepartmentID = model.SearchDepartmentID,
                    FunctionID = model.SearchFunctionID
                });

                string xmlUsers = _exportManagerService.ExportUserXml(user);

                string fileName = string.Format("UsersList-{0}.xml", Guid.NewGuid().ToString());

                return new XmlDownloadResult(xmlUsers, fileName);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(UserModel model, HttpPostedFileBase file)
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
                    WarningNotification("Você não tem permissão para atualizar registros de um usuário!");

                    return RedirectToAction("Index");
                }

                Result<User> user;

                var modelLocal = new UserModel();

                try
                {
                    //historical
                    Historical(model);

                    user = _userService.GetByEmail(model.Email);

                    modelLocal = user.Value.ToModel();
                }
                catch
                {
                    user = null;
                }

                var command = MaintenanceUserCommand(model);

                if (modelLocal == null)
                {
                    _userService.Update(command);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.UserID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                        var dir = new DirectoryInfo(newPath);

                        if (!dir.Exists) dir.Create();

                        var path = Path.Combine(newPath, fileName);

                        var size = (file.ContentLength / 1024) + "KB";

                        file.SaveAs(path);

                        var attachmentModel = new AttachmentModel();

                        attachmentModel.Description = model.Description + "\n\n" + "Empresa: " + model.CustomerID + " - Usuário: " + model.DemandID;
                        attachmentModel.FileName = fileName;
                        attachmentModel.PathFile = path;
                        attachmentModel.RecordID = model.UserID.ToString();
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
                else
                {
                    if (modelLocal.UserID == model.UserID && modelLocal.Email == model.Email) //valida caso email e alterado e existe na base
                    {
                        _userService.Update(command);

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);

                            string newPath = Server.MapPath("~/App_Data/Uploads/" + SystemFeatureID + "/" + model.UserID + "/" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                            var dir = new DirectoryInfo(newPath);

                            if (!dir.Exists) dir.Create();

                            var path = Path.Combine(newPath, fileName);

                            var size = (file.ContentLength / 1024) + "KB";

                            file.SaveAs(path);

                            var attachmentModel = new AttachmentModel();

                            attachmentModel.Description = model.Description + "\n\n" + "Empresa: " + model.CustomerID + " - Usuário: " + model.DemandID;
                            attachmentModel.FileName = fileName;
                            attachmentModel.PathFile = path;
                            attachmentModel.RecordID = model.UserID.ToString();
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
                    else
                    {
                        WarningNotification(string.Format("Este e-mail já está cadastrado! Utilize outro e-mail. E-mail informado: {0}", model.Email));

                        return RedirectToAction("Index", "User");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);

                throw;
            }
        }

        private bool change_Historical_Status;

        [HttpPost]
        public ActionResult ChangeStatus(UserModel model)
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
                    WarningNotification("Você não tem permissão para alterar o status de um usuário!");

                    return RedirectToAction("Index");
                }

                change_Historical_Status = true;

                var command = MaintenanceUserCommand(model);

                if (command.IsActive == true)
                {
                    command.IsActive = false;

                    _userService.Update(command);

                    Historical(model);

                    SuccessNotification(string.Format("Usuário desativado com sucesso! Registro: {0} - {1}", model.UserName, model.Email));

                    return RedirectToAction("Index");
                }
                else
                {
                    command.IsActive = true;

                    command.AccessToDate = Convert.ToDateTime(DateTime.Today.AddMonths(3)).ToString("dd/MM/yyyy");

                    _userService.Update(command);

                    Historical(model);

                    SuccessNotification(string.Format("Usuário ativado com sucesso! Registro: {0} - {1}. Usuário ativado até: {2}", command.UserName, command.Email, command.AccessToDate));

                    return RedirectToAction("Index");
                }
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


        private void Historical(UserModel model)
        {
            var command = new UserModel();

            var modelHistorical = new HistoricalModel();

            var LocalCommand = _userService.GetByID(model.UserID);

            command = LocalCommand.Value.ToModel();

            if (command.SupervisorID != model.SupervisorID)
            {
                string commandSupervisorID = _userService.GetUserNameByID(Convert.ToInt32(command.SupervisorID));

                string modelSupervisorID = _userService.GetUserNameByID(Convert.ToInt32(model.SupervisorID));

                AddHistorical(commandSupervisorID, modelSupervisorID, "Supervisor", model.UserID.ToString());
            }
            if (command.UserName != model.UserName) AddHistorical(command.UserName, model.UserName, "Nome", model.UserID.ToString());
            if (command.Email != model.Email) AddHistorical(command.Email, model.Email, "E-mail", model.UserID.ToString());
            if (command.AccessToDate != model.AccessToDate)
                if (change_Historical_Status == true)
                {
                    AddHistorical(model.AccessToDate, command.AccessToDate, "Data limite para acesso", model.UserID.ToString());
                }
                else
                {
                    AddHistorical(command.AccessToDate, model.AccessToDate, "Data limite para acesso", model.UserID.ToString());
                }
            if (command.UpdateRecordTo != model.UpdateRecordTo) AddHistorical(command.UpdateRecordTo, model.UpdateRecordTo, "Data retroativa limite", model.UserID.ToString());
            if (command.ReleaseDateUpdateRecordTo != model.ReleaseDateUpdateRecordTo) AddHistorical(command.ReleaseDateUpdateRecordTo, model.ReleaseDateUpdateRecordTo, "Validade da liberação", model.UserID.ToString());
            //if (command.Description != model.Description) AddHistorical(command.Description, model.Description, "Descrição", model.UserID.ToString());
            if (command.DepartmentID != model.DepartmentID) AddHistorical(command.DepartmentID, model.DepartmentID, "Departamento", model.UserID.ToString(), true);
            if (command.FunctionID != model.FunctionID) AddHistorical(command.FunctionID, model.FunctionID, "Função", model.UserID.ToString(), true);
            if (command.FunctionLevelID != model.FunctionLevelID) AddHistorical(command.FunctionLevelID, model.FunctionLevelID, "Nível", model.UserID.ToString(), true);
            if (command.LevelClassificationID != model.LevelClassificationID) AddHistorical(command.LevelClassificationID, model.LevelClassificationID, "Classificação", model.UserID.ToString(), true);
            if (command.ContractTypeID != model.ContractTypeID) AddHistorical(command.ContractTypeID, model.ContractTypeID, "Tipo de Contrato", model.UserID.ToString(), true);
            if (command.HourTypeID != model.HourTypeID) AddHistorical(command.HourTypeID, model.HourTypeID, "Tipo de Hora", model.UserID.ToString(), true);
            if (command.StartJob != model.StartJob) AddHistorical(command.StartJob, model.StartJob, "Data da contratação", model.UserID.ToString());
            if (command.EndJob != model.EndJob) AddHistorical(command.EndJob, model.EndJob, "Data do desligamento", model.UserID.ToString());
            if (command.RG != model.RG) AddHistorical(command.RG, model.RG, "Número do RG", model.UserID.ToString());
            if (command.CPF != model.CPF) AddHistorical(command.CPF, model.CPF, "CPF", model.UserID.ToString());
            if (command.DateOfBirth != model.DateOfBirth) AddHistorical(command.DateOfBirth, model.DateOfBirth, "Data de nascimento", model.UserID.ToString());
            if (command.HomeAddress != model.HomeAddress) AddHistorical(command.HomeAddress, model.HomeAddress, "Endereço residêncial", model.UserID.ToString());
            if (command.CEP != model.CEP) AddHistorical(command.CEP, model.CEP, "CEP", model.UserID.ToString());
            if (command.District != model.District) AddHistorical(command.District, model.District, "Bairro", model.UserID.ToString());
            if (command.City != model.City) AddHistorical(command.City, model.City, "Cidade", model.UserID.ToString());
            if (command.State != model.State) AddHistorical(command.State, model.State, "Estado", model.UserID.ToString());
            if (command.CellNumber != model.CellNumber) AddHistorical(command.CellNumber, model.CellNumber, "Celular", model.UserID.ToString());
            if (command.HomePhone != model.HomePhone) AddHistorical(command.HomePhone, model.HomePhone, "Telefone Residêncial", model.UserID.ToString());
            if (command.TotalCost != model.TotalCost) AddHistorical(command.TotalCost, model.TotalCost, "Custo Total", model.UserID.ToString());
            if (command.TypePerson != model.TypePerson) AddHistorical(command.TypePerson, model.TypePerson, "Tipo de Pessoa", model.UserID.ToString(), true);
            if (command.BankName != model.BankName) AddHistorical(command.BankName, model.BankName, "Nome do Banco", model.UserID.ToString());
            if (command.TypeBankAccount != model.TypeBankAccount) AddHistorical(command.TypeBankAccount, model.TypeBankAccount, "Tipo de Conta Bancária", model.UserID.ToString(), true);
            if (command.Agency != model.Agency) AddHistorical(command.Agency, model.Agency, "Agência", model.UserID.ToString());
            if (command.BankAccount != model.BankAccount) AddHistorical(command.BankAccount, model.BankAccount, "Número da conta bancária", model.UserID.ToString());
            if (command.SocialReason != model.SocialReason) AddHistorical(command.SocialReason, model.SocialReason, "Razão Social", model.UserID.ToString());
            if (command.CNPJ != model.CNPJ) AddHistorical(command.CNPJ, model.CNPJ, "CNPJ", model.UserID.ToString());
            if (command.RegisteredCity != model.RegisteredCity) AddHistorical(command.RegisteredCity, model.RegisteredCity, "Cidade de Registro", model.UserID.ToString());

            if (command.IsActive != model.IsActive)
            {
                string status1;
                string status2;

                if (command.IsActive)
                {
                    status1 = "Ativado"; status2 = "Desativado";
                }
                else
                {
                    status1 = "Desativado"; status2 = "Ativado";
                }
                if (change_Historical_Status == true)
                {
                    AddHistorical(status2, status1, "Usuário Ativo", model.UserID.ToString());
                }
                else
                {
                    AddHistorical(status1, status2, "Usuário Ativo", model.UserID.ToString());
                }
            }

            if (command.OptingSimple != model.OptingSimple)
            {
                string status1;
                string status2;

                if (command.OptingSimple)
                {
                    status1 = "Ativado"; status2 = "Desativado";
                }
                else
                {
                    status1 = "Desativado"; status2 = "Ativado";
                }

                AddHistorical(status1, status2, "Optante pelo simples", model.UserID.ToString());
            }
            if (command.IsAdmin != model.IsAdmin)
            {
                string status1;
                string status2;

                if (command.IsAdmin)
                {
                    status1 = "Ativado"; status2 = "Desativado";
                }
                else
                {
                    status1 = "Desativado"; status2 = "Ativado";
                }

                AddHistorical(status1, status2, "Administrador", model.UserID.ToString());
            }
            if (command.IsEmployee != model.IsEmployee)
            {
                string status1;
                string status2;

                if (command.IsAdmin)
                {
                    status1 = "Ativado"; status2 = "Desativado";
                }
                else
                {
                    status1 = "Desativado"; status2 = "Ativado";
                }

                AddHistorical(status1, status2, "Funcionário", model.UserID.ToString());
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

    }
}