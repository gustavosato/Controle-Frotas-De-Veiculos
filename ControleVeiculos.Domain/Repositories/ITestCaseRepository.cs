using Lean.Test.Cloud.Domain.Command.TestCases;
using Lean.Test.Cloud.Domain.Entities.TestCases;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ITestCaseRepository
    {
        void Add(TestCase testCase);
        void Update(TestCase testCase);
        TestCase GetByID(int testCaseID);
        List<TestCase> GetAll(FilterTestCaseCommand command);
        void Delete(int testCaseID);
    }
}
