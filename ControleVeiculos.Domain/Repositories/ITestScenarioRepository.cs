using ControleVeiculos.Domain.Command.TestScenarios;
using ControleVeiculos.Domain.Entities.TestScenarios;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
