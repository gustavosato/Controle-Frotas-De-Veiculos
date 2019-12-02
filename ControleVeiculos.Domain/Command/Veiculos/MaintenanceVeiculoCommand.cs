namespace ControleVeiculos.Domain.Command.Veiculos
{
    public class MaintenanceVeiculoCommand
    {
        public int VeiculoID { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }
        public string Status { get; set; }
        public string Ano { get; set; }
        public string ManutencaoID { get; set; }
        public string AbastecimentoID { get; set; }
        public string NumeroChassi { get; set; }
        public string Motor { get; set; }
        
    }
}
