using Lean.Test.Cloud.Domain.Command.DailyLogs;
using Lean.Test.Cloud.Domain.Entities.DailyLogs;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IDailyLogRepository
    {
        string Add(DailyLog dailyLog);
        void Update(DailyLog dailyLog);
        DailyLog GetByID(int dailyLogID);
        List<DailyLog> GetAll(FilterDailyLogCommand command);
        void Delete(int dailyLogID);
    }
}
