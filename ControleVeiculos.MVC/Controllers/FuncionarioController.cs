using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Funcionarios;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.Funcionarios;
using ControleVeiculos.Domain.Entities.Funcionarios;

namespace ControleVeiculos.MVC.Controllers
{
    public class FuncionarioController : BaseController
    {
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly IFuncionarioService _funcionarioService;


        public FuncionarioController(IUserService userService,
                                    IParameterValueService parameterValueService,
                                    IFuncionarioService funcionarioService,
                                    ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _parameterValueService = parameterValueService;
            _systemFeatureService = systemFeatureService;
            _funcionarioService = funcionarioService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new FuncionarioModel();
            var funcao = _parameterValueService.GetAllByParameterID("223202");
            //var feature = _systemFeatureService.GetAll();
            var setor = _parameterValueService.GetAllByParameterID("40");
            
            model.SearchLoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            //model.SearchLoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
            model.SearchLoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(FuncionarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceFuncionarioCommand(model);

                    _funcionarioService.Add(command);

                    SuccessNotification(string.Format("Funcionário adicionado com sucesso! Nome: {0}.", model.NomeFuncionario));

                    return RedirectToAction("Index", "Funcionario");
                }

                ErrorNotification(string.Format("Não foi possível incluir um novo funcionário devido ao preenchimento dos campos obrigatórios"));

                return RedirectToAction("Index", "Funcionario");
            }

            catch (Exception ex)
            {
                ErrorNotification(string.Format("Não foi possível incluir um novo funcionario: {0}, " + ex.Message.ToString(), model.NomeFuncionario));

                return RedirectToAction("Index", "Funcionario");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, FuncionarioModel model)
        {
            var funcionarios = _funcionarioService.GetAll(new FilterFuncionarioCommand
            {
                NomeFuncionario = model.SearchNomeFuncionario,
                CPF = model.SearchCPF,
                Funcao = model.SearchFuncao,
                Setor = model.SearchSetor,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = funcionarios.Select(x =>
                {
                    var funcionarioModel = x.ToModel();

                    return funcionarioModel;
                }),
                Total = funcionarios.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new FuncionarioModel();
            var funcao = _parameterValueService.GetAllByParameterID("223202");
            //var feature = _systemFeatureService.GetAll();
            var setor = _parameterValueService.GetAllByParameterID("40");

            model.LoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
           // model.LoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
            model.LoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return PartialView("Maintenance", model);
        }

        private MaintenanceFuncionarioCommand MaintenanceFuncionarioCommand(FuncionarioModel model)
        {
            MaintenanceFuncionarioCommand command = new MaintenanceFuncionarioCommand();

            command.FuncionarioID = model.FuncionarioID;
            command.NomeFuncionario = model.NomeFuncionario;
            command.Endereco = model.Endereco;
            command.CPF = model.CPF;
            command.Funcao = model.Funcao;
            command.Setor = model.Setor;
            command.Telefone = model.Telefone;
            command.NumeroCnh = model.NumeroCnh;

            return command;
        }

        public ActionResult GetByID(int funcionarioID, string ActionName)
        {
            var model = new FuncionarioModel();

            Result<Funcionario> funcionario = _funcionarioService.GetByID(funcionarioID);

            if (funcionario.IsSuccess)
            {
                model = funcionario.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var funcao = _parameterValueService.GetAllByParameterID("223202");
                    //var feature = _systemFeatureService.GetAll();
                    var setor = _parameterValueService.GetAllByParameterID("40");

                    model.LoadFuncao = funcao.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
                    //model.LoadFeature = feature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();
                    model.LoadSetor = setor.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    return PartialView("Maintenance", model);
                }
            }

            return RedirectToAction("Index", "Funcionario");
        }

        public ActionResult Delete(int funcionarioID)
        {
            try
            {
                if (funcionarioID == 0)
                {
                    ErrorNotification(string.Format("O funcionario selecionado não pode ser excluido! Nome: {0} ", funcionarioID));
                    return Redirect("Index");
                }
                var model = new FuncionarioModel();

                Result<Funcionario> funcionario = _funcionarioService.GetByID(funcionarioID);

                if (funcionario.IsSuccess)
                {
                    model = funcionario.Value.ToModel();

                    _funcionarioService.Delete(model.FuncionarioID);

                    SuccessNotification(string.Format("Funcionario excluido com sucesso! Nome: {0}", model.NomeFuncionario));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir o funcionario, tente novamente.");

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Update(FuncionarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceFuncionarioCommand(model);

                    _funcionarioService.Update(command);

                    SuccessNotification(string.Format("Funcionário atualizado com sucesso! Nome: {0}", model.NomeFuncionario));

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