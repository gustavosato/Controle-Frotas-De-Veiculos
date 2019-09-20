using ControleVeiculos.Domain.Command.Expenses;
using ControleVeiculos.Domain.Entities.Expenses;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
