namespace ControleVeiculos.Domain.Entities.Customers
{
    public class Customer
    {
        public int customerID { get; set;}
        public string customerName { get; set; }
        public string description { get; set; }
        public string isActive { get; set; }
        public string segmentID { get; set; }
        public string typeID { get; set; }
        public string site { get; set; }
        public string address { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
