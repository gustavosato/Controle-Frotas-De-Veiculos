namespace ControleVeiculos.Domain.Command.Kilometragens
{
    public class MaintenanceKilometragemCommand
    {
        public int KilometragemID { get; set; }
        public string VeiculoID { get; set; }
        public string KilometragemTotal { get; set; }
        
    }
}
