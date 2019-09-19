using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("TestPackages")]
    public class TestPackageDapper
    {
        [ExplicitKey]
        public int testPackageID { get; set; }
        public string packageName { get; set; }
        public string description { get; set; }
        public string demandID { get; set; }
        public string statusID { get; set; }
        public string release { get; set; }
        public string cycle { get; set; }
        public string emailsToSendReport { get; set; }
        public string tecnologyID { get; set; }
        public string browserID { get; set; }
        public string executionSpeedy { get; set; }
        public bool resetApp { get; set; }
        public bool highLight { get; set; }
        public bool highLightOut { get; set; }
        public string deviceID { get; set; }
        public string platformNameID { get; set; }
        public bool sendEmail { get; set; }
        public bool generateLog { get; set; }
        public bool logHtml { get; set; }
        public string methodologyID { get; set; }
        public string solutionPath { get; set; }
        public string leantestVariable { get; set; }
        public string saveEvidenceToExternalPath { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
