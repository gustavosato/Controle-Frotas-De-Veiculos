using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("SaleEvents")]
    public class SaleEventDapper
    {
        [ExplicitKey]
        public int saleEvent { get; set; }
        public string summary { get; set; }
        public string registerDate { get; set; }
        public string nextStepID { get; set; }
        public string targetDate { get; set; }
        public string description { get; set; }
        public string customerID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}

