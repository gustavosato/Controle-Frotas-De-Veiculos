using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.TestCases;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.TestCases;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
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

