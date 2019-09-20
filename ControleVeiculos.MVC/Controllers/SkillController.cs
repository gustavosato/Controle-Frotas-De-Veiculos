using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Skills;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Skills;
using ControleVeiculos.Domain.Entities.Skills;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;

namespace ControleVeiculos.MVC.Controllers
{
    public class SkillController : BaseController
    {
        private readonly ISkillService _skillService;
        private readonly ICustomerService _customerService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IProfilesService _profilesService;
        private readonly IUserService _userService;

        public SkillController(ISkillService skillService,
                                    ICustomerService customerService,
                                    IProfilesService profilesService,
                                    IUserService userService,
                                    IParameterValueService parameterValueService)
        {
            _userService = userService;
            _skillService = skillService;
            _profilesService = profilesService;
            _customerService = customerService;
            _parameterValueService = parameterValueService;
        }

        private string SystemFeatureID = "318";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var model = new SkillModel();

            var skillTypeID = _parameterValueService.GetAllByParameterID("318300");
            model.SearchLoadSkillType = skillTypeID.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SkillModel model)
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
                    WarningNotification("Você não tem permissão para adicionar um registro em Habilidades e Competências!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceSkillCommand(model);

                    _skillService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso! "));

                    return RedirectToAction("Index", "Skill");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro! "));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro! "));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, SkillModel model)
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
                WarningNotification("Você não tem permissão para visualizar os registros de apropriação de horas!");

                return Json(gridModel);
            }
            else
            {
                var skills = _skillService.GetAll(new FilterSkillCommand
                {
                    Summary = model.SearchSummary,
                    SkillTypeID = model.SearchSkillTypeID,
                }, request.Page - 1, request.PageSize);

                 gridModel = new DataSourceResult
                {
                    Data = skills.Select(x =>
                    {
                        var skillsModel = x.ToModel();

                        return skillsModel;
                    }),
                    Total = skills.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new SkillModel();

            var skillTypeID = _parameterValueService.GetAllByParameterID("318300");

            var users = _userService.GetAll(0);

            model.LoadSkillType = skillTypeID.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();
            model.LoadModifiedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();
            model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }

        public ActionResult LoadActiveDesactiveCustomer()
        {
            return PartialView("ActiveDesactiveCustomer");
        }

        private MaintenanceSkillCommand MaintenanceSkillCommand(SkillModel model)
        {
            MaintenanceSkillCommand command = new MaintenanceSkillCommand();

            command.SkillID = model.SkillID;
            command.Summary = model.Summary;
            command.SkillTypeID = model.SkillTypeID;
            command.Description = model.Description;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int skillID, string ActionName)
        {
            var model = new SkillModel();

            Result<Skill> Skill = _skillService.GetByID(skillID);

            if (Skill.IsSuccess)
            {
                model = Skill.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {

                    var skillType = _parameterValueService.GetAllByParameterID("318300");
                    model.LoadSkillType = skillType.Select(x => new SelectListItem() { Text = x.parameterValue.ToString(), Value = x.parameterValueID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
                else
                {
                    return PartialView("StatusChange", model);
                }

            }

            return RedirectToAction("Index", "Skill");
        }

        public ActionResult Delete(int skillID)
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
                    WarningNotification("Você não tem permissão para excluir um registro em Habilidades e Competências!");

                    return RedirectToAction("Index");
                }

                if (skillID == 0)
                {
                    ErrorNotification(string.Format("O registro não pode ser excluído! "));
                    return Redirect("Index");
                }
                var model = new SkillModel();

                Result<Skill> skill = _skillService.GetByID(skillID);

                if (skill.IsSuccess)
                {
                    model = skill.Value.ToModel();

                    _skillService.Delete(model.SkillID);

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
        public ActionResult Update(SkillModel model)
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
                    WarningNotification("Você não tem permissão para atualizar uma registro em Habilidades e Competências!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceSkillCommand(model);

                    _skillService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso!", model.Description));

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