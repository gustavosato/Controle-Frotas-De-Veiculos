using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Contacts")]
    public class ContactDapper
    {
        [ExplicitKey]
        public int contactID { get; set; }
        public string contactName { get; set; }
        public string email { get; set; }
        public string cellNumber { get; set; }
        public string telNumber { get; set; }
        public string functionID { get; set; }
        public string customerID { get; set; }
        public string description { get; set; }
        public string feature { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
