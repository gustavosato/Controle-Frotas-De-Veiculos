namespace Lean.Test.Cloud.Domain.Entities.SystemFeatures
{
    public class SystemFeature
    {
        public int systemFeatureID { get; set; }
        public string systemFeatureName { get; set; }
        public string systemFeatureTypeID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}

