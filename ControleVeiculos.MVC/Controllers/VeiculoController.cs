using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Veiculos;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.Veiculos;
using ControleVeiculos.Domain.Entities.Veiculos;

namespace ControleVeiculos.MVC.Controllers
{
    public class VeiculoController : BaseController
    {
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly IVeiculoService _veiculoService;


        public VeiculoController(IUserService userService,
                                    IParameterValueService parameterValueService,
                                    IVeiculoService veiculoService,
                                    ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _parameterValueService = parameterValueService;
            _systemFeatureService = systemFeatureService;
            _veiculoService = veiculoService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new VeiculoModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(VeiculoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceVeiculoCommand(model);

                    _veiculoService.Add(command);

                    SuccessNotification(string.Format("Veículo adicionado com sucesso! Veículo: {0}.", model.Modelo));

                    return RedirectToAction("Index", "Veiculo");
                }

                ErrorNotification(string.Format("Não foi possível incluir um novo veículo devido ao preenchimento dos campos obrigatórios"));

                return RedirectToAction("Index", "Veiculo");
            }

            catch (Exception ex)
            {
                ErrorNotification(string.Format("Não foi possível incluir um novo veiculo: {0}, ", model.Modelo));

                return RedirectToAction("Index", "Veiculo");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, VeiculoModel model)
        {
            var veiculos = _veiculoService.GetAll(new FilterVeiculoCommand
            {
                Modelo = model.SearchModelo,
                Status = model.SearchStatus,
                Ano = model.SearchAno,
                Motor = model.SearchMotor,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = veiculos.Select(x =>
                {
                    var veiculoModel = x.ToModel();

                    return veiculoModel;
                }),
                Total = veiculos.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new VeiculoModel();
            
            return PartialView("Maintenance", model);
        }

        private MaintenanceVeiculoCommand MaintenanceVeiculoCommand(VeiculoModel model)
        {
            MaintenanceVeiculoCommand command = new MaintenanceVeiculoCommand();

            command.VeiculoID = model.VeiculoID;
            command.Modelo = model.Modelo;
            command.Cor = model.Cor;
            command.Placa = model.Placa;
            command.Status = model.Status;
            command.Ano = model.Ano;
            command.ManutencaoID = model.ManutencaoID;
            command.AbastecimentoID = model.AbastecimentoID;
            command.NumeroChassi = model.NumeroChassi;
            command.Motor = model.Motor;

            return command;
        }

        public ActionResult GetByID(int veiculoID, string ActionName)
        {
            var model = new VeiculoModel();

            Session["veiculoID"] = model.VeiculoID;

            Result<Veiculo> Veiculo = _veiculoService.GetByID(veiculoID);

            if (Veiculo.IsSuccess)
            {
                model = Veiculo.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    return PartialView("Maintenance", model);
                }
            }

            return RedirectToAction("Index", "Veiculo");
        }

        public ActionResult Delete(int veiculoID)
        {
            try
            {
                if (veiculoID == 0)
                {
                    ErrorNotification(string.Format("O veiculo selecionado não pode ser excluido!"));
                    return Redirect("Index");
                }
                var model = new VeiculoModel();

                Result<Veiculo> veiculo = _veiculoService.GetByID(veiculoID);

                if (veiculo.IsSuccess)
                {
                    model = veiculo.Value.ToModel();

                    _veiculoService.Delete(model.VeiculoID);

                    SuccessNotification(string.Format("Veiculo excluido com sucesso! Veículo: {0}", model.Modelo));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir o veiculo, tente novamente.");

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Update(VeiculoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceVeiculoCommand(model);

                    _veiculoService.Update(command);

                    SuccessNotification(string.Format("Veículo atualizado com sucesso! Veículo: {0}", model.Modelo));

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