using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("TestScenarios")]
    public class TestScenarioDapper
    {
        [ExplicitKey]
        public int testScenarioID { get; set; }
        public string testScenario { get; set; }
        public string description { get; set; }
        public string statusID { get; set; }
        public string executionOrder { get; set; }
        public string startExecution { get; set; }
        public string endExecution { get; set; }
        public string timeExecution { get; set; }
        public string testTypeID { get; set; }
        public string executionTypeID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
        public string testPackageID { get; set; }
    }
}

