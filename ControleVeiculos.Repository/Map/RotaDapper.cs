using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Rotas")]
    public class RotaDapper
    {
        [ExplicitKey]
        public int rotaID { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string distancia { get; set; }
        public bool pedagio { get; set; }
        public string dataIda { get; set; }
        public string dataVolta { get; set; }
         
    }
}
