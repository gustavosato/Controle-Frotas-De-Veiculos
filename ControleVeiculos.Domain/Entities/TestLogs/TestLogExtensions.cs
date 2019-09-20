using ControleVeiculos.Domain.Command.TestLogs;
using System;

namespace ControleVeiculos.Domain.Entities.TestLogs
{
    public static class TestLogExtensions
    {
        public static Result<TestLog> GetTestLog(this TestLog testLog)
        {
            return Result.Ok(0, "", testLog);
        }

        public static TestLog Map(this TestLog testLog, MaintenanceTestLogCommand command)
        {

            testLog.logID = command.LogID;
            testLog.testID = command.TestID;
            testLog.statusID = command.StatusID;
            testLog.stepName = command.StepName;
            testLog.expectedResult = command.ExpectedResult;
            testLog.actualResult = command.ActualResult;
            testLog.pathEvidence = command.PathEvidence;
            testLog.createdByID = command.CreatedByID;
            testLog.creationDate = command.CreationDate;
            testLog.modifiedByID = command.ModifiedByID;
            testLog.lastModifiedDate = DateTime.Now.ToString();

            return testLog;
        }
    }
}
