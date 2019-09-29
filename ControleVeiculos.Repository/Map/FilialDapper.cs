using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Filial")]
    public class FilialDapper
    {
        [ExplicitKey]
        public int filialID { get; set; }
        public string nomeFilial { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
         
    }
}
