using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.SystemMenus;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.SystemMenus;
using ControleVeiculos.Domain.Entities.SystemMenus;
using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Command.Profiles;
//using ControleVeiculos.MVC.Infrastructure.Mvc;


namespace ControleVeiculos.MVC.Controllers
{
    public class SystemMenuController : BaseController
    {
        private readonly ISystemMenuService _systemMenuService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly IProfilesService _profilesService;


        public SystemMenuController(ISystemMenuService systemMenuService,
                                IParameterValueService parameterValueService,
                                IProfilesService profilesService,
                                IUserService userService,
                                ISystemFeatureService systemFeatureService)
        {
            _systemMenuService = systemMenuService;
            _parameterValueService = parameterValueService;
            _profilesService = profilesService;
            _userService = userService;
            _systemFeatureService = systemFeatureService;
        }

        private string SystemFeatureID = "106";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new SystemMenuModel();

            var systemFeatures = _systemFeatureService.GetAll();
            var users = _userService.GetAll(0);

            model.SearchLoadSystemFeatures = systemFeatures.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SystemMenuModel model)
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
                    WarningNotification("Você não tem permissão para adicionar um registro em Menus!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceSystemMenuCommand(model);

                    _systemMenuService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "SystemMenu");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro! "));

                return RedirectToAction("Index", "SystemMenu");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro! ", model.TextMenu));

                return RedirectToAction("Index", "SystemMenu");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, SystemMenuModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registros de Menus!");

                return Json(gridModel);
            }
            else
            {
                var systemMenus = _systemMenuService.GetAll(new FilterSystemMenuCommand
                {
                    SystemFeatureID = model.SearchSystemFeature,

                }, request.Page - 1, request.PageSize);

                gridModel = new DataSourceResult
                {
                    Data = systemMenus.Select(x =>
                    {
                        var systemMenusModel = x.ToModel();

                        return systemMenusModel;
                    }),
                    Total = systemMenus.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new SystemMenuModel();

            var systemFeatures = _systemFeatureService.GetAll();
            var users = _userService.GetAll(0);

            model.LoadSystemFeatures = systemFeatures.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }


        private MaintenanceSystemMenuCommand MaintenanceSystemMenuCommand(SystemMenuModel model)
        {
            MaintenanceSystemMenuCommand command = new MaintenanceSystemMenuCommand();

            command.MenuID = model.MenuID;
            command.TextMenu = model.TextMenu;
            command.Description = model.Description;
            command.Ordem = model.Ordem;
            command.UrlAction = model.UrlAction;
            command.Controller = model.Controller;
            command.Icon = model.Icon;
            command.ItsAdmin = model.ItsAdmin;
            command.SystemFeatureID = model.SystemFeatureID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int menuID, string ActionName)
        {
            var model = new SystemMenuModel();

            Result<SystemMenu> systemMenu = _systemMenuService.GetByID(menuID);

            if (systemMenu.IsSuccess)
            {
                model = systemMenu.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var systemFeatures = _systemFeatureService.GetAll();

                    model.LoadSystemFeatures = systemFeatures.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);
                    ActionName = null;
                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "SystemMenu");
        }

        public ActionResult Delete(int menuID)
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
                    WarningNotification("Você não tem permissão para excluir um registro em Menus!");

                    return RedirectToAction("Index");
                }
                if (menuID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new SystemMenuModel();

                Result<SystemMenu> systemMenu = _systemMenuService.GetByID(menuID);

                if (systemMenu.IsSuccess)
                {
                    model = systemMenu.Value.ToModel();


                    _systemMenuService.Delete(model.MenuID);

                    SuccessNotification(string.Format("Registro excluído com sucesso! "));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("O registro não pode ser excluído! ");

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(SystemMenuModel model)
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
                    WarningNotification("Você não tem permissão para atualizar um registro em Menus!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceSystemMenuCommand(model);

                    _systemMenuService.Update(command);

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
    }
}