namespace Lean.Test.Cloud.Domain.Command.TestScenarios
{
    public class FilterTestScenarioCommand
    {
        public string TestScenario { get; set; }
        public string StatusID { get; set; }
        public string TestTypeID   { get; set; }
        public string ExecutionTypeID  { get; set; }
    }
}
