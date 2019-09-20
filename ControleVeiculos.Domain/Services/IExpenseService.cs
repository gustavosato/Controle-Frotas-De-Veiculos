using ControleVeiculos.Domain.Command.Expenses;
using ControleVeiculos.Domain.Entities.Expenses;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IExpenseService : IDisposable
    {
        string Add(MaintenanceExpenseCommand command);
        void Update(MaintenanceExpenseCommand command);
        Result<Expense> GetByID(int expenseID);
        IPagedList<Expense> GetAll(FilterExpenseCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Expense> GetTotalByUsers(FilterExpenseCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int expenseID);
    }
}
