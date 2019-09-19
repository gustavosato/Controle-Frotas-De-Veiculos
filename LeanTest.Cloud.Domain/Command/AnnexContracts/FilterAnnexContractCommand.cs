namespace Lean.Test.Cloud.Domain.Command.AnnexContracts

{
    public class FilterAnnexContractCommand
    {
        public string ExtencionPeriodID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ContractID { get; set; }
    }
}