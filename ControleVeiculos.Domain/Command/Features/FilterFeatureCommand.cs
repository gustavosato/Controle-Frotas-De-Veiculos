namespace ControleVeiculos.Domain.Command.Features
{
    public class FilterFeatureCommand
    {
      
        public string FeatureName { get; set; }
        public string ApplicationSystemID { get; set; }
        public int TestScenarioID { get; set; }
        public string CustomerID { get; set; }
        public string ExecutionOrder { get; set; }
        public bool IsLoop { get; set; }
        public string ToolsTestID { get; set; }

    }
}