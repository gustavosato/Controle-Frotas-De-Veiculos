using ControleVeiculos.Domain.Command.DailyLogs;
using ControleVeiculos.Domain.Entities.DailyLogs;
using System;

namespace ControleVeiculos.Domain.Services
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
