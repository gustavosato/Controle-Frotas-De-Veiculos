using ControleVeiculos.Domain.Entities.SystemMenus;
using ControleVeiculos.MVC.Models.SystemMenus;

namespace ControleVeiculos.MVC.Extensions
{
    public static class SystemMenuMappingExtensions
    {
        public static SystemMenuModel ToModel(this SystemMenu entity)
        {
            if (entity == null)
                return null;

            var model = new SystemMenuModel
            {
                MenuID = entity.menuID,
                TextMenu = entity.textMenu,
                Description = entity.description,
                Ordem = entity.ordem,
                UrlAction = entity.urlAction,
                Controller = entity.controller,
                Icon = entity.icon,
                ItsAdmin = entity.itsAdmin,
                SystemFeatureID = entity.systemFeatureID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}