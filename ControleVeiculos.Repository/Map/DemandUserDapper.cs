using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("DemandsUsers")]
    public class DemandUserDapper
    {
        [ExplicitKey]
        public int userID {get; set;}
        public int demandID { get; set;}
    }
}
