using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("MovimentEmployees")]
    public class MovimentEmployeeDapper
    {
        [ExplicitKey]
        public int movimentEmployeeID  { get; set;}
        public string employeeID { get; set;}
        public string startDate { get; set;}
        public string endDate { get; set;}
        public string statusID { get; set; }
        public string movimentEmployeeTypeID { get; set; }
        public string approvedDate { get; set; }
        public string approvedByID { get; set; }
        public string description { get; set; }
        public string createdByID {get; set;}
        public string creationDate{get; set;}
        public string modifiedByID {get; set;}
        public string lastModifiedDate {get; set;}
    }
}
