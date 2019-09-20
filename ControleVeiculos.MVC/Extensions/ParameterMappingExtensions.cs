using ControleVeiculos.Domain.Entities.Parameters;
using ControleVeiculos.MVC.Models.Parameters;

namespace ControleVeiculos.MVC.Extensions
{
    public static class ParameterMappingExtensions
    {
        public static ParameterModel ToModel(this Parameter entity)
        {
            if (entity == null)
                return null;

            var model = new ParameterModel
            {
                ParameterID = entity.parameterID,
                ParameterName = entity.parameterName,
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