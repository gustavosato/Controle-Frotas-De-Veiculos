using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.SystemFeatures;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.SystemFeatures;
using ControleVeiculos.Domain.Entities.SystemFeatures;
using ControleVeiculos.Domain;

namespace ControleVeiculos.MVC.Controllers
{
    public class SystemFeatureController : BaseController
    {
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly IParameterValueService _parameterValueService;

        public SystemFeatureController(ISystemFeatureService systemFeatureService,
                                    IParameterValueService parameterValueService)
        {
            _systemFeatureService = systemFeatureService;
            _parameterValueService = parameterValueService;

        }

        private string SystemFeatureID = "111";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new SystemFeatureModel();

            var systemFeatureType = _parameterValueService.GetAllByParameterID("111100");
            model.SearchLoadSystemFeatureType = systemFeatureType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SystemFeatureModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceSystemFeatureCommand(model);

                    _systemFeatureService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "SystemFeature");
                }

                ErrorNotification(string.Format("Não foi possível realizar registro da funcioanalidade! "));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Erro ao tentar registrar funcionalidade! "));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, SystemFeatureModel model)
        {
            var gridModel = new DataSourceResult();

            {
                var systemFeatures = _systemFeatureService.GetAll(new FilterSystemFeatureCommand
                {
                    SystemFeatureName = model.SearchSystemFeature,
                    SystemFeatureTypeID = model.SearchSystemFeatureTypeID
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = systemFeatures.Select(x =>
                    {
                        var systemFeatureModel = x.ToModel();

                        return systemFeatureModel;
                    }),
                    Total = systemFeatures.TotalCount
                };

                return Json(gridModel);
            }
        }
        public ActionResult New()
        {
            var model = new SystemFeatureModel();

            var systemFeatureType = _parameterValueService.GetAllByParameterID("111100");

            model.LoadSystemFeatureType= systemFeatureType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); ;

            return PartialView("Maintenance", model);
        }

        private MaintenanceSystemFeatureCommand MaintenanceSystemFeatureCommand(SystemFeatureModel model)
        {
            MaintenanceSystemFeatureCommand command = new MaintenanceSystemFeatureCommand();

            command.SystemFeatureID= model.SystemFeatureID;
            command.SystemFeatureName = model.SystemFeatureName;
            command.SystemFeatureTypeID = model.SystemFeatureTypeID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int systemFeatureID, string ActionName)
        {
            var model = new SystemFeatureModel();

            Result<SystemFeature> SystemFeature = _systemFeatureService.GetByID(systemFeatureID);

            if (SystemFeature.IsSuccess)
            {
                model = SystemFeature.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var systemFeature = _parameterValueService.GetAllByParameterID("111100");

                    model.LoadSystemFeatureType = systemFeature.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "SystemFeature");
        }

        public ActionResult Delete(int systemFeatureID)
        {
            try
            {
                if (systemFeatureID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new SystemFeatureModel();

                Result<SystemFeature> systemFeature = _systemFeatureService.GetByID(systemFeatureID);

                if (systemFeature.IsSuccess)
                {
                    model = systemFeature.Value.ToModel();

                    _systemFeatureService.Delete(model.SystemFeatureID);

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

        [HttpPost]
        public ActionResult Update(SystemFeatureModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceSystemFeatureCommand(model);

                    _systemFeatureService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso!"));

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