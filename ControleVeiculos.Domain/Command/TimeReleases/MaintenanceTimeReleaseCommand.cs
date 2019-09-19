namespace Lean.Test.Cloud.Domain.Command.TimeReleases
{
    public class MaintenanceTimeReleaseCommand
    {
        public int TimeReleaseID { get; set; }
        public string RegisterDate { get; set; }
        public string StartWork { get; set; }
        public string EndWork { get; set; }
        public string DemandID { get; set; }
        public string CustomerID { get; set; }
        public bool IsApproved { get; set; }
        public string ActivityID { get; set; }
        public string ApprovedByID { get; set; }
        public string ApprovedDate { get; set; }
        public string Description { get; set; }
        public string ReasonChange { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
        public string TotalTime { get; set; }
    }
}

        