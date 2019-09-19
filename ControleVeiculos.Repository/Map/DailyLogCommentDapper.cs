using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("DailyLogComments")]
    public class DailyLogCommentDapper
    {
        [ExplicitKey]
        public int dailyLogsCommentID { get; set; }
        public string descrition { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
