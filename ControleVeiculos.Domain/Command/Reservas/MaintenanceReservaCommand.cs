namespace ControleVeiculos.Domain.Command.Reservas
{
    public class MaintenanceReservaCommand
    {
        public int ReservaID { get; set; }
        public string DataReserva { get; set; }
        public string Finalidade { get; set; }
        public string Destino { get; set; }
        public string FuncionarioID { get; set; }
        public string NumeroCnh { get; set; }
        public string VeiculoID { get; set; }
        
    }
}

  
       