using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Abastecimento")]
    public class AbastecimentoDapper
    {
        [ExplicitKey]
        public int abastecimentoID { get; set; }
        public string tipoCombustivel { get; set; }
        public string responsavel { get; set; }
        public string data { get; set; }
        public string kmAtual { get; set; }
        public string veiculoID { get; set; }
        
    }
}
