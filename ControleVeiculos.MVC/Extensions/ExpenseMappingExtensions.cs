using ControleVeiculos.Domain.Entities.Expenses;
using ControleVeiculos.MVC.Models.Expenses;

namespace ControleVeiculos.MVC.Extensions
{
    public static class ExpenseMappingExtensions
    {
        public static ExpenseModel ToModel(this Expense entity)
        {
            if (entity == null)
                return null;

            var model = new ExpenseModel
            {
                ExpenseID = entity.expenseID,
                RegisterDate = entity.registerDate,
                Description = entity.description,
                TypeExpenseID = entity.typeExpenseID,
                DemandID = entity.demandID,
                StatusID = entity.statusID,
                CustomerID = entity.customerID,
                DepartmentID = entity.departmentID,
                SubTotal = entity.subTotal,
                Kilometer = entity.kilometer,
                AmountExpense = entity.amountExpense,
                Refundable = entity.refundable,
                ApprovedByID = entity.approvedByID,
                ApprovedDate = entity.approvedDate,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}