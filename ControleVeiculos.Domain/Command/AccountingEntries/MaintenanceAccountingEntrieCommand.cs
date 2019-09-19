namespace Lean.Test.Cloud.Domain.Command.AccountingEntries
{
    public class MaintenanceAccountingEntrieCommand
    {
        public int AccountingEntrieID { get; set; }
        public string ClassID { get; set; }
        public string CategoryID { get; set; }
        public string SubCategoryID { get; set; }
        public string AccountID { get; set; }
        public string StatusID { get; set; }
        public string ValueToBeRealized { get; set; }
        public string CompetitionDate { get; set; }
        public string RealizedValue { get; set; }
        public string RealizedDate { get; set; }
        public string DueDate { get; set; }
        public string Interest { get; set; }
        public string InvoiceNumber { get; set; }
        public string DocumentNumber { get; set; }
        public string CustomerID { get; set; }
        public string DemandID { get; set; }
        public string EmployeeID { get; set; }
        public string Description { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}

