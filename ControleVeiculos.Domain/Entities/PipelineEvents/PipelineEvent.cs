namespace Lean.Test.Cloud.Domain.Entities.PipelineEvents
{
    public class PipelineEvent
    {
        public int saleEventID { get; set; }
        public string registerDate { get; set; }
        public string typeID { get; set; }
        public string nextStepID { get; set; }
        public string targetDate { get; set; }
        public string description { get; set; }
        public string oportunityID { get; set; }        
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
