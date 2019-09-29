using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Status")]
    public class StatusDapper
    {
        [ExplicitKey]
        public int statusID { get; set; }
        public string disponibilidade { get; set; }
        public bool emUso { get; set; }
        public bool emManutencao { get; set; }
        public bool reservado { get; set; }
        public string veiculoID { get; set; }
        
    }
}

