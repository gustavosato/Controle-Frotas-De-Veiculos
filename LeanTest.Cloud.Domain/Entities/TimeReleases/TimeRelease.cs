namespace Lean.Test.Cloud.Domain.Entities.TimeReleases
{
    public class TimeRelease
    {
        public int timeReleaseID { get; set; }
        public string registerDate { get; set; }
        public string startWork { get; set; }
        public string endWork { get; set; }
        public string demandID { get; set; }
        public string customerID { get; set; }
        public bool isApproved { get; set; }
        public string activityID { get; set; }
        public string approvedByID { get; set; }
        public string approvedDate { get; set; }
        public string description { get; set; }
        public string reasonChange { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
        public string totalTime { get; set; }
    }
}
