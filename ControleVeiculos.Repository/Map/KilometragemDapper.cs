using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Kilometragem")]
    public class KilometragemDapper
    {
        [ExplicitKey]
        public int kilometragemID { get; set; }
        public string kilometragemTotal { get; set; }
        public string veiculoID { get; set; }
         
    }
}
