using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("DemandsUsers")]
    public class DemandUserDapper
    {
        [ExplicitKey]
        public int userID {get; set;}
        public int demandID { get; set;}
    }
}
