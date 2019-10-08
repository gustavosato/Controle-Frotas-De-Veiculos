namespace ControleVeiculos.Domain.Command.Status
{
    public class MaintenanceStatusCommand
    {
        public int StatusID { get; set; }
        public string Disponibilidade { get; set; }
        public bool EmUso { get; set; }
        public bool EmManutencao { get; set; }
        public bool Reservado { get; set; }
        public string VeiculoID { get; set; }

    }
}
