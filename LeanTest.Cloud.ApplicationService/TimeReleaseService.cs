using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.TimeReleases;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.TimeReleases;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
{
    public class TimeReleaseService : BaseAppService, ITimeReleaseService
    {
        private readonly ITimeReleaseRepository _timeReleaseRepository;

        public TimeReleaseService(ITimeReleaseRepository timeReleaseRepository)
        {
            _timeReleaseRepository = timeReleaseRepository;
        }

        public string Add(MaintenanceTimeReleaseCommand command)
        {
            TimeRelease timeRelease = new TimeRelease();
            
            timeRelease = timeRelease.Map(command);

            return _timeReleaseRepository.Add(timeRelease);

        }

        public void Update(MaintenanceTimeReleaseCommand command)
        {
            TimeRelease timeRelease = new TimeRelease();

            timeRelease = timeRelease.Map(command);

            _timeReleaseRepository.Update(timeRelease);
        }

        public Result<TimeRelease> GetByID(int timeReleaseID)
        {
            var timeRelease = _timeReleaseRepository.GetByID(timeReleaseID);

            return Result.Ok<TimeRelease>(0, "", timeRelease);
        }

        public string GetTotalHours(string userID, string startDate, string endDate)
        {
            return _timeReleaseRepository.GetTotalHours(userID, startDate, endDate);
        }

        public IPagedList<TimeRelease> GetAll(FilterTimeReleaseCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var timeRelease = _timeReleaseRepository.GetAll(command);

            return new PagedList<TimeRelease>(timeRelease, pageIndex, pageSize);
        }

        public IPagedList<TimeRelease> GetTotalByUsers(FilterTimeReleaseCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var timeRelease = _timeReleaseRepository.GetTotalByUsers(command);

            return new PagedList<TimeRelease>(timeRelease, pageIndex, pageSize);
        }

        public List<TimeRelease> GetTotalByUsersNoPage(FilterTimeReleaseCommand command)
        {
            var timeRelease = _timeReleaseRepository.GetTotalByUsersNoPage(command);

            return new List<TimeRelease>(timeRelease);
        }

        public void Delete(int timeReleaseID)
        {
            _timeReleaseRepository.Delete(timeReleaseID);
        }

        public string GetAbsence(int userID, string registerDate)
        {
            return _timeReleaseRepository.GetAbsence(userID, registerDate);
        }

        public string GetNotAbsence(int userID, string registerDate)
        {
            return _timeReleaseRepository.GetNotAbsence(userID, registerDate);
        }

        public string GetApropriateByRangeTime(int timeReleaseID, int createdByID, string registerDate, string startWork, string endWork)
        {
            return _timeReleaseRepository.GetApropriateByRangeTime(timeReleaseID, createdByID, registerDate, startWork, endWork);
        }

      
    }
}

