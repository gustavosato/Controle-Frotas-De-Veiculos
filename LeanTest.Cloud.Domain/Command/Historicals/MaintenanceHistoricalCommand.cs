namespace Lean.Test.Cloud.Domain.Command.Historicals
{
    public class MaintenanceHistoricalCommand
    {
        public int HistoricalID { get; set; }
        public string SystemFeatureID { get; set; }
        public string RecordID { get; set; }
        public string ActionID { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string FieldName { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}

     