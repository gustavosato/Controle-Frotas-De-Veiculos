namespace ControleVeiculos.Domain.Command.AnnexContracts
{
    public class MaintenanceAnnexContractCommand
    {
        public int AnnexID { get; set; }
        public string ContractID { get; set; }
        public string OportunityID { get; set; }
        public string AnnexObject { get; set; }
        public string Summary { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ExtencionPeriodID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
