using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Multa")]
    public class MultaDapper
    {
        [ExplicitKey]
        public int multaID { get; set; }
        public string veiculoID { get; set; }
        public string clienteID { get; set; }
        public string cnhID { get; set; }
        
    }
}
