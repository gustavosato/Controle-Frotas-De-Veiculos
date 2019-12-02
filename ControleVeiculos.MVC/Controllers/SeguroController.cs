using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Seguros;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.Seguros;
using ControleVeiculos.Domain.Entities.Seguros;

namespace ControleVeiculos.MVC.Controllers
{
    public class SeguroController : BaseController
    {
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly ISeguroService _seguroService;
        private readonly IVeiculoService _veiculoService;


        public SeguroController(IUserService userService,
                                    IParameterValueService parameterValueService,
                                    ISeguroService seguroService,
                                    IVeiculoService veiculoService,
                                    ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _parameterValueService = parameterValueService;
            _systemFeatureService = systemFeatureService;
            _seguroService = seguroService;
            _veiculoService = veiculoService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new SeguroModel();
            var veiculo = _veiculoService.GetAll(0);
            
            model.SearchLoadVeiculo = veiculo.Select(x => new SelectListItem() { Text = x.modelo.ToString(), Value = x.veiculoID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SeguroModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceSeguroCommand(model);

                    _seguroService.Add(command);

                    SuccessNotification(string.Format("Seguro cadastrado com sucesso!"));

                    return RedirectToAction("Index", "Seguro");
                }

                ErrorNotification(string.Format("Não foi possível incluir um novo seguro devido ao preenchimento dos campos obrigatórios"));

                return RedirectToAction("Index", "Seguro");
            }

            catch (Exception ex)
            {
                ErrorNotification(string.Format("Não foi possível incluir um novo seguro "));

                return RedirectToAction("Index", "Seguro");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, SeguroModel model)
        {
            var seguros = _seguroService.GetAll(new FilterSeguroCommand
            {
                Apolice = model.SearchApolice,
                Seguradora = model.SearchSeguradora,
                Franquia = model.SearchFranquia,
                TipoSeguro = model.SearchTipoSeguro,
                DataContratacao = model.SearchDataContratacao,
                Vigencia = model.SearchVigencia,
                FimContratacao = model.SearchFimContratacao,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = seguros.Select(x =>
                {
                    var seguroModel = x.ToModel();

                    return seguroModel;
                }),
                Total = seguros.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new SeguroModel();
            var veiculo = _veiculoService.GetAll(0);

            model.LoadVeiculo = veiculo.Select(x => new SelectListItem() { Text = x.modelo.ToString(), Value = x.veiculoID.ToString() }).ToList();

            return PartialView("Maintenance", model);
        }

        private MaintenanceSeguroCommand MaintenanceSeguroCommand(SeguroModel model)
        {
            MaintenanceSeguroCommand command = new MaintenanceSeguroCommand();

            command.SeguroID = model.SeguroID;
            command.VeiculoID = model.VeiculoID;
            command.Apolice = model.Apolice;
            command.Seguradora = model.Seguradora;
            command.Franquia = model.Franquia;
            command.TipoSeguro = model.TipoSeguro;
            command.DataContratacao = model.DataContratacao;
            command.Vigencia = model.Vigencia;
            command.FimContratacao = model.FimContratacao;
            command.Renovacao = model.Renovacao;
            command.TelefoneSeguradora = model.TelefoneSeguradora;
            command.PeriodoCarencia = model.PeriodoCarencia;
            command.Indenizacao = model.Indenizacao;

            return command;
        }

        public ActionResult GetByID(int seguroID, string ActionName)
        {
            var model = new SeguroModel();

            Result<Seguro> seguro = _seguroService.GetByID(seguroID);

            if (seguro.IsSuccess)
            {
                model = seguro.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var veiculo = _veiculoService.GetAll(0);

                    model.LoadVeiculo = veiculo.Select(x => new SelectListItem() { Text = x.modelo.ToString(), Value = x.veiculoID.ToString() }).ToList();

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "Seguro");
        }

        public ActionResult Delete(int seguroID)
        {
            try
            {
                if (seguroID == 0)
                {
                    ErrorNotification(string.Format("O seguro selecionado não pode ser excluido!"));
                    return Redirect("Index");
                }
                var model = new SeguroModel();

                Result<Seguro> seguro = _seguroService.GetByID(seguroID);

                if (seguro.IsSuccess)
                {
                    model = seguro.Value.ToModel();

                    _seguroService.Delete(model.SeguroID);

                    SuccessNotification(string.Format("Seguro excluido com sucesso!"));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir o seguro, tente novamente.");

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Update(SeguroModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceSeguroCommand(model);

                    _seguroService.Update(command);

                    SuccessNotification(string.Format("Seguro atualizado com sucesso!"));

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