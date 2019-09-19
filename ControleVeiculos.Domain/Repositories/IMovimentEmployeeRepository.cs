using Lean.Test.Cloud.Domain.Command.MovimentEmployees;
using Lean.Test.Cloud.Domain.Entities.MovimentEmployees;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IMovimentEmployeeRepository
    {
        void Add(MovimentEmployee movimentEmployee);
        void Update(MovimentEmployee movimentEmployee);
        MovimentEmployee GetByID(int movimentEmployeeID);
        List<MovimentEmployee> GetAll(FilterMovimentEmployeeCommand command);
        void Delete(int movimentEmployeeID);
        string GetApropriateByRangeTime(int movimentEmployeeID, int employeeID, string startDate, string endDate);

    }
}
