using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;       
using ControleVeiculos.MVC.Models.ParameterValues;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.ParameterValues;
using ControleVeiculos.Domain.Entities.ParameterValues;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;


namespace ControleVeiculos.MVC.Controllers
{
    public class ParameterValueController : BaseController
    {
        private readonly IParameterValueService _parameterValueService;
        private readonly IParameterService _parameterService;

        public ParameterValueController(IParameterValueService parameterValueService, 
                                        IParameterService parameterService)
        {
            _parameterValueService = parameterValueService;
            _parameterService = parameterService;
        }

        private string SystemFeatureID = "105";

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ParameterValueModel();

            var parameters = _parameterService.GetAll();

            model.SearchLoadParameters = parameters.Select(x => new SelectListItem() { Text = x.parameterName.ToString(), Value = x.parameterID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ParameterValueModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceParameterValueCommand(model);

                    _parameterValueService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "ParameterValue");
                }

                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "ParameterValue");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "ParameterValue");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ParameterValueModel model)
        {
            var gridModel = new DataSourceResult();

           {
                var parameterValues = _parameterValueService.GetAll(new FilterParameterValueCommand
                {
                    ParameterID = model.SearchParameterID,
                    ParameterValue = model.SearchParameterValue,
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = parameterValues.Select(x =>
                    {
                        var parameterValuesModel = x.ToModel();

                        return parameterValuesModel;
                    }),

                    Total = parameterValues.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new ParameterValueModel();

            var parameters = _parameterService.GetAll();
            
            model.LoadParameters = parameters.Select(x => new SelectListItem() { Text = x.parameterName.ToString(), Value = x.parameterID.ToString() }).ToList();

            model.IsSystem = "True";

            model.CreatedByID = Convert.ToString(Session["userID"]);

            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
            return PartialView("Maintenance", model);
        }

        private MaintenanceParameterValueCommand MaintenanceParameterValueCommand(ParameterValueModel model)
        {
            MaintenanceParameterValueCommand command = new MaintenanceParameterValueCommand();
                            
            command.ParameterValueID = model.ParameterValueID;
            command.ParameterValue = model.ParameterValue;
            command.ParameterID = model.ParameterID;
            command.ParentID = model.ParentID;
            command.IsSystem = model.IsSystem;
            command.Description = model.Description;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 

            return command;
        }

        public ActionResult GetByID(int parameterValueID, string ActionName)
        {
            var model = new ParameterValueModel();

            Result<ParameterValue> parameterValue = _parameterValueService.GetByID(parameterValueID);

            if (parameterValue.IsSuccess)
            {
                model = parameterValue.Value.ToModel();
                
                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var parameters = _parameterService.GetAll();

                    model.LoadParameters = parameters.Select(x => new SelectListItem() { Text = x.parameterName.ToString(), Value = x.parameterID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "ParameterValue");
        }

        public ActionResult Delete(int parameterValueID)
        {
            try
            {
                if (parameterValueID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir registro! "));

                    return Redirect("Index");
                }
                var model = new ParameterValueModel();

                Result<ParameterValue> parameterValue = _parameterValueService.GetByID(parameterValueID);

                if (parameterValue.IsSuccess)
                {
                    model = parameterValue.Value.ToModel();


                    _parameterValueService.Delete(model.ParameterValueID);

                    SuccessNotification(string.Format("Registro excluído com sucesso! "));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                WarningNotification("Erro ao excluir registro!");

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(ParameterValueModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceParameterValueCommand(model);

                    _parameterValueService.Update(command);

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