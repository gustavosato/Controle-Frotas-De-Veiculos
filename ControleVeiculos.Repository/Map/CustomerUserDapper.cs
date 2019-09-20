using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("CustomersUsers")]
    public class CustomerUserDapper
    {
        [ExplicitKey]
        public int customerID { get; set; }
        public int userID { get; set; }

    }
}
