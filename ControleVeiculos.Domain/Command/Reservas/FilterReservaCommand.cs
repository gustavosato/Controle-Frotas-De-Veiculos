namespace ControleVeiculos.Domain.Command.Reservas
{
    public class FilterReservaCommand
    {
        public string DataReserva { get; set; }
        public string Destino { get; set; }
        public string Funcionario { get; set; }
        public string Veiculo { get; set; }
    }
}