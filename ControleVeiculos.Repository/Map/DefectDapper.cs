using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Defects")]
    public class DefectDapper
    {
        [ExplicitKey]
        public int defectID { get; set;}
        public string summary { get; set;}
        public string description { get; set;}
        public string statusID {get; set;}
        public string severityID { get; set;}
        public string priorityID { get; set; }
        public string assingToID { get; set; }
        public string typeID { get; set; }
        public string resolutionID { get; set; }
        public string resolution { get; set; }
        public string resolutionDate { get; set; }
        public string applicationSystemID { get; set; }
        public string featureID { get; set; }
        public string createdByID {get; set;}
        public string creationDate {get; set;}
        public string modifiedByID {get; set;}
        public string lastModifiedDate {get; set;}
    }
}
