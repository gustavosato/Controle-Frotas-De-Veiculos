using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Historicals")]
    public class HistoricalDapper
    {
        [ExplicitKey]
        public int historicalID { get; set; }
        public string systemFeatureID { get; set; }
        public string recordID { get; set; }
        public string actionID { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
        public string fieldName { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}

