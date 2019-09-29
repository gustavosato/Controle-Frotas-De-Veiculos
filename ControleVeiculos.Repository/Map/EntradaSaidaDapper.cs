using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("EntradaSaida")]
    public class EntradaSaidaDapper
    {
        [ExplicitKey]
        public int entradaSaidaID { get; set; }
        public string emprestimoID { get; set; }
        public string servicosID { get; set; }
        public string veiculoID { get; set; }
         
    }
}
