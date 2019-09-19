namespace Lean.Test.Cloud.Domain.Command.ContractAdditives

{
    public class FilterContractAdditiveCommand
    {
        public string ContractID { get; set; }
        public string PeriodValidityID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ExtencionID { get; set; }
        public string ExtencionPeriodID { get; set; }
        public string ResetModalityID { get; set; }
    }
}
