using Lean.Test.Cloud.Domain.Command.DailyLogComments;
using Lean.Test.Cloud.Domain.Entities.DailyLogComments;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IDailyLogCommentRepository
    {
        void Add(DailyLogComment DailyLogComment);
        void Update(DailyLogComment DailyLogComment);
        DailyLogComment GetByID(int dailyLogsCommentID);
        List<DailyLogComment> GetAll(FilterDailyLogCommentCommand command);
        void Delete(int dailyLogsCommentID);
    }
}
