using Lean.Test.Cloud.Domain.Command.TestLogs;
using Lean.Test.Cloud.Domain.Entities.TestLogs;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface ITestLogService : IDisposable
    {
        void Add(MaintenanceTestLogCommand command);
        void Update(MaintenanceTestLogCommand command);
        Result<TestLog> GetByID(int logID);
        IPagedList<TestLog> GetAll(FilterTestLogCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int logID);
    }
}
