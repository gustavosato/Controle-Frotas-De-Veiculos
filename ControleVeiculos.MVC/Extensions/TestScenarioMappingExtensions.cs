using ControleVeiculos.Domain.Entities.TestScenarios;
using ControleVeiculos.MVC.Models.TestScenarios;

namespace ControleVeiculos.MVC.Extensions
{
    public static class TestScenarioMappingExtensions
    {
        public static TestScenarioModel ToModel(this TestScenario entity)
        {
            if (entity == null)
                return null;

            var model = new TestScenarioModel
            {
                TestScenarioID = entity.testScenarioID,
                TestScenario = entity.testScenario,
                Description = entity.description,
                StatusID = entity.statusID,
                ExecutionOrder = entity.executionOrder,
                StartExecution = entity.startExecution,
                EndExecution = entity.endExecution,
                TimeExecution = entity.timeExecution,
                TestTypeID = entity.testTypeID,
                ExecutionTypeID = entity.executionTypeID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,
                TestPackageID = entity.lastModifiedDate,

            };

            return model;
        }
    }
}