namespace ControleVeiculos.Domain.Entities.TestLogs
{
    public class TestLog
    {
        public int logID { get; set; }
        public string testID { get; set; }
        public string statusID { get; set; }
        public string stepName { get; set; }
        public string expectedResult { get; set; }
        public string actualResult { get; set; }
        public string pathEvidence { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
