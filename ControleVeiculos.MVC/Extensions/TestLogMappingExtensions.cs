using Lean.Test.Cloud.Domain.Entities.TestLogs;
using Lean.Test.Cloud.MVC.Models.TestLogs;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class TestLogMappingExtensions
    {
        public static TestLogModel ToModel(this TestLog entity)
        {
            if (entity == null)
                return null;

            var model = new TestLogModel
            {
                LogID = entity.logID,
                TestID = entity.testID,
                StatusID = entity.statusID,
                StepName = entity.stepName,
                ExpectedResult = entity.expectedResult,
                ActualResult = entity.actualResult,
                PathEvidence = entity.pathEvidence,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,

            };

            return model;
        }
    }
}