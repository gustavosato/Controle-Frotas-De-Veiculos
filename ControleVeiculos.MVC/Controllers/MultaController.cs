using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Multas;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.Multas;
using ControleVeiculos.Domain.Entities.Multas;

namespace ControleVeiculos.MVC.Controllers
{
    public class MultaController : BaseController
    {
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly IMultaService _multaService;


        public MultaController(IUserService userService,
                                    IParameterValueService parameterValueService,
                                    IMultaService multaService,
                                    ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _parameterValueService = parameterValueService;
            _systemFeatureService = systemFeatureService;
            _multaService = multaService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new MultaModel();
            //var funcao = _parameterValueService.GetAllByParameterID("223202");
            ////var feature = _systemFeatureService.GetAll();
            //var setor = _parameterValueService.GetAllByParameterID("40");
            
            //model.SearchLoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            ////model.SearchLoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
            //model.SearchLoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(MultaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceMultaCommand(model);

                    _multaService.Add(command);

                    SuccessNotification(string.Format("Multa adicionado com sucesso! Multa: {0}.", model.MultaID));

                    return RedirectToAction("Index", "Multa");
                }

                ErrorNotification(string.Format("Não foi possível incluir uma nova multa devido ao preenchimento dos campos obrigatórios"));

                return RedirectToAction("Index", "Multa");
            }

            catch (Exception ex)
            {
                ErrorNotification(string.Format("Não foi possível incluir uma nova multa: {0}, " + ex.Message.ToString(), model.MultaID));

                return RedirectToAction("Index", "Multa");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, MultaModel model)
        {
            var multas = _multaService.GetAll(new FilterMultaCommand
            {
                VeiculoID = model.SearchVeiculo,
                FuncionarioID = model.SearchFuncionario,
                
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = multas.Select(x =>
                {
                    var multaModel = x.ToModel();

                    return multaModel;
                }),
                Total = multas.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new MultaModel();
           // var funcao = _parameterValueService.GetAllByParameterID("223202");
           // //var feature = _systemFeatureService.GetAll();
           // var setor = _parameterValueService.GetAllByParameterID("40");

           // model.LoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
           //// model.LoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
           // model.LoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return PartialView("Maintenance", model);
        }

        private MaintenanceMultaCommand MaintenanceMultaCommand(MultaModel model)
        {
            MaintenanceMultaCommand command = new MaintenanceMultaCommand();

            command.MultaID = model.MultaID;
            command.VeiculoID = model.VeiculoID;
            command.FuncionarioID = model.FuncionarioID;
            
            return command;
        }

        public ActionResult GetByID(int multaID, string ActionName)
        {
            var model = new MultaModel();

            Result<Multa> multa = _multaService.GetByID(multaID);

            if (multa.IsSuccess)
            {
                model = multa.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    //var funcao = _parameterValueService.GetAllByParameterID("223202");
                    ////var feature = _systemFeatureService.GetAll();
                    //var setor = _parameterValueService.GetAllByParameterID("40");

                    //model.LoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    ////model.LoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
                    //model.LoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "Multa");
        }

        public ActionResult Delete(int multaID)
        {
            try
            {
                if (multaID == 0)
                {
                    ErrorNotification(string.Format("A multa selecionada não pode ser excluida! Multa: {0} ", multaID));
                    return Redirect("Index");
                }
                var model = new MultaModel();

                Result<Multa> multa = _multaService.GetByID(multaID);

                if (multa.IsSuccess)
                {
                    model = multa.Value.ToModel();

                    _multaService.Delete(model.MultaID);

                    SuccessNotification(string.Format("Multa excluida com sucesso! Multa: {0}", model.MultaID));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir a multa, tente novamente.");

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Update(MultaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceMultaCommand(model);

                    _multaService.Update(command);

                    SuccessNotification(string.Format("Multa atualizada com sucesso! Multa: {0}", model.MultaID));

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