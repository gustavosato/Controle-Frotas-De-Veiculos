using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Groups;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Groups;
using ControleVeiculos.Domain.Entities.Groups;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;
using ControleVeiculos.Domain.Command.GroupsUsers;
using ControleVeiculos.MVC.Models.Users;
using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain.Command.Users;

namespace ControleVeiculos.MVC.Controllers
{
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly IParameterValueService _parameterValueService;
        private readonly IGroupUserService _groupUserService;
        private readonly IProfilesService _profilesService;
        private readonly IUserService _userService;

        public GroupController(IGroupService groupService,
                                IParameterValueService parameterValueService,
                                IGroupUserService groupUserService,
                                IProfilesService profilesService,
                                IUserService userService)
        {
            _groupService = groupService;
            _parameterValueService = parameterValueService;
            _groupUserService = groupUserService;
            _profilesService = profilesService;
            _userService = userService;
        }
        private string SystemFeatureID = "101";

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var model = new GroupModel();

            var users = _userService.GetAll(0);


            return View(model);
        }

        [HttpPost]
        public ActionResult Add(GroupModel model)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowAdd = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para adicionar um registro em Grupos!");

                    return RedirectToAction("Index");
                }

                if (ModelState.IsValid)
                {

                    var command = MaintenanceGroupCommand(model);

                    _groupService.Add(command);

                    SuccessNotification(string.Format("Registro realizado com sucesso!", model.GroupName));

                    return RedirectToAction("Index", "Group");

                }

                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Home");
            }

            catch (Exception)
            {
                ErrorNotification(string.Format("Não foi possível realizar registro!"));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, GroupModel model)
        {
            var gridModel = new DataSourceResult();

            //permissions
            if (_profilesService.GetAllow(new FilterAbastecimentoCommand
            {
                AllowView = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para visualizar os registros de Grupos!");

                return Json(gridModel);
            }
            else
            {
                var groups = _groupService.GetAll(new FilterGroupCommand
                {
                    GroupName = model.SearchGroupName,
                }, request.Page - 1, request.PageSize);

                gridModel = new DataSourceResult
                {
                    Data = groups.Select(x =>
                    {
                        var groupsModel = x.ToModel();

                        return groupsModel;
                    }),
                    Total = groups.TotalCount
                };

                return Json(gridModel);
            }
        }

        public ActionResult New()
        {
            var model = new GroupModel();


            var users = _userService.GetAll(0);


            model.CreatedByID = Convert.ToString(Session["userID"]);
            model.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return PartialView("Maintenance", model);
        }


        private MaintenanceGroupCommand MaintenanceGroupCommand(GroupModel model)
        {
            MaintenanceGroupCommand command = new MaintenanceGroupCommand();

            command.GroupID = model.GroupID;
            command.GroupName = model.GroupName;
            command.Description = model.Description;
            command.IsSystem = model.IsSystem;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = model.CreationDate;
            command.ModifiedByID = Convert.ToString(Session["userID"]);
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }

        public ActionResult GetByID(int groupID, string ActionName)
        {
            var model = new GroupModel();

            Result<Group> group = _groupService.GetByID(groupID);

            if (group.IsSuccess)
            {
                model = group.Value.ToModel();

                if (ActionName == "Delete")
                {
                    return PartialView("Delete", model);
                }
                else if (ActionName == "Maintenance")
                {

                    var users = _userService.GetAll(0);


                    model.LoadCreatedBy = users.Select(x => new SelectListItem() { Text = x.userName.ToString(), Value = x.userID.ToString() }).ToList();

                    model.Description = Server.HtmlDecode(model.Description);

                    return PartialView("Maintenance", model);
                }
            }
            return RedirectToAction("Index", "Group");
        }

        public ActionResult Delete(int groupID)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowDelete = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para excluir um registro em Grupos!");

                    return RedirectToAction("Index");
                }

                if (groupID == 0)
                {
                    ErrorNotification(string.Format("Não foi possível excluir registro! "));
                    return Redirect("Index");
                }
                var model = new GroupModel();

                Result<Group> group = _groupService.GetByID(groupID);

                if (group.IsSuccess)
                {
                    model = group.Value.ToModel();


                    _groupService.Delete(model.GroupID);

                    SuccessNotification(string.Format("Registro excluido com sucesso!"));

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
        //TESTE

        public ActionResult UserAssociate(int groupID)
        {
            var model = new GroupModel();
            model.GroupID = groupID;
            Session["groupAssociateID"] = groupID;
            return PartialView("UserAssociate");
        }

        [HttpPost]
        public ActionResult GetAllAssociateUserByGroupID(DataSourceRequest request, UserModel model)
        {

            model.GroupID = Session["groupAssociateID"].ToString();

            var users = _userService.GetAllAssociateUserByGroupID(new FilterUserCommand
            {
                UserName = model.SearchUserName,
                GroupID = model.GroupID
            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = users.Select(x =>
                {
                    var usersModel = x.ToModel();

                    return usersModel;
                }),
                Total = users.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GetAllNoAssociateUserByGroupID(DataSourceRequest request, UserModel model)
        {
            model.GroupID = Session["groupAssociateID"].ToString();

            var users = _userService.GetAllNoAssociateUserByGroupID(new FilterUserCommand
            {
                UserName = model.SearchUserName,
                GroupID = model.GroupID
            },
                request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = users.Select(x =>
                {
                    var usersModel = x.ToModel();

                    return usersModel;
                }),
                Total = users.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult DisassociateUser(int userID)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterAbastecimentoCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para desassociar um usuário!");

                return RedirectToAction("Index");
            }
            _groupUserService.Delete(Convert.ToInt16(Session["groupAssociateID"]), userID);

            return View();
        }

        public ActionResult AssociateUser(int userID)
        {
            //permissions
            if (_profilesService.GetAllow(new FilterAbastecimentoCommand
            {
                AllowAddRemove = true,
                SystemFeatureID = SystemFeatureID,
                UserID = Session["userID"].ToString(),
            }) == "0")
            {
                WarningNotification("Você não tem permissão para associar um usuário!");

                return RedirectToAction("Index");
            }

            var command = new MaintenanceGroupUserCommand();

            command.GroupID = Convert.ToInt16(Session["groupAssociateID"]);
            command.UserID = userID;

            _groupUserService.Add(command);

            return View();
        }

        //TESTE

        [HttpPost]
        public ActionResult Update(GroupModel model)
        {
            try
            {
                //permissions
                if (_profilesService.GetAllow(new FilterAbastecimentoCommand
                {
                    AllowUpdate = true,
                    SystemFeatureID = SystemFeatureID,
                    UserID = Session["userID"].ToString(),
                }) == "0")
                {
                    WarningNotification("Você não tem permissão para atualizar um registro em grupos!");

                    return RedirectToAction("Index");
                }
                if (ModelState.IsValid)
                {
                    var command = MaintenanceGroupCommand(model);

                    _groupService.Update(command);

                    SuccessNotification(string.Format("Registro atualizado com sucesso! "));

                    return RedirectToAction("Index");
                }

                ErrorNotification("Não foi possível salvar atualização!");

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