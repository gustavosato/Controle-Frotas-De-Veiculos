namespace Lean.Test.Cloud.Domain.Entities.TestCases
{
    public class TestCase
    {
        public int testCaseID { get; set; }
        public string statusID { get; set; }
        public string testCase { get; set; }
        public string description { get; set; }
        public string precondition { get; set; }
        public string expectedResult { get; set; }
        public string featureID { get; set; }
        public string testScenarioID { get; set; }
        public string executionOrder { get; set; }
        public string flowTestID { get; set; }
        public string startExecution { get; set; }
        public string endExecution { get; set; }
        public string timeExecution { get; set; }
        public string release { get; set; }
        public string cycle { get; set; }
        public string testTypeID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
