using Lean.Test.Cloud.Domain.Command.DailyLogComments;
using Lean.Test.Cloud.Domain.Entities.DailyLogComments;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IDailyLogCommentService : IDisposable
    {
        void Add(MaintenanceDailyLogCommentCommand command);
        void Update(MaintenanceDailyLogCommentCommand command);
        Result<DailyLogComment> GetByID(int dailyLogsCommentID);
        IPagedList<DailyLogComment> GetAll(FilterDailyLogCommentCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int dailyLogsCommentID);
    }
}
