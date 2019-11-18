using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Reservas;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.Reservas;
using ControleVeiculos.Domain.Entities.Reservas;

namespace ControleVeiculos.MVC.Controllers
{
    public class ReservaController : BaseController
    {
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly IReservaService _reservaService;
        private readonly IVeiculoService _veiculoService;
        private readonly IFuncionarioService _funcionarioService;


        public ReservaController(IUserService userService,
                                 IParameterValueService parameterValueService,
                                 IReservaService reservaService,
                                 IVeiculoService veiculoService,
                                 IFuncionarioService funcionarioService,
                                 ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _parameterValueService = parameterValueService;
            _systemFeatureService = systemFeatureService;
            _reservaService = reservaService;
            _veiculoService = veiculoService;
            _funcionarioService = funcionarioService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new ReservaModel();
            var veiculo = _veiculoService.GetByID(0);
            //var feature = _systemFeatureService.GetAll();
            
            
            model.SearchLoadVeiculo = veiculo.Select(x => new SelectListItem() { Text = x.modelo.ToString(), Value = x.veiculoID.ToString() }).ToList();
            //model.SearchLoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ReservaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceReservaCommand(model);

                    _reservaService.Add(command);

                    SuccessNotification(string.Format("Reserva realizada com sucesso!"));

                    return RedirectToAction("Index", "Reserva");
                }

                ErrorNotification(string.Format("Não foi possível realizar a reserva devido ao preenchimento dos campos obrigatórios"));

                return RedirectToAction("Index", "Reserva");
            }

            catch (Exception ex)
            {
                ErrorNotification(string.Format("Não foi possível realizar a reserva: {0}, " + ex.Message.ToString(), model.NomeReserva));

                return RedirectToAction("Index", "Reserva");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ReservaModel model)
        {
            var reservas = _reservaService.GetAll(new FilterReservaCommand
            {
                DataReserva = model.SearchDataReserva,
                Destino = model.SearchDestino,
                Funcionario = model.SearchFuncionario,
                Veiculo = model.SearchVeiculo,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = reservas.Select(x =>
                {
                    var reservaModel = x.ToModel();

                    return reservaModel;
                }),
                Total = reservas.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new ReservaModel();
            var veiculo = _veiculoService.GetByID(0);
            //var feature = _systemFeatureService.GetAll();

            model.LoadVeiculo = veiculo.Select(x => new SelectListItem() { Text = x.modelo.ToString(), Value = x.veiculoID.ToString() }).ToList();
            //model.LoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return PartialView("Maintenance", model);
        }

        private MaintenanceReservaCommand MaintenanceReservaCommand(ReservaModel model)
        {
            MaintenanceReservaCommand command = new MaintenanceReservaCommand();

            command.ReservaID = model.ReservaID;
            command.DataReserva = model.DataReserva;
            command.Finalidade = model.Finalidade;
            command.FuncionarioID = model.FuncionarioID;
            command.Destino = model.Destino;
            command.NumeroCnh = model.NumeroCnh;
            command.VeiculoID = model.VeiculoID;

            return command;
        }

        public ActionResult GetByID(int reservaID, string ActionName)
        {
            var model = new ReservaModel();

            Result<Reserva> reserva = _reservaService.GetByID(reservaID);

            if (reserva.IsSuccess)
            {
                model = reserva.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var veiculo = _veiculoService.GetByID(0);

                    model.LoadVeiculo = veiculo.Select(x => new SelectListItem() { Text = x.modelo.ToString(), Value = x.veiculoID.ToString() }).ToList();
                    //model.LoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "Reserva");
        }

        public ActionResult Delete(int reservaID)
        {
            try
            {
                if (reservaID == 0)
                {
                    ErrorNotification(string.Format("A reserva selecionada não pode ser excluida!"));
                    return Redirect("Index");
                }
                var model = new ReservaModel();

                Result<Reserva> reserva = _reservaService.GetByID(reservaID);

                if (reserva.IsSuccess)
                {
                    model = reserva.Value.ToModel();

                    _reservaService.Delete(model.ReservaID);

                    SuccessNotification(string.Format("Reserva excluída com sucesso!"));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir a reserva, tente novamente.");

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Update(ReservaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceReservaCommand(model);

                    _reservaService.Update(command);

                    SuccessNotification(string.Format("Reserva atualizada com sucesso!"));

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