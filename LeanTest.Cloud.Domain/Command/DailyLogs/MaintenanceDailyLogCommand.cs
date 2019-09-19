using System;
namespace Lean.Test.Cloud.Domain.Command.DailyLogs
{
    public class MaintenanceDailyLogCommand
    {
        public int DailyLogID { get; set; }
        public string Description { get; set; }
        public string Description1 { get; set; }
        public string DemandID { get; set; }
        public bool IsInternal { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}