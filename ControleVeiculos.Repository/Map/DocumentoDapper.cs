using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Documento")]
    public class DocumentoDapper
    {
        [ExplicitKey]
        public int documentoID { get; set; }
        public string seguroID { get; set; }
        public string numeroCnh { get; set; }
        public string clienteID { get; set; }
         
    }
}
