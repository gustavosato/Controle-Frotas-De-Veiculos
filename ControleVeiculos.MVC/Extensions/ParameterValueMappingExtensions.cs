using ControleVeiculos.Domain.Entities.ParameterValues;
using ControleVeiculos.MVC.Models.ParameterValues;

namespace ControleVeiculos.MVC.Extensions
{
    public static class ParameterValueMappingExtensions
    {
        public static ParameterValueModel ToModel(this ParameterValue entity)
        {
            if (entity == null)
                return null;

            var model = new ParameterValueModel
            {
                ParameterValueID = entity.parameterValueID,
                ParameterValue = entity.parameterValue,
                ParameterID = entity.parameterID,
                ParentID = entity.parentID,
                IsSystem = entity.isSystem,
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