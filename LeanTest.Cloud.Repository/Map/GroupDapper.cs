using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Groups")]
    public class GroupDapper
    {
        [ExplicitKey]
        public int groupID { get; set; }
        public string groupName { get; set; }
        public bool isSystem { get; set; }
        public string description { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}

