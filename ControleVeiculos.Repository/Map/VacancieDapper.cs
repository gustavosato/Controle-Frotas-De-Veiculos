using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Vacancies")]
    public class VacancieDapper
    {
        [ExplicitKey]
        public int vacancieID { get; set; }
        public string summary { get; set; }
        public string vacanciesTypeID { get; set; }
        public string description { get; set; }
        public string customerID { get; set; }
        public string internalApplicantID { get; set; }
        public string externalApplicantID { get; set; }
        public string assignToID { get; set; }
        public string contractTypeID { get; set; }
        public string validityID { get; set; }
        public string statusID { get; set; }
        public string openingDate { get; set; }
        public string closingDate { get; set; }
        public string expectedStartDate { get; set; }
        public string maximumValue { get; set; }
        public string closedValue { get; set; }
        public string workPlace { get; set; }
        public string resumeSelectedID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
