using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("PositionsSalaries")]
    public class PositionsSalarieDapper
    {
        [ExplicitKey]
        public int positionsSalarieID { get; set; }
        public string functionID { get; set; }
        public string levelID { get; set; }
        public string classificationID { get; set; }
        public string amountPJ { get; set; }
        public string amountCLT { get; set; }
        public string amountCLTFLEX { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
        public string startingDate { get; set; }
        public string closingDate { get; set; }


    }
}
