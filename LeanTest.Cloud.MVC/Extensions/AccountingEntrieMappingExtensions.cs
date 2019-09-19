using Lean.Test.Cloud.Domain.Entities.AccountingEntries;
using Lean.Test.Cloud.MVC.Models.AccountingEntries;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class AccountingEntrieMappingExtensions
    {
        public static AccountingEntrieModel ToModel(this AccountingEntrie entity)
        {
            if (entity == null)
                return null;

            var model = new AccountingEntrieModel
            {
                AccountingEntrieID = entity.accountingEntrieID,
                ClassID = entity.classID,
                CategoryID = entity.categoryID,
                SubCategoryID = entity.subCategoryID,
                AccountID = entity.accountID,
                StatusID = entity.statusID,
                ValueToBeRealized = entity.valueToBeRealized,
                CompetitionDate = entity.competitionDate,
                RealizedValue = entity.realizedValue,
                RealizedDate = entity.realizedDate,
                DueDate = entity.dueDate,
                Interest = entity.interest,
                InvoiceNumber = entity.invoiceNumber,
                DocumentNumber = entity.documentNumber,
                CustomerID = entity.customerID,
                EmployeeID = entity.employeeID,
                DemandID = entity.demandID,
                Description = entity.description,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}