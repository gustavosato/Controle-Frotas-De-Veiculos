using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.ApplicationSystems;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.ApplicationSystems;
using ControleVeiculos.Domain.Entities.ApplicationSystems;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;


namespace ControleVeiculos.MVC.Controllers
{
    public class ApplicationSystemController : BaseController
    {
        private readonly IApplicationSystemService _applicationSystemService;
        private readonly IProfilesService _profilesService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;

        public ApplicationSystemController(IApplicationSystemService applicationSystemService,
                                           ICustomerService customerService,
                                           IProfilesService profilesService,
                                           IParameterValueService parameterValueService)
        {
            _applicationSystemService = applicationSystemService;
            _customerService = customerService;
            _profilesService = profilesService;
            _parameterValueService = parameterValueService;
        }

        private string SystemFeatureID = "206";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Add(ApplicationSystemModel model)
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
                    WarningNotification("Você não tem permissão para adicionar uma aplicação!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceApplicationSystemCommand(model);

                    _applicationSystemService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso!", model.ApplicationSystemName));

                    return RedirectToAction("Index", "ApplicationSystem");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro!", model.ApplicationSystemName));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!", model.ApplicationSystemName));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ApplicationSystemModel model)
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
                WarningNotification("Você não tem permissão para visualizar as aplicações!");

                return Json(gridModel);
            }
            else
            {
                var applicationSystems = _applicationSystemService.GetAll(new FilterApplicationSystemCommand
                {
                    ApplicationSystemName = model.SearchApplicationSystemName
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = applicationSystems.Select(x =>
                    {
                        var applicationSystemsModel = x.ToModel();

                        return applicationSystemsModel;
                    }),
                    Total = applicationSystems.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new ApplicationSystemModel();
            var typeApplicationSystems = _parameterValueService.GetAllByParameterID("21");

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            model.LoadApplicationType = typeApplicationSystems.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return PartialView("Maintenance", model);
        }

        public ActionResult LoadActiveDesactiveCustomer()
        {
            return PartialView("ActiveDesactiveCustomer");
        }

        private MaintenanceApplicationSystemCommand MaintenanceApplicationSystemCommand(ApplicationSystemModel model)
        {
            MaintenanceApplicationSystemCommand command = new MaintenanceApplicationSystemCommand();

            command.ApplicationSystemID = model.ApplicationSystemID;
            command.ApplicationSystemName = model.ApplicationSystemName;
            command.Description = model.Description;
            command.ApplicationTypeID = model.ApplicationTypeID;
            command.CustomerID = Convert.ToString(Session["customerID"]);
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int applicationSystemID, string ActionName)
        {
            var model = new ApplicationSystemModel();

            var typeApplicationSystems = _parameterValueService.GetAllByParameterID("21");
            
            Result<ApplicationSystem> applicationSystem = _applicationSystemService.GetByID(applicationSystemID);

            if (applicationSystem.IsSuccess)
            {
                model = applicationSystem.Value.ToModel();

                model.LoadApplicationType = typeApplicationSystems.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                if (ActionName == "Delete")
                    
                {
                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "ApplicationSystem");
        }

        public ActionResult Delete(int applicationSystemID)
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
                    WarningNotification("Você não tem permissão para excluir uma aplicação!");

                    return RedirectToAction("Index");
                }

                if (applicationSystemID == 0)
                {
                    ErrorNotification(string.Format("Registro não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new ApplicationSystemModel();

                Result<ApplicationSystem> applicationSystem = _applicationSystemService.GetByID(applicationSystemID);

                if (applicationSystem.IsSuccess)
                {
                    model = applicationSystem.Value.ToModel();


                    _applicationSystemService.Delete(model.ApplicationSystemID);

                    SuccessNotification(string.Format("Registro excluído com sucesso!", model.ApplicationSystemName));

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
        public ActionResult Update(ApplicationSystemModel model)
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
                    WarningNotification("Você não tem permissão para atualizar uma aplicação!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceApplicationSystemCommand(model);

                    _applicationSystemService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso!"));

                    return RedirectToAction("Index");
                }

                ErrorNotification("Não foi possível salvar alteração!");

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