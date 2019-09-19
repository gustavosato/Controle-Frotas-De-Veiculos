namespace Lean.Test.Cloud.Domain.Command.ContractAdditives
{
    public class MaintenanceContractAdditiveCommand
    {
        public int AdditiveID { get; set; }
        public string ContractID { get; set; }
        public string AdditiveObject { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PeriodValidityID { get; set; }
        public string ExtencionID { get; set; }
        public string ExtencionPeriodID { get; set; }
        public string ResetModalityID { get; set; }
        public string BillingCondition { get; set; }
        public string OportunityID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
