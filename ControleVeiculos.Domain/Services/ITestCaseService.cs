using Lean.Test.Cloud.Domain.Command.TestCases;
using Lean.Test.Cloud.Domain.Entities.TestCases;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface ITestCaseService : IDisposable
    {
        void Add(MaintenanceTestCaseCommand command);
        void Update(MaintenanceTestCaseCommand command);
        Result<TestCase> GetByID(int testCaseID);
        IPagedList<TestCase> GetAll(FilterTestCaseCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int testCaseID);
    }
}
