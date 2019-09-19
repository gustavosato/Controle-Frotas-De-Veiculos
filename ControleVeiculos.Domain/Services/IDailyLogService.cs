using Lean.Test.Cloud.Domain.Command.DailyLogs;
using Lean.Test.Cloud.Domain.Entities.DailyLogs;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IDailyLogService : IDisposable
    {
        string Add(MaintenanceDailyLogCommand command);
        void Update(MaintenanceDailyLogCommand command);
        Result<DailyLog> GetByID(int dailyLogID);
        IPagedList<DailyLog> GetAll(FilterDailyLogCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int dailyLogID);
    }
}
