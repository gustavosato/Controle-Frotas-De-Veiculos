namespace ControleVeiculos.Domain.Command.TestCases
{
    public class MaintenanceTestCaseCommand
    {
        public int TestCaseID { get; set; }
        public string StatusID { get; set; }
        public string TestCase { get; set; }
        public string Description { get; set; }
        public string Precondition { get; set; }
        public string ExpectedResult { get; set; }
        public string FeatureID { get; set; }
        public string TestScenarioID { get; set; }
        public string ExecutionOrder { get; set; }
        public string FlowTestID { get; set; }
        public string StartExecution { get; set; }
        public string EndExecution { get; set; }
        public string TimeExecution { get; set; }
        public string Release { get; set; }
        public string Cycle { get; set; }
        public string TestTypeID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}
