using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;       
using Lean.Test.Cloud.MVC.Models.Elements;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Elements;
using Lean.Test.Cloud.Domain.Entities.Elements;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;


namespace Lean.Test.Cloud.MVC.Controllers
{
    public class ElementController : BaseController
    {
        private readonly IElementService _elementService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IApplicationSystemService _applicationSystemService;
        private readonly IFeatureService _featureService;

        public ElementController(IElementService elementService, 
                                IParameterValueService parameterValueService,
                                IUserService userService,
                                IApplicationSystemService applicationSystemService,
                                IFeatureService featureService)
        {
            _elementService = elementService;
            _parameterValueService = parameterValueService;
            _userService = userService;
            _applicationSystemService = applicationSystemService;
            _featureService = featureService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new ElementModel();

            var applications = _applicationSystemService.GetAll(Convert.ToInt32(Session["customerID"]));

            var actions = _parameterValueService.GetAllByParameterID("208200");

            var typeIdentifications = _parameterValueService.GetAllByParameterID("208202");

            model.LoadActions = actions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.LoadTypeIdentification= typeIdentifications.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.SearchLoadApplications = applications.Select(x => new SelectListItem() { Text = x.applicationSystemName.ToString(), Value = x.applicationSystemID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ElementModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceElementCommand(model);

                    _elementService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso!"));

                    return RedirectToAction("Index", "Element");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Element");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Element");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ElementModel model)
        {
            var elements = _elementService.GetAll(new FilterElementCommand
            {
               SystemApplicationID = model.SearchApplicatioinID,
               FeatureID = model.SearchFeatureID
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = elements.Select(x =>
                {
                    var elementsModel = x.ToModel();

                    return elementsModel;
                }),
                Total = elements.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new ElementModel();


            var functions = _parameterValueService.GetAllByParameterID("100100");
            var users = _userService.GetAll(0);

            model.LoadFunction = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
            return PartialView("Maintenance", model);
        }


        private MaintenanceElementCommand MaintenanceElementCommand(ElementModel model)
        {
            MaintenanceElementCommand command = new MaintenanceElementCommand();
                            
            command.ElementID = model.ElementID;
            command.Element = model.Element;
            command.ActionID = model.ActionID;
            command.DefaultValue = model.DefaultValue;
            command.Domains = model.Domains;
            command.AutomationID = model.AutomationID;
            command.TypeIdentificationID = model.TypeIdentificationID;
            command.FeatureID = model.FeatureID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 

            return command;
        }

        public ActionResult GetByID(int elementID, string ActionName)
        {
            var model = new ElementModel();

            Result<Element> element = _elementService.GetByID(elementID);

            if (element.IsSuccess)
            {
                model = element.Value.ToModel();
                
                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {

                    var functions = _parameterValueService.GetAllByParameterID("100100");
                    var users = _userService.GetAll(0);

                    model.LoadFunction = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();


                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "Element");
        }

        public ActionResult Delete(int elementID)
        {
            try
            {
                if (elementID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir registro!", elementID));
                    return Redirect("Index");
                }
                var model = new ElementModel();

                Result<Element> element = _elementService.GetByID(elementID);

                if (element.IsSuccess)
                {
                    model = element.Value.ToModel();


                    _elementService.Delete(model.ElementID);

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
        public ActionResult Update(ElementModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceElementCommand(model);

                    _elementService.Update(command);

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