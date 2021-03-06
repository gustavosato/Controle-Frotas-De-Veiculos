using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Parameters;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Parameters;
using ControleVeiculos.Domain.Entities.Parameters;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;


namespace ControleVeiculos.MVC.Controllers
{
    public class ParameterController : BaseController
    {
        private readonly IParameterService _parameterService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;

        public ParameterController(IParameterService parameterService,
                                    IUserService userService,
                                    ISystemFeatureService systemFeatureService,
                                    IParameterValueService parameterValueService)
        {
            _userService = userService;
            _parameterService = parameterService;
            _systemFeatureService = systemFeatureService;
            _parameterValueService = parameterValueService;
        }

        private string SystemFeatureID = "104";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ParameterModel();

            var systemFeatures = _systemFeatureService.GetAll();

            model.SearchLoadSystemFeatures = systemFeatures.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ParameterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceParameterCommand(model);

                    _parameterService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "Parameter");
                }

                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Erro ao realizar registro de parâmetro!"));

                return RedirectToAction("Index", "Parameter");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ParameterModel model)
        {
            var gridModel = new DataSourceResult();

            {
                var parameters = _parameterService.GetAll(new FilterParameterCommand
                {
                    ParameterName = model.SearchParameterName,
                    SystemFeatureID = model.SearchSystemFeatureID
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = parameters.Select(x =>
                    {
                        var parameterModel = x.ToModel();

                        return parameterModel;
                    }),

                    Total = parameters.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new ParameterModel();

            var systemFeatures = _systemFeatureService.GetAll();

            model.LoadSystemFeatures = systemFeatures.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        public ActionResult LoadActiveDesactiveCustomer()
        {
            return PartialView("ActiveDesactiveCustomer");
        }

        private MaintenanceParameterCommand MaintenanceParameterCommand(ParameterModel model)
        {
            MaintenanceParameterCommand command = new MaintenanceParameterCommand();

            command.ParameterID = model.ParameterID;
            command.ParameterName = model.ParameterName;
            command.SystemFeatureID = model.SystemFeatureID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int parameterID, string ActionName)
        {
            var model = new ParameterModel();

            Result<Parameter> Parameter = _parameterService.GetByID(parameterID);

            if (Parameter.IsSuccess)
            {
                model = Parameter.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var systemFeatures = _systemFeatureService.GetAll();

                    model.LoadSystemFeatures = systemFeatures.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "Parameter");
        }

        public ActionResult Delete(int parameterID)
        {
            try
            {
                if (parameterID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new ParameterModel();

                Result<Parameter> parameter = _parameterService.GetByID(parameterID);

                if (parameter.IsSuccess)
                {
                    model = parameter.Value.ToModel();

                    _parameterService.Delete(model.ParameterID);

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
        public ActionResult Update(ParameterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceParameterCommand(model);

                    _parameterService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso!"));

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