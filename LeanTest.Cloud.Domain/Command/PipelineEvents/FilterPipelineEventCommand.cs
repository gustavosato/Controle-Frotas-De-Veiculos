namespace Lean.Test.Cloud.Domain.Command.PipelineEvents
{
    public class FilterPipelineEventCommand
    {        
        public string RegisterDate { get; set; }
        public string TypeID { get; set; }
        public string NextStepID { get; set; }
        public string OportunityID { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }
}
