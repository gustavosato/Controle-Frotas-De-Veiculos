using ControleVeiculos.Domain.Command.TestCases;
using System;

namespace ControleVeiculos.Domain.Entities.TestCases
{
    public static class TestCaseExtensions
    {
        public static Result<TestCase> GetTestCase(this TestCase testCase)
        {
            return Result.Ok(0, "", testCase);
        }

        public static TestCase Map(this TestCase testCase, MaintenanceTestCaseCommand command)
        {

            testCase.testCaseID = command.TestCaseID;
            testCase.statusID = command.StatusID;
            testCase.testCase = command.TestCase;
            testCase.description = command.Description;
            testCase.precondition = command.Precondition;
            testCase.expectedResult = command.ExpectedResult;
            testCase.featureID = command.FeatureID;
            testCase.testScenarioID = command.TestScenarioID;
            testCase.executionOrder = command.ExecutionOrder;
            testCase.flowTestID = command.FlowTestID;
            testCase.startExecution = command.StartExecution;
            testCase.endExecution = command.EndExecution;
            testCase.timeExecution = command.TimeExecution;
            testCase.release = command.Release;
            testCase.cycle = command.Cycle;
            testCase.testTypeID = command.TestTypeID;
            testCase.createdByID = command.CreatedByID;
            testCase.creationDate = command.CreationDate;
            testCase.modifiedByID = command.ModifiedByID;
            testCase.lastModifiedDate = command.LastModifiedDate;

            return testCase;
        }
    }
}
