namespace ControleVeiculos.Domain.Command.SystemParameters
{
    public class MaintenanceSystemParameterCommand
    {
        public int ParameterID { get; set; }
        public string ParamterName { get; set; }
        public string ParamterValue { get; set; }
        public string ParamterDefaultValue { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}
