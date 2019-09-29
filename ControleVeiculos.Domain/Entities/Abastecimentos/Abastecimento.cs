namespace ControleVeiculos.Domain.Entities.Abastecimentos
{
    public class Abastecimento
    {
        public int abastecimentoID { get; set; }
        public string tipoCombustivel { get; set; }
        public string responsavel { get; set; }
        public string data { get; set; }
        public string kmAtual { get; set; }
        public string veiculoID { get; set; }
    }
}
