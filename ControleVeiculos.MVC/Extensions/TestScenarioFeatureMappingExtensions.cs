using Lean.Test.Cloud.Domain.Entities.TestScenarioFeatures;
using Lean.Test.Cloud.MVC.Models.TestScenarioFeatures;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class TestScenarioFeatureMappingExtensions
    {
        public static TestScenarioFeatureModel ToModel(this TestScenarioFeature entity)
        {
            if (entity == null)
                return null;

            var model = new TestScenarioFeatureModel
            {
                TestScenarioFeatureID = entity.testScenarioFeatureID,
                TestScenarioID = entity.testScenarioID,
                FeatureID = entity.featureID,
                ExecutionOrder = entity.executionOrder,
                IsLoop = entity.isLoop,
                StatusID = entity.statusID,
                ToolsTestID = entity.toolsTestID,
                TestID = entity.testID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,
                FeatureName = entity.featureName,
            };

            return model;
        }
    }
}