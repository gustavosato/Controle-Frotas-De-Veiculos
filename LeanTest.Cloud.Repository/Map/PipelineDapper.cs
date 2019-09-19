using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Pipelines")]
    public class PipelineDapper
    {
        [ExplicitKey]
        public int oportunityID { get; set;}
        public string customerID { get; set;}
        public string summary { get; set; }
        public string oportunityCode { get; set;}
        public string description { get; set;}
        public string priorityID { get; set;}
        public string faseID { get; set; }
        public string ownerID { get; set; }
        public string saleManagerID { get; set; }
        public string preSalesID { get; set; }
        public string operationManagerID { get; set; }
        public string typeID { get; set; }
        public string costCenterID { get; set; }
        public string offerID { get; set; }
        public string sponsor { get; set; }
        public string powerSponsor { get; set; }
        public string expectedValue { get; set; }
        public string targetDate { get; set; }
        public string statusID { get; set; }
        public string probability { get; set; }
        public string billed { get; set; }
        public string comments { get; set; }
        public string closingDate { get; set; }
        public string frequencyOfInteractionID { get; set; }
        public string approvedByID { get; set; }
        public string approvedDate { get; set; }
        public string quarter1 { get; set; }
        public string quarter2 { get; set; }
        public string quarter3 { get; set; }
        public string quarter4 { get; set; }
        public string createdByID {get; set;}
        public string creationDate {get; set;}
        public string modifiedByID {get; set;}
        public string lastModifiedDate {get; set;}
    }
}
