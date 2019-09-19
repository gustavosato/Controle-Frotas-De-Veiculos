namespace Lean.Test.Cloud.Domain.Command.SystemFeatures
{
    public class MaintenanceSystemFeatureCommand
    {
        public int SystemFeatureID { get; set; }
        public string SystemFeatureName { get; set; }
        public string SystemFeatureTypeID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}


   