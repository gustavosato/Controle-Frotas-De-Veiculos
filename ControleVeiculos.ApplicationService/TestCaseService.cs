using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.TestCases;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.TestCases;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class TestCaseService : BaseAppService, ITestCaseService
    {
        private readonly ITestCaseRepository _testCaseRepository;

        public TestCaseService(ITestCaseRepository testCaseRepository)
        {
            _testCaseRepository = testCaseRepository;
        }

        public void Add(MaintenanceTestCaseCommand command)
        {
            TestCase testCase = new TestCase();

            testCase = testCase.Map(command);

            _testCaseRepository.Add(testCase);
        }

        public void Update(MaintenanceTestCaseCommand command)
        {
            TestCase testCase = new TestCase();

            testCase = testCase.Map(command);

            _testCaseRepository.Update(testCase);
        }

        public Result<TestCase> GetByID(int testCaseID)
        {
            var testCase = _testCaseRepository.GetByID(testCaseID);

            return Result.Ok<TestCase>(0, "", testCase);
        }

        public IPagedList<TestCase> GetAll(FilterTestCaseCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var testCases = _testCaseRepository.GetAll(command);

            return new PagedList<TestCase>(testCases, pageIndex, pageSize);
        }

        public void Delete(int testCaseID)
        {
            _testCaseRepository.Delete(testCaseID);
        }
    }
}

