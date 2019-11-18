using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Multas")]
    public class MultaDapper
    {
        [ExplicitKey]
        public int multaID { get; set; }
        public string veiculoID { get; set; }
        public string funcionarioID { get; set; }
        
    }
}
