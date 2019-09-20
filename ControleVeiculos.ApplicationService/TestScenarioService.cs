using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.TestScenarios;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.TestScenarios;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class TestScenarioService : BaseAppService, ITestScenarioService
    {
        private readonly ITestScenarioRepository _testScenarioRepository;

        public TestScenarioService(ITestScenarioRepository testScenarioRepository)
        {
            _testScenarioRepository = testScenarioRepository;
        }

        public void Add(MaintenanceTestScenarioCommand command)
        {
            TestScenario testScenario = new TestScenario();

            testScenario = testScenario.Map(command);

            _testScenarioRepository.Add(testScenario);
        }

        public void Update(MaintenanceTestScenarioCommand command)
        {
            TestScenario testScenario = new TestScenario();

            testScenario = testScenario.Map(command);

            _testScenarioRepository.Update(testScenario);
        }

        public Result<TestScenario> GetByID(int testScenarioID)
        {
            var testScenario = _testScenarioRepository.GetByID(testScenarioID);

            return Result.Ok<TestScenario>(0, "", testScenario);
        }

        public IPagedList<TestScenario> GetAll(FilterTestScenarioCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var testScenario = _testScenarioRepository.GetAll(command);

            return new PagedList<TestScenario>(testScenario, pageIndex, pageSize);
        }

        public void Delete(int testScenarioID)
        {
            _testScenarioRepository.Delete(testScenarioID);
        }
    }
}

