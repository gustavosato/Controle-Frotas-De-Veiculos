namespace Lean.Test.Cloud.Domain.Command.PipelineEvents
{
    public class MaintenancePipelineEventCommand
    {
        public int SaleEventID { get; set; }
        public string RegisterDate { get; set; }
        public string TypeID { get; set; }
        public string NextStepID { get; set; }
        public string TargetDate { get; set; }
        public string Description { get; set; }
        public string OportunityID { get; set; }
        public string CreatedByID { get; set; }        
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
