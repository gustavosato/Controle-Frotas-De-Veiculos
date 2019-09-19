namespace Lean.Test.Cloud.Domain.Command.TestLogs
{
    public class MaintenanceTestLogCommand
    {
        public int LogID { get; set; }
        public string TestID { get; set; }
        public string StatusID { get; set; }
        public string StepName { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public string PathEvidence { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}
