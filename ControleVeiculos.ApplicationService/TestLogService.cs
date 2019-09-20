using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.TestLogs;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.TestLogs;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class TestLogService : BaseAppService, ITestLogService
    {
        private readonly ITestLogRepository _testLogRepository;

        public TestLogService(ITestLogRepository testLogRepository)
        {
            _testLogRepository = testLogRepository;
        }

        public void Add(MaintenanceTestLogCommand command)
        {
            TestLog testLog = new TestLog();

            testLog = testLog.Map(command);

            _testLogRepository.Add(testLog);
        }

        public void Update(MaintenanceTestLogCommand command)
        {
            TestLog testLog = new TestLog();

            testLog = testLog.Map(command);

            _testLogRepository.Update(testLog);
        }

        public Result<TestLog> GetByID(int logID)
        {
            var testLog = _testLogRepository.GetByID(logID);

            return Result.Ok<TestLog>(0, "", testLog);
        }

        public IPagedList<TestLog> GetAll(FilterTestLogCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var testLog = _testLogRepository.GetAll(command);

            return new PagedList<TestLog>(testLog, pageIndex, pageSize);
        }

        public void Delete(int logID)
        {
            _testLogRepository.Delete(logID);
        }
    }
}

