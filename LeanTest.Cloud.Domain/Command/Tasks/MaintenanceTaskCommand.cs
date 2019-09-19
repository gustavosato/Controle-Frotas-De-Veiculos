namespace Lean.Test.Cloud.Domain.Command.Tasks
{
    public class MaintenanceTaskCommand
    {
        public int TaskID { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string AssignToID { get; set; }
        public string DemandID { get; set; }
        public string CustomerID { get; set; }
        public string StatusID { get; set; }
        public string TargetDate { get; set; }
        public string ClosingDate { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}
