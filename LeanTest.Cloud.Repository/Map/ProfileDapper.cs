using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Profiles")]
    public class ProfileDapper
    {
        [ExplicitKey]
        public int profileID { get; set; }
        public string groupID { get; set; }
        public string systemFeatureID { get; set; }
        public bool allowView { get; set; }
        public bool allowAdd { get; set; }
        public bool allowUpdate { get; set; }
        public bool allowDelete { get; set; }
        public bool allowChangeStatus { get; set; }
        public bool allowAddRemove { get; set; }
        public bool allowExportExcel { get; set; }
        public bool allowReportView { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}

