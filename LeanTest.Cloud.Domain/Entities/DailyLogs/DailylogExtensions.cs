using Lean.Test.Cloud.Domain.Command.DailyLogs;
using System;

namespace Lean.Test.Cloud.Domain.Entities.DailyLogs
{
    public static class DailyLogExtensions
    {
        public static Result<DailyLog> GetDailyLog(this DailyLog dailyLog)
        {
            return Result.Ok(0, "", dailyLog);
        }

        public static DailyLog Map(this DailyLog dailyLog, MaintenanceDailyLogCommand command)
        {

            dailyLog.dailyLogID = command.DailyLogID;
            dailyLog.description = command.Description;
            dailyLog.demandID = command.DemandID;
            dailyLog.isInternal = command.IsInternal;
            dailyLog.createdByID = command.CreatedByID;
            dailyLog.creationDate = command.CreationDate;
            dailyLog.modifiedByID = command.ModifiedByID;
            dailyLog.lastModifiedDate = command.LastModifiedDate;

            return dailyLog;
        }
    }
}
