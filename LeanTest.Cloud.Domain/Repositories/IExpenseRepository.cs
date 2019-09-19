using Lean.Test.Cloud.Domain.Command.Expenses;
using Lean.Test.Cloud.Domain.Entities.Expenses;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IExpenseRepository
    {
        string Add(Expense expense);
        void Update(Expense expense);
        Expense GetByID(int expenseID);
        List<Expense> GetAll(FilterExpenseCommand command);
        List<Expense> GetTotalByUsers(FilterExpenseCommand command);
        void Delete(int expenseID);
    }
}
