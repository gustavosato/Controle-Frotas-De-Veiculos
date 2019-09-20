using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Expenses")]
    public class ExpenseDapper
    {
        [ExplicitKey]
        public int expenseID { get; set;}
        public string registerDate { get; set; }
        public string description { get; set; }
        public string typeExpenseID { get; set; }
        public string demandID { get; set; }
        public string statusID { get; set; }
        public string customerID { get; set; }
        public string departmentID { get; set; }
        public string subTotal { get; set; }
        public string Kilometer { get; set; }
        public string amountExpense { get; set; }
        public string refundable { get; set; }
        public string approvedByID { get; set; }
        public string approvedDate { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}

