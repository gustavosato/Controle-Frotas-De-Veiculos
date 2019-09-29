using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Manutencao")]
    public class ManutencaoDapper
    {
        [ExplicitKey]
        public int manutencaoID { get; set; }
        public string responsavel { get; set; }
        public string dataManutencao { get; set; }
        public string descricao { get; set; }
        public string veiculoID  { get; set; }
        
    }
}
