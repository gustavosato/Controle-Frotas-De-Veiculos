using ControleVeiculos.Domain.Command.TestLogs;
using ControleVeiculos.Domain.Entities.TestLogs;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ITestLogRepository
    {
        void Add(TestLog testLog);
        void Update(TestLog testLog);
        TestLog GetByID(int logID);
        List<TestLog> GetAll(FilterTestLogCommand command);
        void Delete(int logID);
    }
}
