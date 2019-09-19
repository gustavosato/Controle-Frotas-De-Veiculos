namespace Lean.Test.Cloud.Domain.Command.Features
{
    public class MaintenanceFeatureCommand
    {
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }
        public string StatusID { get; set; }
        public string Description { get; set; }
        public string CustomerID { get; set; }
        public string ApplicationSystemID { get; set; }
        public string DeveloperID { get; set; }
        public string FeatureTypeID { get; set; }
        public string MetaScript { get; set; }
        public string AutomationScript { get; set; }
        public string TestPoints { get; set; }
        public string TargetDate { get; set; }
        public string TimeEffort { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}

   