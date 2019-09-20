namespace ControleVeiculos.Domain.Entities.Demands
{
    public class Demand
    {
        public int demandID { get; set; }
        public string demandName { get; set; }
        public string typeID { get; set; }
        public string statusID { get; set; }
        public string scope { get; set; }
        public string serviceID { get; set; }
        public string externalCode { get; set; }
        public string demandCode { get; set; }
        public string responsibleID { get; set; }
        public string assignToTargetID { get; set; }
        public string planningStartDate { get; set; }
        public string planningEndDate { get; set ; }
        public string managementEffort { get; set; }
        public string planningEffort { get; set; }
        public string executionEffort { get; set; }
        public string description { get; set; }
        public string customerID { get; set; }
        public string oportunityID { get; set; }
        public bool isActive { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
        public string totalTime { get; set; }
    }
}