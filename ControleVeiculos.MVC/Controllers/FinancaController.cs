using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Financas;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.Financas;
using ControleVeiculos.Domain.Entities.Financas;

namespace ControleVeiculos.MVC.Controllers
{
    public class FinancaController : BaseController
    {
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly IFinancaService _financaService;


        public FinancaController(IUserService userService,
                                    IParameterValueService parameterValueService,
                                    IFinancaService financaService,
                                    ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _parameterValueService = parameterValueService;
            _systemFeatureService = systemFeatureService;
            _financaService = financaService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new FinancaModel();
            //var funcao = _parameterValueService.GetAllByParameterID("223202");
            ////var feature = _systemFeatureService.GetAll();
            //var setor = _parameterValueService.GetAllByParameterID("40");
            
            //model.SearchLoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            ////model.SearchLoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
            //model.SearchLoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(FinancaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceFinancaCommand(model);

                    _financaService.Add(command);

                    SuccessNotification(string.Format("Conta adicionado com sucesso!"));

                    return RedirectToAction("Index", "Financa");
                }

                ErrorNotification(string.Format("Não foi possível incluir uma nova conta devido ao preenchimento dos campos obrigatórios"));

                return RedirectToAction("Index", "Financa");
            }

            catch (Exception ex)
            {
                ErrorNotification(string.Format("Não foi possível incluir uma nova conta: {0}, " + ex.Message.ToString(), model.ValorCarro));

                return RedirectToAction("Index", "Financa");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, FinancaModel model)
        {
            var financas = _financaService.GetAll(new FilterFinancaCommand
            {
                ValorCarro = model.SearchValorCarro,
                ValorSeguro = model.SearchValorSeguro,
                ValorAgua = model.SearchValorAgua,
                ValorLuz = model.SearchValorLuz,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = financas.Select(x =>
                {
                    var financaModel = x.ToModel();

                    return financaModel;
                }),
                Total = financas.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new FinancaModel();
           // var funcao = _parameterValueService.GetAllByParameterID("223202");
           // //var feature = _systemFeatureService.GetAll();
           // var setor = _parameterValueService.GetAllByParameterID("40");

           // model.LoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
           //// model.LoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
           // model.LoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return PartialView("Maintenance", model);
        }

        private MaintenanceFinancaCommand MaintenanceFinancaCommand(FinancaModel model)
        {
            MaintenanceFinancaCommand command = new MaintenanceFinancaCommand();

            command.FinancaID = model.FinancaID;
            command.ValorCarro = model.ValorCarro;
            command.ValorSeguro = model.ValorSeguro;
            command.ValorAgua = model.ValorAgua;
            command.ValorLuz = model.ValorLuz;
            command.ValorInternet = model.ValorInternet;
            command.ValorManutencao = model.ValorManutencao;
            command.Salarios = model.Salarios;
            command.GastosExtras = model.GastosExtras;

            return command;
        }

        public ActionResult GetByID(int financaID, string ActionName)
        {
            var model = new FinancaModel();

            Result<Financa> financa = _financaService.GetByID(financaID);

            if (financa.IsSuccess)
            {
                model = financa.Value.ToModel();

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

            return RedirectToAction("Index", "Financa");
        }

        public ActionResult Delete(int financaID)
        {
            try
            {
                if (financaID == 0)
                {
                    ErrorNotification(string.Format("A conta selecionado não pode ser excluida!"));
                    return Redirect("Index");
                }
                var model = new FinancaModel();

                Result<Financa> financa = _financaService.GetByID(financaID);

                if (financa.IsSuccess)
                {
                    model = financa.Value.ToModel();

                    _financaService.Delete(model.FinancaID);

                    SuccessNotification(string.Format("Conta excluida com sucesso!"));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir a conta, tente novamente.");

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Update(FinancaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceFinancaCommand(model);

                    _financaService.Update(command);

                    SuccessNotification(string.Format("Conta atualizada com sucesso!"));

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