namespace Lean.Test.Cloud.Domain.Entities.Groups
{
    public class Group
    {

        public int groupID { get; set; }
        public string groupName { get; set; }
        public bool isSystem { get; set; }
        public string domainID { get; set; }
        public string description { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}

