using ControleVeiculos.Domain.Command.DailyLogs;
using ControleVeiculos.Domain.Entities.DailyLogs;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
