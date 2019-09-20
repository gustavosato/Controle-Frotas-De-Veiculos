using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.DailyLogs;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.DailyLogs;

namespace ControleVeiculos.ApplicationService
{
    public class DailyLogService : BaseAppService, IDailyLogService
    {
        private readonly IDailyLogRepository _dailyLogRepository;

        public DailyLogService(IDailyLogRepository dailyLogRepository)
        {
            _dailyLogRepository = dailyLogRepository;
        }

        public string Add(MaintenanceDailyLogCommand command)
        {
            DailyLog dailyLog = new DailyLog();

            dailyLog = dailyLog.Map(command);

            return _dailyLogRepository.Add(dailyLog);
        }

        public void Update(MaintenanceDailyLogCommand command)
        {
            DailyLog dailyLog = new DailyLog();

            dailyLog = dailyLog.Map(command);

            _dailyLogRepository.Update(dailyLog);
        }

        public Result<DailyLog> GetByID(int dailyLogID)
        {
            var dailyLog = _dailyLogRepository.GetByID(dailyLogID);

            return Result.Ok<DailyLog>(0, "", dailyLog);
        }

        public IPagedList<DailyLog> GetAll(FilterDailyLogCommand filterDailyLogCommand, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var dailyLog = _dailyLogRepository.GetAll(filterDailyLogCommand);

            return new PagedList<DailyLog>(dailyLog, pageIndex, pageSize);
        }

        public void Delete(int dailyLogID)
        {
            _dailyLogRepository.Delete(dailyLogID);
        }
    }
}

