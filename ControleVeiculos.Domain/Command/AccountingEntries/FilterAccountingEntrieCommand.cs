namespace ControleVeiculos.Domain.Command.AccountingEntries
{  public class FilterAccountingEntrieCommand
    {
        public string ClassID { get; set; }
        public string CategoryID { get; set; }
        public string SubCategoryID { get; set; }
        public string AccountID { get; set; }

        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public string DemandID { get; set; }
        public string StatusID { get; set; }

        public string InvoiceNumber { get; set; }
        public string DocumentNumber { get; set; }

        public string ValueToBeRealized { get; set; }
        public string RealizedValue { get; set; }

        public string CompetitionStartDate { get; set; }
        public string CompetitionEndDate { get; set; }

        public string StartDueDate { get; set; }
        public string EndDueDate { get; set; }

        public string StartDateRealized { get; set; }
        public string EndDateRealized { get; set; }
    }
}

