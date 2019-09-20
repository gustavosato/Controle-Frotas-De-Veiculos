using ControleVeiculos.Domain.Command.TimeReleases;
using ControleVeiculos.Domain.Entities.TimeReleases;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface ITimeReleaseService : IDisposable
    {
        string Add(MaintenanceTimeReleaseCommand command);
        void Update(MaintenanceTimeReleaseCommand command);
        Result<TimeRelease> GetByID(int timeReleaseID);
        string GetTotalHours(string userID, string StartDate, string endDate);
        string GetAbsence(int userID, string date);
        string GetNotAbsence(int userID, string date);
        string GetApropriateByRangeTime(int timeReleaseID, int createdByID, string registerDate, string startWork, string endWork);
        IPagedList<TimeRelease> GetAll(FilterTimeReleaseCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<TimeRelease> GetTotalByUsers(FilterTimeReleaseCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        List<TimeRelease> GetTotalByUsersNoPage(FilterTimeReleaseCommand command);
        void Delete(int timeReleaseID);
    }
}
