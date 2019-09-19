using Lean.Test.Cloud.Domain.Command.TestScenarioFeatures;
using System;

namespace Lean.Test.Cloud.Domain.Entities.TestScenarioFeatures
{
    public static class TestScenarioFeatureExtensions
    {
        public static Result<TestScenarioFeature> GetTestScenarioFeature(this TestScenarioFeature testScenarioFeature)
        {
            return Result.Ok(0, "", testScenarioFeature);
        }

        public static TestScenarioFeature Map(this TestScenarioFeature testScenarioFeature, MaintenanceTestScenarioFeatureCommand command)
        {
            testScenarioFeature.testScenarioFeatureID = command.TestScenarioFeatureID;
            testScenarioFeature.testScenarioID = command.TestScenarioID;
            testScenarioFeature.featureID = command.FeatureID;
            testScenarioFeature.executionOrder = command.ExecutionOrder;
            testScenarioFeature.isLoop = command.IsLoop;
            testScenarioFeature.statusID = command.StatusID;
            testScenarioFeature.toolsTestID = command.ToolsTestID;
            testScenarioFeature.testID = command.TestID;
            testScenarioFeature.createdByID = command.CreatedByID;
            testScenarioFeature.creationDate = command.CreationDate;
            testScenarioFeature.modifiedByID = command.ModifiedByID;
            testScenarioFeature.lastModifiedDate = DateTime.Now.ToString();
            testScenarioFeature.featureName = command.FeatureName;

            return testScenarioFeature;
        }
    }
}
