namespace Lean.Test.Cloud.Domain.Command.Supports
{
    public class MaintenanceSupportCommand
    {
        public int SupportID { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string SeverityID { get; set; }
        public string StatusID { get; set; }
        public string PriorityID { get; set; }
        public string TypeID { get; set; }
        public string AssingToID { get; set; }
        public string ResolutionDate { get; set; }
        public string CustomerID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
