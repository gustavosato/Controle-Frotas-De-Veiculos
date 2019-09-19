using Lean.Test.Cloud.Domain.Entities.TestCases;
using Lean.Test.Cloud.MVC.Models.TestCases;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class TestCaseMappingExtensions
    {
        public static TestCaseModel ToModel(this TestCase entity)
        {
            if (entity == null)
                return null;

            var model = new TestCaseModel
            {
                TestCaseID = entity.testCaseID,
                StatusID = entity.statusID,
                TestCase = entity.testCase,
                Description = entity.description,
                Precondition = entity.precondition,
                ExpectedResult = entity.expectedResult,
                FeatureID = entity.featureID,
                TestScenarioID = entity.testScenarioID,
                ExecutionOrder = entity.executionOrder,
                FlowTestID = entity.flowTestID,
                StartExecution = entity.startExecution,
                EndExecution = entity.endExecution,
                TimeExecution = entity.timeExecution,
                Release = entity.release,
                Cycle = entity.cycle,
                TestTypeID = entity.testTypeID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,

            };

            return model;
        }
    }
}