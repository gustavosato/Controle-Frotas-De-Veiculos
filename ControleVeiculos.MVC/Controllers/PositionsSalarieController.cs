using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.PositionsSalaries;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.PositionsSalaries;
using ControleVeiculos.Domain.Entities.PositionsSalaries;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;


namespace ControleVeiculos.MVC.Controllers
{
    public class PositionsSalarieController : BaseController
    {
        private readonly IPositionsSalarieService _positionsSalarieService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IProfilesService _profilesService;
        private readonly ICustomerService _customerService;

        public PositionsSalarieController(IPositionsSalarieService positionsSalarieService,
                                IParameterValueService parameterValueService,
                                IProfilesService profilesService,
                                IUserService userService,
                                ICustomerService customerService)
        {
            _positionsSalarieService = positionsSalarieService;
            _parameterValueService = parameterValueService;
            _userService = userService;
            _profilesService = profilesService;
            _customerService = customerService;
        }

        private string SystemFeatureID = "323";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new PositionsSalarieModel();

            var functions = _parameterValueService.GetAllByParameterID("100100");
            var users = _userService.GetAll(0);

            model.SearchLoadFunction = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();


            return View(model);
        }

        [HttpPost]
        public ActionResult Add(PositionsSalarieModel model)
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
                    WarningNotification("Você não tem permissão para adicionar um registro em Cargos e Salários!");

                    return RedirectToAction("Index");
                }
                if (ModelState.IsValid)
                {

                    var command = MaintenancePositionsSalarieCommand(model);

                    _positionsSalarieService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "PositionsSalarie");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, PositionsSalarieModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registros em Cargos e Salários!");

                return Json(gridModel);
            }
            else
            {
                var positionsSalaries = _positionsSalarieService.GetAll(new FilterPositionsSalarieCommand
                {
                    FunctionID = model.SearchFunctionID
                }, request.Page - 1, request.PageSize);


                 gridModel = new DataSourceResult
                {
                    Data = positionsSalaries.Select(x =>
                    {
                        var positionsSalariesModel = x.ToModel();

                        return positionsSalariesModel;
                    }),
                    Total = positionsSalaries.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new PositionsSalarieModel();


            var functions = _parameterValueService.GetAllByParameterID("100100");
            var classification = _parameterValueService.GetAllByParameterID("100101");
            var level = _parameterValueService.GetAllByParameterID("100102");
            

            model.LoadFunction = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadClassification = classification.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadLevel = level.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


            return PartialView("Maintenance", model);
        }


        private MaintenancePositionsSalarieCommand MaintenancePositionsSalarieCommand(PositionsSalarieModel model)
        {
            MaintenancePositionsSalarieCommand command = new MaintenancePositionsSalarieCommand();

            command.positionsSalarieID = model.PositionsSalarieID;
            command.functionID = model.FunctionID;
            command.levelID = model.LevelID;
            command.classificationID = model.ClassificationID;
            command.amountPJ = model.AmountPJ;
            if (model.AmountPJ != null) command.amountPJ = model.AmountPJ.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.amountCLT = model.AmountCLT;
            if (model.AmountCLT != null) command.amountCLT = model.AmountCLT.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.amountCLTFLEX = model.AmountCLTFLEX;
            if (model.AmountCLTFLEX != null) command.amountCLTFLEX = model.AmountCLTFLEX.Replace("R$", "").Replace(".", "").Replace(" ", "");
            command.createdByID = model.CreatedByID;
            command.creationDate = model.CreationDate;
            command.modifiedByID = Convert.ToString(Session["userID"]);
            command.lastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            command.startingDate = model.StartingDate;
            command.closingDate = model.ClosingDate;


            return command;
        }

        public ActionResult GetByID(int positionsSalarieID, string ActionName)
        {
            var model = new PositionsSalarieModel();

            Result<PositionsSalarie> positionsSalarie = _positionsSalarieService.GetByID(positionsSalarieID);

            if (positionsSalarie.IsSuccess)
            {
                model = positionsSalarie.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {

                    var functions = _parameterValueService.GetAllByParameterID("100100");
                    var classification = _parameterValueService.GetAllByParameterID("100101");
                    var level = _parameterValueService.GetAllByParameterID("100102");
                    var clt = model.AmountCLT;
                    var cltflex = model.AmountCLTFLEX;
                    var pj = model.AmountPJ;

                    model.LoadFunction = functions.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadClassification = classification.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.LoadLevel = level.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    model.AmountCLT = "R$" + clt;
                    model.AmountCLTFLEX = "R$" + cltflex;
                    model.AmountPJ = "R$" + pj;

                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "PositionsSalarie");
        }

        public ActionResult Delete(int positionsSalarieID)
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
                    WarningNotification("Você não tem permissão para excluir um registro em Cargos e Salários!");

                    return RedirectToAction("Index");
                }

                if (positionsSalarieID == 0)
                {
                    ErrorNotification(string.Format("Não é possível excluir registro! "));
                    return Redirect("Index");
                }
                var model = new PositionsSalarieModel();

                Result<PositionsSalarie> positionsSalarie = _positionsSalarieService.GetByID(positionsSalarieID);

                if (positionsSalarie.IsSuccess)
                {
                    model = positionsSalarie.Value.ToModel();


                    _positionsSalarieService.Delete(model.PositionsSalarieID);

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
        public ActionResult Update(PositionsSalarieModel model)
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
                    WarningNotification("Você não tem permissão para atualizar um registro em Cargos e Salários!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenancePositionsSalarieCommand(model);

                    _positionsSalarieService.Update(command);

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