namespace Lean.Test.Cloud.Domain.Entities.Tasks
{
    public class Task
    {
        public int taskID { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public string assignToID { get; set; }
        public string demandID { get; set; }
        public string customerID { get; set; }
        public string statusID { get; set; }
        public string targetDate { get; set; }
        public string closingDate { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}