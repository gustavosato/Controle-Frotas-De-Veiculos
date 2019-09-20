using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("AccountingEntries")]
    public class AccountingEntrieDapper
    {
        [ExplicitKey]
        public int accountingEntrieID { get; set; }
        public string classID { get; set; }
        public string categoryID { get; set; }
        public string subCategoryID { get; set; }
        public string accountID { get; set; }
        public string statusID { get; set; }
        public string valueToBeRealized { get; set; }
        public string competitionDate { get; set; }
        public string realizedValue{ get; set; }
        public string realizedDate { get; set; }
        public string dueDate { get; set; }
        public string interest { get; set; }
        public string invoiceNumber { get; set; }
        public string documentNumber { get; set; }
        public string description { get; set; }
        public string customerID { get; set; }
        public string demandID { get; set; }
        public string employeeID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}

