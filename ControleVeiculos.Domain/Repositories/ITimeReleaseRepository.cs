﻿using Lean.Test.Cloud.Domain.Command.TimeReleases;
using Lean.Test.Cloud.Domain.Entities.TimeReleases;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ITimeReleaseRepository
    {
        string Add(TimeRelease timeRelease);
        void Update(TimeRelease timeRelease);
        TimeRelease GetByID(int timeReleaseID);
        string GetTotalHours(string userID, string startDate, string endDate);
        List<TimeRelease> GetAll(FilterTimeReleaseCommand command);
        List<TimeRelease> GetTotalByUsers(FilterTimeReleaseCommand command);
        List<TimeRelease> GetTotalByUsersNoPage(FilterTimeReleaseCommand command);
        void Delete(int timeReleaseID);
        string GetAbsence(int userID, string registerDate);
        string GetNotAbsence(int userID, string registerDate);
        string GetApropriateByRangeTime(int timeReleaseID, int createdByID, string registerDate, string startWork, string endWork);
    }
}