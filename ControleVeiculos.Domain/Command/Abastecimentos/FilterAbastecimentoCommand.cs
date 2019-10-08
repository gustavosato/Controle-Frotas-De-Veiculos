namespace ControleVeiculos.Domain.Command.Abastecimentos
{
    public class FilterAbastecimentoCommand
    {
        public string TipoCombustivel { get; set; }
        public string Responsavel { get; set; }
        public string Data { get; set; }
        
    }
}
