using ControleVeiculos.Domain.Command.TestLogs;
using ControleVeiculos.Domain.Entities.TestLogs;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ITestLogService : IDisposable
    {
        void Add(MaintenanceTestLogCommand command);
        void Update(MaintenanceTestLogCommand command);
        Result<TestLog> GetByID(int logID);
        IPagedList<TestLog> GetAll(FilterTestLogCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int logID);
    }
}
