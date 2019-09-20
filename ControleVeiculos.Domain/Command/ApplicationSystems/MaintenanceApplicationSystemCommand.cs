namespace ControleVeiculos.Domain.Command.ApplicationSystems
{
    public class MaintenanceApplicationSystemCommand
    {
        public int ApplicationSystemID { get; set; }
        public string ApplicationSystemName { get; set; }
        public string Description { get; set; }
        public string ApplicationTypeID { get; set; }
        public string CustomerID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
