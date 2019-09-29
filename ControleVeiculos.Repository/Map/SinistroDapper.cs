using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Sinistro")]
    public class SinistroDapper
    {
        [ExplicitKey]
        public int sinistroID { get; set; }
        public string apolice { get; set; }
        public string franquia { get; set; }
        public string tipoSinistro { get; set; }
         
    }
}
