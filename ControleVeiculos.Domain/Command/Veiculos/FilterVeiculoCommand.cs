namespace ControleVeiculos.Domain.Command.Veiculos
{
    public class FilterVeiculoCommand
    {
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Status { get; set; }
        public string Motor { get; set; }
        public string NumeroChassi { get; set; }
        public string Cor { get; set; }
        public string Ano { get; set; }
        
    }
}

