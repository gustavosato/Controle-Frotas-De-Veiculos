using Lean.Test.Cloud.Domain.Command.TimeReleases;
using System;

namespace Lean.Test.Cloud.Domain.Entities.TimeReleases
{
    public static class TimeReleaseExtensions
    {
        public static Result<TimeRelease> GetTestScenariosFeature(this TimeRelease timeRelease)
        {
            return Result.Ok(0, "", timeRelease);
        }

        public static TimeRelease Map(this TimeRelease timeRelease, MaintenanceTimeReleaseCommand command)
        {

            timeRelease.timeReleaseID = command.TimeReleaseID;
            timeRelease.registerDate = command.RegisterDate;
            timeRelease.startWork = command.StartWork;
            timeRelease.endWork = command.EndWork;
            timeRelease.demandID = command.DemandID;
            timeRelease.customerID = command.CustomerID;
            timeRelease.isApproved = command.IsApproved;
            timeRelease.activityID = command.ActivityID;
            timeRelease.approvedByID = command.ApprovedByID;
            timeRelease.approvedDate = command.ApprovedDate;
            timeRelease.description = command.Description;
            timeRelease.reasonChange = command.ReasonChange;
            timeRelease.createdByID = command.CreatedByID;
            timeRelease.creationDate = command.CreationDate;
            timeRelease.modifiedByID = command.ModifiedByID;
            timeRelease.lastModifiedDate = command.LastModifiedDate;
            timeRelease.totalTime = command.TotalTime;

            return timeRelease;
        }
    }
}
