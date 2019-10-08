namespace ControleVeiculos.Domain.Command.Abastecimentos
{
    public class MaintenanceAbastecimentoCommand
    {
        public int AbastecimentoID { get; set; }
        public string TipoCombustivel { get; set; }
        public string Responsavel { get; set; }
        public string Data { get; set; }
        public string KmAtual { get; set; }
        public string VeiculoID { get; set; }
        
    }
}