using ControleVeiculos.Domain.Command.AccountingEntries;
using System;

namespace ControleVeiculos.Domain.Entities.AccountingEntries
{
    public static class AccountingEntrieExtensions
    {
        public static Result<AccountingEntrie> GetAccountingEntrie(this AccountingEntrie accountingEntrie)
        {
            return Result.Ok(0, "", accountingEntrie);
        }

        public static AccountingEntrie Map(this AccountingEntrie accountingEntrie, MaintenanceAccountingEntrieCommand command)
        {
            accountingEntrie.accountingEntrieID = command.AccountingEntrieID;
            accountingEntrie.classID = command.ClassID;
            accountingEntrie.categoryID = command.CategoryID;
            accountingEntrie.subCategoryID = command.SubCategoryID;
            accountingEntrie.accountID = command.AccountID;
            accountingEntrie.statusID = command.StatusID;
            accountingEntrie.valueToBeRealized = command.ValueToBeRealized;
            accountingEntrie.competitionDate = command.CompetitionDate;
            accountingEntrie.realizedValue = command.RealizedValue;
            accountingEntrie.realizedDate = command.RealizedDate;
            accountingEntrie.dueDate = command.DueDate;
            accountingEntrie.interest = command.Interest;
            accountingEntrie.invoiceNumber = command.InvoiceNumber;
            accountingEntrie.documentNumber = command.DocumentNumber;
            accountingEntrie.customerID = command.CustomerID;
            accountingEntrie.demandID = command.DemandID;
            accountingEntrie.employeeID = command.EmployeeID;
            accountingEntrie.description = command.Description;
            accountingEntrie.createdByID = command.CreatedByID;
            accountingEntrie.creationDate = command.CreationDate;
            accountingEntrie.modifiedByID = command.ModifiedByID;
            accountingEntrie.lastModifiedDate = command.LastModifiedDate;

            return accountingEntrie;
        }
    }
}
