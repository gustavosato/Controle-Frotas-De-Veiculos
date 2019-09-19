using Lean.Test.Cloud.Domain.Command.Expenses;
using Lean.Test.Cloud.Domain.Entities.Expenses;
using System;

namespace Lean.Test.Cloud.Domain.Services
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
