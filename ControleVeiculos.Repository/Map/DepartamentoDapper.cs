using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Departamento")]
    public class DepartamentoDapper
    {
        [ExplicitKey]
        public int departamentoID { get; set; }
        public string nomeDepartamento { get; set; }
        public string descricao { get; set; }
        public string funcionarioID { get; set; }
         
    }
}
