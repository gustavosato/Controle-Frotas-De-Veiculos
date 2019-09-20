using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("ChangeRequests")]
    public class ChangeRequestDapper
    {
        [ExplicitKey]
        public int changeRequestID { get; set; }
        public string summary { get; set; }
        public string managementEffort { get; set; }
        public string planningEffort { get; set; }
        public string executionEffort { get; set; }
        public string statusID { get; set; }
        public string targetDate { get; set; }
        public string approvedDate { get; set; }
        public string approvedByID { get; set; }
        public string description      {get; set;}
        public string demandID         {get; set;}
        public string requestByID      {get; set;}
        public string createdByID       {get; set;}
        public string creationDate     {get; set;}
        public string modifiedByID     {get; set;}
        public string lastModifiedDate {get; set;}
    }
}
