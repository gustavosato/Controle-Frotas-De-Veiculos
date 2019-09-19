using Lean.Test.Cloud.Domain.Command.MovimentEmployees;
using Lean.Test.Cloud.Domain.Entities.MovimentEmployees;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IMovimentEmployeeService : IDisposable
    {
        void Add(MaintenanceMovimentEmployeeCommand command);
        void Update(MaintenanceMovimentEmployeeCommand command);
        Result<MovimentEmployee> GetByID(int movimentEmployeeID);
        IPagedList<MovimentEmployee> GetAll(FilterMovimentEmployeeCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        string GetApropriateByRangeTime(int movimentEmployeeID, int employeeID, string startWork, string endWork);
        void Delete(int movimentEmployeeID);
    }
}
