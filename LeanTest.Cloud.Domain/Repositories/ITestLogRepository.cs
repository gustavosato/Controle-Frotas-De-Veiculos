using Lean.Test.Cloud.Domain.Command.TestLogs;
using Lean.Test.Cloud.Domain.Entities.TestLogs;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ITestLogRepository
    {
        void Add(TestLog testLog);
        void Update(TestLog testLog);
        TestLog GetByID(int logID);
        List<TestLog> GetAll(FilterTestLogCommand command);
        void Delete(int logID);
    }
}
