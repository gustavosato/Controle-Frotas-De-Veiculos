namespace Lean.Test.Cloud.Domain.Entities.Templates
{
    public class Template
    {
        public int templateID { get; set; }
        public string templateName { get; set; }
        public string description { get; set; }
        public string domainID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
