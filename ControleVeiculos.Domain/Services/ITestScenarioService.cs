using ControleVeiculos.Domain.Command.TestScenarios;
using ControleVeiculos.Domain.Entities.TestScenarios;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ITestScenarioService : IDisposable
    {
        void Add(MaintenanceTestScenarioCommand command);
        void Update(MaintenanceTestScenarioCommand command);
        Result<TestScenario> GetByID(int testScenarioID);
        IPagedList<TestScenario> GetAll(FilterTestScenarioCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int testScenarioID);
    }
}
