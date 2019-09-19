using Lean.Test.Cloud.Domain.Command.TestScenarios;
using Lean.Test.Cloud.Domain.Entities.TestScenarios;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ITestScenarioRepository
    {
        void Add(TestScenario testScenario);
        void Update(TestScenario testScenario);
        TestScenario GetByID(int testScenarioID);
        List<TestScenario> GetAll(FilterTestScenarioCommand command);
        void Delete(int testScenarioID);
    }
}
