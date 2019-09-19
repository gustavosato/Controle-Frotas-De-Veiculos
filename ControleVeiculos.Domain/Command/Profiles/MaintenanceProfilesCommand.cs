namespace Lean.Test.Cloud.Domain.Command.Profiles
{
    public class MaintenanceProfileCommand
    {
        public int ProfileID { get; set; }
        public string GroupID { get; set; }
        public string SystemFeatureID { get; set; }
        public bool AllowView { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowUpdate { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowChangeStatus { get; set; }
        public bool AllowAddRemove { get; set; }
        public bool AllowExportExcel { get; set; }
        public bool AllowReportView { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}