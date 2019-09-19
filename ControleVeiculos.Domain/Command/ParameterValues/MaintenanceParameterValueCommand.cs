namespace Lean.Test.Cloud.Domain.Command.ParameterValues
{
    public class MaintenanceParameterValueCommand
    {
        public int ParameterValueID { get; set; }
        public string ParameterValue { get; set; }
        public string SystemFeatureID { get; set; }
        public string ParameterID { get; set; }
        public string ParentID { get; set; }
        public string IsSystem { get; set; }
        public string Description { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}
