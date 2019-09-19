namespace Lean.Test.Cloud.Domain.Command.Elements
{
    public class MaintenanceElementCommand
    {
        public int ElementID { get; set; }
        public string Element { get; set; }
        public string ActionID { get; set; }
        public string DefaultValue { get; set; }
        public string ValuePerKilometer { get; set; }
        public string Domains { get; set; }
        public string AutomationID { get; set; }
        public string TypeIdentificationID { get; set; }
        public string FeatureID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
