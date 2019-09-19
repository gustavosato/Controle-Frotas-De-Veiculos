using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Skills")]
    public class SkillDapper
    {
        [ExplicitKey]
        public int skillID { get; set; }
        public string summary { get; set; }
        public string skillTypeID { get; set; }
        public string description { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
