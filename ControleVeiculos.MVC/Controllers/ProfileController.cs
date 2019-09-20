using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain.Entities.Profiles;
using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Models.Profiles;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Extensions;

namespace ControleVeiculos.MVC.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IProfilesService _profileService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly ISystemFeatureService _systemFeatureService;

        public ProfileController(IProfilesService profileService,
                                  ICustomerService customerService,
                                  IUserService userService,
                                  IParameterValueService parameterValueService,
                                  IGroupService groupService,
                                  ISystemFeatureService systemFeatureService)
        {
            _userService = userService;
            _profileService = profileService;
            _customerService = customerService;
            _parameterValueService = parameterValueService;
            _groupService = groupService;
            _systemFeatureService = systemFeatureService;
        }

        // GET: Profile
        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ProfileModel();

            var group = _groupService.GetAll(0);
            var systemFeature = _systemFeatureService.GetAll();

            model.SearchLoadGroup = group.Select(x => new SelectListItem() { Text = x.groupName.ToString(), Value = x.groupID.ToString() }).ToList();
            model.SearchLoadSystemFeature = systemFeature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ProfileModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var command = MaintenanceProfileCommand(model);

                    _profileService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso!"));

                    return RedirectToAction("Index");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(ProfileModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = MaintenanceProfileCommand(model);

                    _profileService.Update(command);

                    SuccessNotification(string.Format("Atualização registrada com sucesso."));

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


        private MaintenanceProfileCommand MaintenanceProfileCommand(ProfileModel model)
        {
            MaintenanceProfileCommand command = new MaintenanceProfileCommand();

            command.ProfileID = model.ProfileID;
            command.GroupID = model.GroupID;
            command.SystemFeatureID = model.SystemFeatureID;
            command.AllowView = model.AllowView;
            command.AllowAdd = model.AllowAdd;
            command.AllowUpdate = model.AllowUpdate;
            command.AllowDelete = model.AllowDelete;
            command.AllowChangeStatus = model.AllowChangeStatus;
            command.AllowAddRemove = model.AllowAddRemove;
            command.AllowExportExcel = model.AllowExportExcel;
            command.AllowReportView = model.AllowReportView;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int profileID, string ActionName)
        {
            var model = new ProfileModel();

            Result<Profile> Profile = _profileService.GetByID(profileID);

            if (Profile.IsSuccess)
            {
                model = Profile.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {
                    var group = _groupService.GetAll(0);
                    var systemFeature = _systemFeatureService.GetAll();

                    model.LoadGroup = group.Select(x => new SelectListItem() { Text = x.groupName.ToString(), Value = x.groupID.ToString() }).ToList();
                    model.LoadSystemFeature = systemFeature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();


                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }
            }
            return RedirectToAction("Index", "Contract");
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, ProfileModel model)
        {
            var profiles = _profileService.GetAll(new FilterProfileCommand
            {
                GroupID = model.SearchGroupID,
                SystemFeatureID = model.SearchSystemFeatureID,
                AllowView = model.AllowView,
                AllowAdd = model.AllowAdd,
                AllowUpdate = model.AllowUpdate,
                AllowDelete = model.AllowDelete,
                AllowChangeStatus = model.AllowChangeStatus,
                AllowAddRemove = model.AllowAddRemove,
                AllowExportExcel = model.AllowExportExcel

            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = profiles.Select(x =>
                {
                    var profileModel = x.ToModel();

                    return profileModel;
                }),
                Total = profiles.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult Delete(int profileID)
        {
            try
            {
                if (profileID == 0)
                {
                    ErrorNotification(string.Format("Não é possível excluir registro!"));
                    return Redirect("Index");
                }
                var model = new ProfileModel();

                Result<Profile> contract = _profileService.GetByID(profileID);

                if (contract.IsSuccess)
                {
                    model = contract.Value.ToModel();

                    _profileService.Delete(model.ProfileID);

                    SuccessNotification(string.Format("Registro excluído com sucesso!"));

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

        public ActionResult New()
        {
            var model = new ProfileModel();

            var group = _groupService.GetAll(0);
            var systemFeature = _systemFeatureService.GetAll();

            model.LoadGroup = group.Select(x => new SelectListItem() { Text = x.groupName.ToString(), Value = x.groupID.ToString() }).ToList();
            model.LoadSystemFeature = systemFeature.Select(x => new SelectListItem() { Text = x.systemFeatureName.ToString(), Value = x.systemFeatureID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }
    }
}