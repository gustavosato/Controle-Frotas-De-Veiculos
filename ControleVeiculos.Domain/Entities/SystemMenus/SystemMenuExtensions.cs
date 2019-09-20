using ControleVeiculos.Domain.Command.SystemMenus;
using System;

namespace ControleVeiculos.Domain.Entities.SystemMenus
{
    public static class SystemMenuExtensions
    {
        public static Result<SystemMenu> GetSystemMenu(this SystemMenu systemMenu)
        {
            return Result.Ok(0, "", systemMenu);
        }

        public static SystemMenu Map(this SystemMenu systemMenu, MaintenanceSystemMenuCommand command)
        {

            systemMenu.menuID = command.MenuID;
            systemMenu.textMenu = command.TextMenu;
            systemMenu.description = command.Description;
            systemMenu.ordem = command.Ordem;
            systemMenu.urlAction = command.UrlAction;
            systemMenu.controller = command.Controller;
            systemMenu.icon = command.Icon;
            systemMenu.itsAdmin = command.ItsAdmin;
            systemMenu.systemFeatureID = command.SystemFeatureID;
            systemMenu.createdByID = command.CreatedByID;
            systemMenu.creationDate = command.CreationDate;
            systemMenu.modifiedByID = command.ModifiedByID;
            systemMenu.lastModifiedDate = DateTime.Now.ToString();

            return systemMenu;
        }
    }
}
