using Lean.Test.Cloud.Domain.Entities.SystemFeatures;
using Lean.Test.Cloud.MVC.Models.SystemFeatures;

namespace Lean.Test.Cloud.MVC.Extensions
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