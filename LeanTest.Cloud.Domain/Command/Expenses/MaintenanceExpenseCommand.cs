namespace Lean.Test.Cloud.Domain.Command.Expenses
{
    public class MaintenanceExpenseCommand
    {
        public int ExpenseID { get; set; }
        public string Description { get; set; }
        public string RegisterDate { get; set; }
        public string TypeExpenseID { get; set; }
        public string DemandID { get; set; }
        public string StatusID { get; set; }
        public string CustomerID { get; set ; }
        public string DepartmentID { get; set; }
        public string SubTotal { get; set; }
        public string Kilometer { get; set; }
        public string AmountExpense { get; set;}
        public string Refundable { get; set; }
        public string ApprovedByID { get; set; }
        public string ApprovedDate { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
