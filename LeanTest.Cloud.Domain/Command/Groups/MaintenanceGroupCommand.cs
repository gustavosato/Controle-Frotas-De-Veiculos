namespace Lean.Test.Cloud.Domain.Command.Groups
{
    public class MaintenanceGroupCommand
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public bool IsSystem { get; set; }
        public string Description { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}

