namespace ControleVeiculos.Domain.Command.Contracts

{
    public class FilterContractCommand
    {
        public string OportunityID { get; set; }
        public string ContractTypeID { get; set; }
        public string ContractorCustomerID { get; set; }
        public string ContractingCustomerID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PeriodValidityID { get; set; }
        public string ExtencionID { get; set; }
        public string ExtencionPeriodID { get; set; }
        public string ResetModalityID { get; set; }
    }
}
