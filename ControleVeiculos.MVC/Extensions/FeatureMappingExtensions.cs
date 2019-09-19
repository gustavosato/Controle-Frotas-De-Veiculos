using Lean.Test.Cloud.Domain.Entities.Features;
using Lean.Test.Cloud.MVC.Models.Features;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class FeatureMappingExtensions
    {
        public static FeatureModel ToModel(this Feature entity)
        {
            if (entity == null)
                return null;

            var model = new FeatureModel
            {
                FeatureID = entity.featureID,
                FeatureName = entity.featureName,
                Description = entity.description,
                CustomerID = entity.customerID,
                StatusID = entity.statusID,
                ApplicationSystemID = entity.applicationSystemID,
                DeveloperID = entity.developerID,
                FeatureTypeID = entity.featureTypeID,
                MetaScript = entity.metaScript,
                TestPoints = entity.testPoints,
                AutomationScript = entity.automationScript,
                TimeEffort = entity.timeEffort,
                TargetDate = entity.targetDate,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate


            };

            return model;
        }
    }
}