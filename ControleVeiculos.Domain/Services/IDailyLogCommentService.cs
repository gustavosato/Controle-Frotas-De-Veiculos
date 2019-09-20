using ControleVeiculos.Domain.Command.DailyLogComments;
using ControleVeiculos.Domain.Entities.DailyLogComments;
using System;

namespace ControleVeiculos.Domain.Services
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
