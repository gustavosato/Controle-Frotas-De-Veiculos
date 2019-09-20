using ControleVeiculos.Domain.Command.DailyLogComments;
using ControleVeiculos.Domain.Entities.DailyLogComments;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
