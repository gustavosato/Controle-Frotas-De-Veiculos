namespace Lean.Test.Cloud.Domain.Command.TestScenarioFeatures
{
    public class MaintenanceTestScenarioFeatureCommand
    {
        public int TestScenarioFeatureID { get; set; }
        public int TestScenarioID { get; set; }
        public int FeatureID { get; set; }
        public string ExecutionOrder { get; set; }
        public bool IsLoop { get; set; }
        public string StatusID { get; set; }
        public string ToolsTestID { get; set; }
        public string TestID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
        public string FeatureName { get; set; }
    }
}