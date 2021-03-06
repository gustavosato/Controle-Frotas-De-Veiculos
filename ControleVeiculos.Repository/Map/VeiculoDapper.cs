using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Veiculos")]
    public class VeiculoDapper
    {
        [ExplicitKey]
        public int veiculoID { get; set; }
        public string modelo { get; set; }
        public string cor { get; set; }
        public string placa { get; set; }
        public string status { get; set; }
        public string ano { get; set; }
        public string manutencaoID { get; set; }
        public string abastecimentoID { get; set; }
        public string numeroChassi { get; set; }
        public string motor { get; set; }
         
    }
}
