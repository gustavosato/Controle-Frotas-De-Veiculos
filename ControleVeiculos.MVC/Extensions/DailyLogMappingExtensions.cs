using Lean.Test.Cloud.Domain.Entities.DailyLogs;
using Lean.Test.Cloud.MVC.Models.DailyLogs;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class DailyLogMappingExtensions
    {
        public static DailyLogModel ToModel(this DailyLog entity)
        {
            if (entity == null)
                return null;

            var model = new DailyLogModel
            {
                DailyLogID = entity.dailyLogID,
                Description = entity.description,
                DemandID = entity.demandID,
                IsInternal = entity.isInternal,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}