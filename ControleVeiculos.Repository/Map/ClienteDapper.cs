using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Cliente")]
    public class ClienteDapper
    {
        [ExplicitKey]
        public int clienteID { get; set; }
        public string nomeCliente { get; set; }
        public string ramo { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public string status { get; set; }
         
    }
}
