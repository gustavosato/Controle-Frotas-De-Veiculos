namespace ControleVeiculos.Domain.Command.Parameters
{
    public class MaintenanceParameterCommand
    {
        public int ParameterID { get; set; }
        public string ParameterName { get; set; }
        public string SystemFeatureID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}
