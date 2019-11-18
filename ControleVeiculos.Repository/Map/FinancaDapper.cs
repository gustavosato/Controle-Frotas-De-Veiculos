using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Financas")]
    public class FinancaDapper
    {
        [ExplicitKey]
        public int financaID { get; set; }
        public string valorCarro { get; set; }
        public string valorSeguro { get; set; }
        public string valorAgua { get; set; }
        public string valorLuz { get; set; }
        public string valorInternet { get; set; }
        public string valorManutencao { get; set; }
        public string salarios { get; set; }
        public string gastosExtras { get; set; }
         
    }
}
