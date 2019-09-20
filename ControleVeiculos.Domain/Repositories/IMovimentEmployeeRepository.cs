using ControleVeiculos.Domain.Command.MovimentEmployees;
using ControleVeiculos.Domain.Entities.MovimentEmployees;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
