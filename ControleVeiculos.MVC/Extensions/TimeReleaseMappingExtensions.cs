using Lean.Test.Cloud.Domain.Entities.TimeReleases;
using Lean.Test.Cloud.MVC.Models.TimeReleases;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class TimeReleaseMappingExtensions
    {
        public static TimeReleaseModel ToModel(this TimeRelease entity)
        {
            if (entity == null)
                return null;

            var model = new TimeReleaseModel
            {
                TimeReleaseID = entity.timeReleaseID,
                RegisterDate = entity.registerDate,
                StartWork = entity.startWork,
                EndWork = entity.endWork,
                CustomerID = entity.customerID,
                DemandID = entity.demandID,
                IsApproved = entity.isApproved,
                ActivityID = entity.activityID,
                ApprovedByID = entity.approvedByID,
                ApprovedDate = entity.approvedDate,
                Description = entity.description,
                ReasonChange = entity.reasonChange,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,
                TotalTime = entity.totalTime
            };

            return model;
        }
    }
}