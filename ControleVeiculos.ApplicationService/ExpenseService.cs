using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Expenses;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Expenses;

namespace ControleVeiculos.ApplicationService
{
    public class ExpenseService : BaseAppService, IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public string Add(MaintenanceExpenseCommand command)
        {
            Expense expense = new Expense();

            expense = expense.Map(command);
            
            return _expenseRepository.Add(expense);
        }

        public void Update(MaintenanceExpenseCommand command)
        {
            Expense expense = new Expense();

            expense = expense.Map(command);

            _expenseRepository.Update(expense);
        }

        public Result<Expense> GetByID(int expenseID)
        {
            var expense = _expenseRepository.GetByID(expenseID);

            return Result.Ok<Expense>(0, "", expense);
        }

        public IPagedList<Expense> GetAll(FilterExpenseCommand filterExpenseCommand, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var expense = _expenseRepository.GetAll(filterExpenseCommand);

            return new PagedList<Expense>(expense, pageIndex, pageSize);
        }

        public IPagedList<Expense> GetTotalByUsers(FilterExpenseCommand filterExpenseCommand, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var expense = _expenseRepository.GetTotalByUsers(filterExpenseCommand);

            return new PagedList<Expense>(expense, pageIndex, pageSize);
        }

        public void Delete(int expenseID)
        {
            _expenseRepository.Delete(expenseID);
        }
    }
}

