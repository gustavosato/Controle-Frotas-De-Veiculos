namespace Lean.Test.Cloud.Domain.Entities.ApplicationSystems
{
    public class ApplicationSystem
    {
        public int applicationSystemID { get; set; }
        public string applicationSystemName { get; set; }
        public string description { get; set; }
        public string applicationTypeID { get; set; }
        public string customerID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
