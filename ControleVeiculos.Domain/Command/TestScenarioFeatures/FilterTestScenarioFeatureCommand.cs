namespace ControleVeiculos.Domain.Command.TestScenarioFeatures
{
    public class FilterTestScenarioFeatureCommand
    {
        public int TestScenarioID { get; set; }
        public int FeatureID { get; set; }
        public string StatusID { get; set; }
        public string ToolsTestID { get; set; }
        public string FeatureName { get; set; }
        public string CustomerID { get; set; }
        public string ExecutionOrder { get; set; }
        public bool IsLoop { get; set; }
    }
}
