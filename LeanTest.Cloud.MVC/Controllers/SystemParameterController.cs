﻿using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.SystemParameter;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.SystemParameters;
using Lean.Test.Cloud.Domain.Entities.SystemParameters;
using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class SystemParameterController : BaseController
    {
        private readonly ISystemParameterService _systemParameterService;
        private readonly IProfilesService _profilesService;

        public SystemParameterController(ISystemParameterService systemParameterService,
                                         IProfilesService profilesService)


        {
            _profilesService = profilesService;
            _systemParameterService = systemParameterService;
        }

        private string SystemFeatureID = "107";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Add(SystemParameterModel model)
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
                    WarningNotification("Você não tem permissão para adicionar um registro em Configurações!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceSystemParameterCommand(model);

                    _systemParameterService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "SystemParameter");
                }

                ErrorNotification(string.Format("Não foi possível realizar registro de parâmetro! "));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Erro ao tentar cadastrar o parâmetro! "));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, SystemParameterModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registros de Configurações!");

                return Json(gridModel);
            }
            else
            {
                var systemParameters = _systemParameterService.GetAll(new FilterSystemParameterCommand
                {
                    ParamterName = model.SearchParamterName
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = systemParameters.Select(x =>
                    {
                        var systemParameterModel = x.ToModel();

                        return systemParameterModel;
                    }),
                    Total = systemParameters.TotalCount
                };

                return Json(gridModel);
            }
        }
        public ActionResult New()
        {
            var model = new SystemParameterModel();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); ;

            return PartialView("Maintenance", model);
        }

        public ActionResult LoadActiveDesactiveCustomer()
        {
            return PartialView("ActiveDesactiveCustomer");
        }

        private MaintenanceSystemParameterCommand MaintenanceSystemParameterCommand(SystemParameterModel model)
        {
            MaintenanceSystemParameterCommand command = new MaintenanceSystemParameterCommand();

            command.ParameterID = model.ParameterID;
            command.ParamterName = model.ParamterName;
            command.ParamterValue = model.ParamterValue;
            command.ParamterDefaultValue = model.ParamterDefaultValue;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int parameterID, string ActionName)
        {
            var model = new SystemParameterModel();

            Result<SystemParameter> SystemParameter = _systemParameterService.GetByID(parameterID);

            if (SystemParameter.IsSuccess)
            {
                model = SystemParameter.Value.ToModel();

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

            return RedirectToAction("Index", "SystemParameter");
        }

        public ActionResult Delete(int parameterID)
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
                    WarningNotification("Você não tem permissão para excluir um registro em Configurações!");

                    return RedirectToAction("Index");
                }

                if (parameterID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new SystemParameterModel();

                Result<SystemParameter> systemParameter = _systemParameterService.GetByID(parameterID);

                if (systemParameter.IsSuccess)
                {
                    model = systemParameter.Value.ToModel();

                    _systemParameterService.Delete(model.ParameterID);

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
        public ActionResult Update(SystemParameterModel model)
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
                    WarningNotification("Você não tem permissão para atualizar um registro em Configurações!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {
                    var command = MaintenanceSystemParameterCommand(model);

                    _systemParameterService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso! "));

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