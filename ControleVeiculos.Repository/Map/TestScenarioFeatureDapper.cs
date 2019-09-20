using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("TestScenarioFeatures")]
    public class TestScenarioFeatureDapper
    {
        [ExplicitKey]
        public int testScenarioFeatureID { get; set; }
        public int testScenarioID { get; set; }
        public int featureID { get; set; }
        public string executionOrder { get; set; }
        public bool isLoop { get; set; }
        public string statusID { get; set; }
        public string toolsTestID { get; set; }
        public string testID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
