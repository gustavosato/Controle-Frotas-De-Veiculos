namespace ControleVeiculos.Domain.Command.Multas
{
    public class MaintenanceMultaCommand
    {
        public int MultaID { get; set; }
        public string VeiculoID { get; set; }
        public string ClienteID { get; set; }
        public string CnhID { get; set; }
        
    }
}
