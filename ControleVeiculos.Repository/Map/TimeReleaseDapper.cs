using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("TimeReleases")]
    public class TimeReleaseDapper
    {
        [ExplicitKey]
        public int timeReleaseID { get; set; }
        public string registerDate { get; set; }
        public string startWork { get; set; }
        public string endWork { get; set; }
        public string demandID { get; set; }
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

    }
}

///test
