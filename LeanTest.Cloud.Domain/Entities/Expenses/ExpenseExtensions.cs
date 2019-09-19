using Lean.Test.Cloud.Domain.Command.Expenses;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Expenses
{
    public static class ExpenseExtensions
    {
        public static Result<Expense> GetExpense(this Expense expense)
        {
            return Result.Ok(0, "", expense);
        }

        public static Expense Map(this Expense expense, MaintenanceExpenseCommand command)
        {

            expense.expenseID = command.ExpenseID;
            expense.description = command.Description;
            expense.registerDate = command.RegisterDate;
            expense.refundable = command.Refundable;
            expense.typeExpenseID = command.TypeExpenseID;
            expense.demandID = command.DemandID;
            expense.statusID = command.StatusID;
            expense.departmentID = command.DepartmentID;
            expense.customerID = command.CustomerID;
            expense.subTotal = command.SubTotal;
            expense.kilometer = command.Kilometer;
            expense.amountExpense = command.AmountExpense;
            expense.refundable = command.Refundable;
            expense.approvedByID = command.ApprovedByID;
            expense.approvedDate = command.ApprovedDate;
            expense.createdByID = command.CreatedByID;
            expense.creationDate = command.CreationDate;
            expense.modifiedByID = command.ModifiedByID;
            expense.lastModifiedDate = command.LastModifiedDate;

            return expense;
        }
    }
}
