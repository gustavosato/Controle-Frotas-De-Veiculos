using ControleVeiculos.Domain.Entities.Groups;
using ControleVeiculos.MVC.Models.Groups;

namespace ControleVeiculos.MVC.Extensions
{
    public static class GroupMappingExtensions
    {
        public static GroupModel ToModel(this Group entity)
        {
            if (entity == null)
                return null;

            var model = new GroupModel
            {
                GroupID = entity.groupID,
                GroupName = entity.groupName,
                IsSystem = entity.isSystem,
                DomainID = entity.domainID,
                Description = entity.description,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}