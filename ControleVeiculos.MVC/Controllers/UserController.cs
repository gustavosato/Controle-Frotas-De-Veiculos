using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Users;
using ControleVeiculos.Domain.Entities.Users;
using ControleVeiculos.Domain;
using ControleVeiculos.MVC.Infrastructure;
using System.Globalization;
using ControleVeiculos.MVC.Models.SystemParameter;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Web.Configuration;
using ControleVeiculos.Infrastructure.Mvc;

namespace ControleVeiculos.MVC.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserService _userService1;
        private readonly IParameterValueService _parameterValueService;
        private readonly ISystemParameterService _systemParameterService;
        //private readonly IEncryptyService _encryptyService;
        private readonly IStringUtilityService _stringUtilityService;


        public UserController(IUserService userService,
                              IParameterValueService parameterValueService,
                              IUserService userService1,
          //                    IEncryptyService encryptyService,
                              IStringUtilityService stringUtlilityService,
                              ISystemParameterService systemParameterService)
        {
            _userService = userService;
            _userService1 = userService1;
            _parameterValueService = parameterValueService;
            _systemParameterService = systemParameterService;
            //_encryptyService = encryptyService;
            _stringUtilityService = stringUtlilityService;
        }

        private string SystemFeatureID = "0";

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Site");
            }

            var model = new UserModel();

            var Departamentos = _parameterValueService.GetAllByParameterID("4");

            model.LoadDepartamentos = Departamentos.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddByUser(UserModel model)
        {
            try
            {
                Result<User> user;
               //string password = _encryptyService.GetHash(model.PasswordNew);

                var modelLocal = new UserModel();

                try
                {
                    user = _userService.GetByEmail(model.EmailNew);
                    modelLocal = user.Value.ToModel();
                }
                catch
                {
                    user = null;
                    //var mensagem = ex.Message;
                }

                if (user.IsSuccess)
                {
                    if (modelLocal == null)
                    {
                        model.Email = model.EmailNew;
                        model.Password = model.PasswordNew;
                        model.DepartamentoID = null;
                        model.FirstAccess = "True";
                        model.IsAdmin = false;
                        model.IsActive = false;
                        model.DateOfBirth = null;

                        var command = MaintenanceUserCommand(model);

                        _userService.Add(command);

                        SuccessNotification(string.Format("Usuário criado com sucesso! Usuário: {0} - {1}.", model.UserName, model.Email));

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

        [HttpPost]
        public ActionResult Add(UserModel model, HttpPostedFileBase file)
        {
            try
            {
                Result<User> user;

                string password = _stringUtilityService.RandomPassword(8);

                //string passwordHash = _encryptyService.GetHash(password);

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
                        model.Password = model.Password;

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

                        }

                        string body = "Dados para acesso do novo usuário: \n \n Usuário: " + model.Email + " \n Senha: " + password;

                        var mailEmail = _systemParameterService.GetByID(2);

                        var passwordEmail = _systemParameterService.GetByID(3);

                        var mailEmailModel = new SystemParameterModel();

                        var passwordEmailModel = new SystemParameterModel();

                        mailEmailModel = mailEmail.Value.ToModel();

                        passwordEmailModel = passwordEmail.Value.ToModel();

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
            {
                var users = _userService.GetAll(new FilterUserCommand
                {
                    UserName = model.SearchUserName,
                    Email = model.SearchEmail,
                    DepartamentoID = model.SearchDepartamentoID,
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

        public ActionResult New()
        {
            var model = new UserModel();

            var Departamentos = _parameterValueService.GetAllByParameterID("4");

            var supervisor = _userService1.GetAll(0);

            model.FirstAccess = "True";
            model.IsActive = true;

            model.LoadDepartamentos = Departamentos.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return PartialView("Maintenance", model);
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
                model.Password = model.Password;
                model.FirstAccess = "True";

                var command = MaintenanceUserCommand(model);

                _userService.Update(command);

                var mailEmail = _systemParameterService.GetByID(2);

                var passwordEmail = _systemParameterService.GetByID(3);

                var mailEmailModel = new SystemParameterModel();

                var passwordEmailModel = new SystemParameterModel();

                mailEmailModel = mailEmail.Value.ToModel();

                passwordEmailModel = passwordEmail.Value.ToModel();

                return Json(new { success = false, responseText = string.Format("Sua nova senha foi enviada para o e-mail {0}", email) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, responseText = string.Format("Usuário não encontrado, e-mail {0}", email) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePassword(string email, string password)
        {
            var model = new UserModel();

            Result<User> localUser = _userService.GetByEmail(email);

            model = localUser.Value.ToModel();

            if (model != null)
            {
                model.Password = model.Password;
                model.FirstAccess = "False";

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

            Session["userName"] = userName[0];

            Session["userFullName"] = model.UserName;

            Session["isAdmin"] = model.IsAdmin;

            Session.Timeout = 60;

            if (!string.IsNullOrEmpty(user.PasswordNew))
            {
                //model.Password = _encryptyService.GetHash(user.PasswordNew);
            }
            model.FirstAccess = "False";

            var command = MaintenanceUserCommand(model);

            _userService.Update(command);

            return RedirectToAction("Index", "Funcionario");
        }

        public ActionResult LoadSignin()
        {
            var model = new UserModel();

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
            command.DepartamentoID = model.DepartamentoID;
            command.FirstAccess = model.FirstAccess;
            command.IsAdmin = model.IsAdmin;
            command.IsActive = model.IsActive;
            command.RG = model.RG;
            command.CPF = model.CPF;
            command.DateOfBirth = model.DateOfBirth;
            command.HomeAddress = model.HomeAddress;
            command.CEP = model.CEP;
            command.District = model.District;
            command.City = model.City;
            command.State = model.State;
            command.HomePhone = model.HomePhone;

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
                    var Departamentos = _parameterValueService.GetAllByParameterID("4");

                    model.LoadDepartamentos = Departamentos.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

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
                    return PartialView("AllowApropriate", model);
                }
            }
            return RedirectToAction("Index", "User");
        }

        public ActionResult Delete(int userID)
        {
            try
            {
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

        [HttpPost]
        public ActionResult Update(UserModel model, HttpPostedFileBase file)
        {
            try
            {
                Result<User> user;

                var modelLocal = new UserModel();
                var command = MaintenanceUserCommand(model);

                if (modelLocal == null)
                {
                    _userService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso! "));

                    return RedirectToAction("Index");
                }
                else
                {
                    if (modelLocal.UserID == model.UserID && modelLocal.Email == model.Email) //valida caso email e alterado e existe na base
                    {
                        _userService.Update(command);

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
    }
}