using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Features;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Features;
using Lean.Test.Cloud.Domain.Entities.Features;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class FeatureController : BaseController
    {
        private readonly IFeatureService _featureService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IApplicationSystemService _applicationSystemService;
        private readonly IProfilesService _profilesService;
        private readonly IUserService _userService;

        public FeatureController(IFeatureService featureService, 
                                    IParameterValueService parameterValueService,
                                    IApplicationSystemService applicationSystemService,
                                    IProfilesService profilesService,
                                    IUserService userService)
        {
            _featureService = featureService;
            _parameterValueService = parameterValueService;
            _applicationSystemService = applicationSystemService;
            _profilesService = profilesService;
            _userService = userService;
        }

        private string SystemFeatureID = "207";

        public ActionResult Index()
        {
            var model = new FeatureModel();

            var applicationSystems = _applicationSystemService.GetAll(Convert.ToInt32(Session["customerID"]));

            model.SearchLoadApplications = applicationSystems.Select(x => new SelectListItem() { Text = x.applicationSystemName.ToString(), Value = x.applicationSystemID.ToString() }).ToList();

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(FeatureModel model)
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
                    WarningNotification("Você não tem permissão para adicionar uma funcionalidade!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceFeatureCommand(model);

                    _featureService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "Feature");
                }

                ErrorNotification(string.Format("Não foi possível criar a funcionalidade!"));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível criar a funcionalidade!"));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, FeatureModel model)
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
                WarningNotification("Você não tem permissão para visualizar as funcionalidades!");

                return Json(gridModel);
            }
            else
            {

                int userID = Convert.ToInt32(Session["userID"]);

                var features = _featureService.GetAll(userID, new FilterFeatureCommand
                {
                    FeatureName = model.SearchFeatureName,
                    ApplicationSystemID = model.SearchApplicationSystemID
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = features.Select(x =>
                    {
                        var featuresModel = x.ToModel();

                        return featuresModel;
                    }),
                    Total = features.TotalCount
                };
                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new FeatureModel();

            var typeFeatures = _parameterValueService.GetAllByParameterID("24");
            var statusFeatures = _parameterValueService.GetAllByParameterID("23");
            var applicationSystems = _applicationSystemService.GetAll(Convert.ToInt32(Session["customerID"]));
            var developers = _userService.GetAll(0);

            model.TargetDate = DateTime.Today.ToString("dd/MM/yyyy");
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            model.LoadFeatureTypes = typeFeatures.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadStatus = statusFeatures.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadApplications = applicationSystems.Select(x => new SelectListItem() { Text = x.applicationSystemName.ToString(), Value = x.applicationSystemID.ToString() }).ToList();
            model.LoadDevelopers = developers.Select(x => new SelectListItem() { Text = x.userName, Value = x.userID.ToString() }).ToList();

            return PartialView("Maintenance", model);
        }

        public ActionResult LoadActiveDesactiveCustomer()
        {
            return PartialView("ActiveDesactiveCustomer");
        }

        private MaintenanceFeatureCommand MaintenanceFeatureCommand(FeatureModel model)
        {
            MaintenanceFeatureCommand command = new MaintenanceFeatureCommand();

            command.FeatureID = model.FeatureID;
            command.FeatureName = model.FeatureName;
            command.Description = model.Description;
            command.FeatureTypeID = model.FeatureTypeID;
            command.ApplicationSystemID = model.ApplicationSystemID;
            command.StatusID = model.StatusID;
            command.DeveloperID = model.DeveloperID;
            command.MetaScript = model.MetaScript;
            command.AutomationScript = model.AutomationScript;
            command.TestPoints = model.TestPoints;
            command.TimeEffort = model.TimeEffort;
            command.TargetDate = model.TargetDate;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int featureID, string ActionName)
        {
            var model = new FeatureModel();

            var typeFeatures = _parameterValueService.GetAllByParameterID("24");
            var statusFeatures = _parameterValueService.GetAllByParameterID("23");
            var applicationSystems = _applicationSystemService.GetAll(Convert.ToInt32(Session["customerID"]));

            Result<Feature> feature = _featureService.GetByID(featureID);

            if (feature.IsSuccess)
            {
                model = feature.Value.ToModel();

                model.LoadFeatureTypes = typeFeatures.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                model.LoadStatus = statusFeatures.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                model.LoadApplications = applicationSystems.Select(x => new SelectListItem() { Text = x.applicationSystemName.ToString(), Value = x.applicationSystemID.ToString() }).ToList();

                var developers = _userService.GetAll(Convert.ToInt32(model.DeveloperID));
                model.LoadDevelopers = developers.Select(x => new SelectListItem() { Text = x.userName, Value = x.userID.ToString() }).ToList();

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
            return RedirectToAction("Index", "Feature");
        }

        public ActionResult Delete(int featureID)
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
                    WarningNotification("Você não tem permissão para excluir uma funcionalidade!");

                    return RedirectToAction("Index");
                }

                if (featureID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir funcionalidade! "));
                    return Redirect("Index");
                }
                var model = new FeatureModel();

                Result<Feature> feature = _featureService.GetByID(featureID);

                if (feature.IsSuccess)
                {
                    model = feature.Value.ToModel();

                    _featureService.Delete(model.FeatureID);

                    SuccessNotification(string.Format("Funcionalidade excluída com sucesso!"));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("A funcionalidade contêm elementos associados, exclua primeiro os elementos.");

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(FeatureModel model)
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
                    WarningNotification("Você não tem permissão para atualizar uma funcionalidade!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceFeatureCommand(model);

                    _featureService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso!"));

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
    }
}