namespace ControleVeiculos.Domain.Command.Reservas
{
    public class FilterReservaCommand
    {
        public string DataReserva { get; set; }
        public string Destino { get; set; }
        public string FuncionarioID { get; set; }
        public string VeiculoID { get; set; }
    }
}