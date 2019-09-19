using Lean.Test.Cloud.Domain.Command.TestScenarios;
using System;

namespace Lean.Test.Cloud.Domain.Entities.TestScenarios
{
    public static class TestScenarioExtensions
    {
        public static Result<TestScenario> GetTestScenario(this TestScenario testScenario)
        {
            return Result.Ok(0, "", testScenario);
        }

        public static TestScenario Map(this TestScenario testScenario, MaintenanceTestScenarioCommand command)
        {

            testScenario.testScenarioID = command.TestScenarioID;
            testScenario.testScenario = command.TestScenario;
            testScenario.description = command.Description;
            testScenario.statusID = command.StatusID;
            testScenario.executionOrder = command.ExecutionOrder;
            testScenario.startExecution = command.StartExecution;
            testScenario.endExecution = command.EndExecution;
            testScenario.timeExecution = command.TimeExecution;
            testScenario.testTypeID = command.TestTypeID;
            testScenario.executionTypeID = command.ExecutionTypeID;
            testScenario.createdByID = command.CreatedByID;
            testScenario.creationDate = command.CreationDate;
            testScenario.modifiedByID = command.ModifiedByID;
            testScenario.lastModifiedDate = DateTime.Now.ToString();
            testScenario.testPackageID = command.TestPackageID;


            return testScenario;
        }
    }
}
