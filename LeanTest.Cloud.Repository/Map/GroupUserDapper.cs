using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("GroupsUsers")]
    public class GroupUserDapper
    {
        [ExplicitKey]
        public int groupID { get; set; }
        public int userID { get; set; }
    }
}
