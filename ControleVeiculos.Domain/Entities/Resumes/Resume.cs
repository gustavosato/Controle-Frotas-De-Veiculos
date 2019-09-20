namespace ControleVeiculos.Domain.Entities.Resumes
{
    public class Resume
    {
        public int resumeID { get; set; }
        public string summary { get; set; }
        public string functionID { get; set; }
        public string description { get; set; }
        public string genderID { get; set; }
        public string age { get; set; }
        public string timeExperience { get; set; }
        public string functionLevelID { get; set; }
        public string statusRhID { get; set; }
        public string approvedDateRh { get; set; }
        public string statusManagerID { get; set; }
        public string approvedDateManager { get; set; }
        public string statusClientID { get; set; }
        public string approvedDateClient { get; set; }
        public string expectedSalary { get; set; }
        public string contractTypeID { get; set; }
        public bool isEmployee { get; set; }
        public bool willingToTravel { get; set; }
        public string maritalStatusID { get; set; }
        public bool haveChildren { get; set; }
        public bool isSmoker { get; set; }
        public string availabilityToStart { get; set; }
        public string observation { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
        public string resultRh { get; set; }
        public string resultManager { get; set; }
        public string resultClient { get; set; }
    }
}
