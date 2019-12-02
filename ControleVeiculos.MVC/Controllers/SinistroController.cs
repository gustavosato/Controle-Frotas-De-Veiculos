using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Sinistros;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.Sinistros;
using ControleVeiculos.Domain.Entities.Sinistros;

namespace ControleVeiculos.MVC.Controllers
{
    public class SinistroController : BaseController
    {
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly ISystemFeatureService _systemFeatureService;
        private readonly ISinistroService _sinistroService;


        public SinistroController(IUserService userService,
                                    IParameterValueService parameterValueService,
                                    ISinistroService sinistroService,
                                    ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _parameterValueService = parameterValueService;
            _systemFeatureService = systemFeatureService;
            _sinistroService = sinistroService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new SinistroModel();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SinistroModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceSinistroCommand(model);

                    _sinistroService.Add(command);

                    SuccessNotification(string.Format("Sinistro adicionado com sucesso! "));

                    return RedirectToAction("Index", "Sinistro");
                }

                ErrorNotification(string.Format("Não foi possível cadastrar um novo sinistro devido ao preenchimento dos campos obrigatórios"));

                return RedirectToAction("Index", "Sinistro");
            }

            catch (Exception ex)
            {
                ErrorNotification(string.Format("Não foi possível cadastrar um novo sinistro"));

                return RedirectToAction("Index", "Sinistro");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, SinistroModel model)
        {
            var sinistros = _sinistroService.GetAll(new FilterSinistroCommand
            {
                Apolice = model.SearchApolice,
                TipoSinistro = model.SearchTipoSinistro,
                Franquia = model.SearchFranquia,

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = sinistros.Select(x =>
                {
                    var sinistroModel = x.ToModel();

                    return sinistroModel;
                }),
                Total = sinistros.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult New()
        {
            var model = new SinistroModel();
            
            return PartialView("Maintenance", model);
        }

        private MaintenanceSinistroCommand MaintenanceSinistroCommand(SinistroModel model)
        {
            MaintenanceSinistroCommand command = new MaintenanceSinistroCommand();

            command.SinistroID = model.SinistroID;
            command.Apolice = model.Apolice;
            command.Franquia = model.Franquia;
            command.TipoSinistro = model.TipoSinistro;

            return command;
        }

        public ActionResult GetByID(int sinistroID, string ActionName)
        {
            var model = new SinistroModel();

            Result<Sinistro> sinistro = _sinistroService.GetByID(sinistroID);

            if (sinistro.IsSuccess)
            {
                model = sinistro.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "Sinistro");
        }

        public ActionResult Delete(int sinistroID)
        {
            try
            {
                if (sinistroID == 0)
                {
                    ErrorNotification(string.Format("O sinistro selecionado não pode ser excluido!"));
                    return Redirect("Index");
                }
                var model = new SinistroModel();

                Result<Sinistro> sinistro = _sinistroService.GetByID(sinistroID);

                if (sinistro.IsSuccess)
                {
                    model = sinistro.Value.ToModel();

                    _sinistroService.Delete(model.SinistroID);

                    SuccessNotification(string.Format("Sinistro excluido com sucesso!"));

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                WarningNotification("Erro ao tentar excluir o sinistro, tente novamente.");

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Update(SinistroModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var command = MaintenanceSinistroCommand(model);

                    _sinistroService.Update(command);

                    SuccessNotification(string.Format("Sinistro atualizado com sucesso!"));

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