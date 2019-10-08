using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Cnh")]
    public class CnhDapper
    {
        [ExplicitKey]
        public int cnhID { get; set; }
        public int numeroCnh { get; set; }
        public string nome { get; set; }
        public string validade { get; set; }
        public string categoria { get; set; }
        public string funcionarioID { get; set; }
        
    }
}

