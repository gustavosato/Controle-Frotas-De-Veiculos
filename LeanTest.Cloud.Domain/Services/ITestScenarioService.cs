using Lean.Test.Cloud.Domain.Command.TestScenarios;
using Lean.Test.Cloud.Domain.Entities.TestScenarios;
using System;

namespace Lean.Test.Cloud.Domain.Services
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
