using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Rotas;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.Rotas;
using ControleVeiculos.Domain.Entities.Rotas;

namespace ControleVeiculos.MVC.Controllers
{
    public class RotaController : BaseController
    {
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly IRotaService _rotaService;


        public RotaController(IUserService userService,
                                IParameterValueService parameterValueService,
                                IRotaService rotaService,
                                ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _parameterValueService = parameterValueService;
            _systemFeatureService = systemFeatureService;
            _rotaService = rotaService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new RotaModel();
            //var funcao = _parameterValueService.GetAllByParameterID("223202");
            ////var feature = _systemFeatureService.GetAll();
            //var setor = _parameterValueService.GetAllByParameterID("40");

            //model.SearchLoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            ////model.SearchLoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
            //model.SearchLoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(RotaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceRotaCommand(model);

                    _rotaService.Add(command);

                    SuccessNotification(string.Format("Rota adicionada com sucesso! Rota: {0}.", model.Cidade));

                    return RedirectToAction("Index", "Rota");
                }

                ErrorNotification(string.Format("Não foi possível incluir uma nova rota devido ao preenchimento dos campos obrigatórios"));

                return RedirectToAction("Index", "Rota");
            }

            catch (Exception ex)
            {
                ErrorNotification(string.Format("Não foi possível incluir uma nova rota: {0}, " + ex.Message.ToString(), model.Cidade));

                return RedirectToAction("Index", "Rota");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, RotaModel model)
        {
            var rotas = _rotaService.GetAll(new FilterRotaCommand
            {
                Cidade = model.SearchCidade,
                Estado = model.SearchEstado,
                DataIda = model.SearchDataIda,
                DataVolta = model.SearchDataVolta,
                Pedagio = model.SearchPedagio

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = rotas.Select(x =>
                {
                    var rotaModel = x.ToModel();

                    return rotaModel;
                }),
                Total = rotas.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new RotaModel();
            // var funcao = _parameterValueService.GetAllByParameterID("223202");
            // //var feature = _systemFeatureService.GetAll();
            // var setor = _parameterValueService.GetAllByParameterID("40");

            // model.LoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            //// model.LoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
            // model.LoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return PartialView("Maintenance", model);
        }

        private MaintenanceRotaCommand MaintenanceRotaCommand(RotaModel model)
        {
            MaintenanceRotaCommand command = new MaintenanceRotaCommand();

            command.RotaID = model.RotaID;
            command.Cidade = model.Cidade;
            command.Estado = model.Estado;
            command.Distancia = model.Distancia;
            command.DataIda = model.DataIda;
            command.DataVolta = model.DataVolta;
            command.Pedagio = model.Pedagio;

            return command;
        }

        public ActionResult GetByID(int rotaID, string ActionName)
        {
            var model = new RotaModel();

            Result<Rota> rota = _rotaService.GetByID(rotaID);

            if (rota.IsSuccess)
            {
                model = rota.Value.ToModel();

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

            return RedirectToAction("Index", "Rota");
        }

        public ActionResult Delete(int rotaID)
        {
            try
            {
                if (rotaID == 0)
                {
                    ErrorNotification(string.Format("A rota selecionada não pode ser excluida! Rota: {0} ", rotaID));
                    return Redirect("Index");
                }
                var model = new RotaModel();

                Result<Rota> rota = _rotaService.GetByID(rotaID);

                if (rota.IsSuccess)
                {
                    model = rota.Value.ToModel();

                    _rotaService.Delete(model.RotaID);

                    SuccessNotification(string.Format("Rota excluida com sucesso! Nome: {0}", model.Cidade));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir a rota, tente novamente.");

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Update(RotaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceRotaCommand(model);

                    _rotaService.Update(command);

                    SuccessNotification(string.Format("Rota atualizada com sucesso! Rota: {0}", model.Cidade));

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