using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Licenses")]
    public class LicenseDapper
    {
        [ExplicitKey]
        public int licenseID { get; set;}
        public string licenseCode  { get; set;}
        public string customerID { get; set;}
        public string expirationDate{ get; set;}
        public string licenseTypeID { get; set;}
        public string hostName { get; set; }
        public string macAddress { get; set; }
        public string description { get; set; }
        public string license { get; set; }
        public string approvedByID { get; set; }
        public string approvedDate { get; set; }
        public string createdByID {get; set;}
        public string creationDate {get; set;}
        public string modifiedByID  {get; set;}
        public string lastModifiedDate  {get; set;}
    }
}
