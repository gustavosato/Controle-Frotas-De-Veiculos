using ControleVeiculos.Domain.Entities.SystemFeatures;
using ControleVeiculos.MVC.Models.SystemFeatures;

namespace ControleVeiculos.MVC.Extensions
{
    public static class SystemFeatureMappingExtensions
    {
        public static SystemFeatureModel ToModel(this SystemFeature entity)
        {
            if (entity == null)
                return null;

            var model = new SystemFeatureModel
            {
                SystemFeatureID = entity.systemFeatureID,
                SystemFeatureName = entity.systemFeatureName,
                SystemFeatureTypeID = entity.systemFeatureTypeID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}