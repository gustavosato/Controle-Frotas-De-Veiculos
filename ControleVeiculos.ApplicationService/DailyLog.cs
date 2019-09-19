using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.DailyLogs;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.DailyLogs;

namespace Lean.Test.Cloud.ApplicationService
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

