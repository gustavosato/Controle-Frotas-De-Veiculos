namespace Lean.Test.Cloud.Domain.Command.Resumes
{
    public class MaintenanceResumeCommand
    {
        public int ResumeID { get; set; }
        public string Summary { get; set; }
        public string FunctionID { get; set; }
        public string Description { get; set; }
        public string GenderID { get; set; }
        public string Age { get; set; }
        public string TimeExperience { get; set; }
        public string FunctionLevelID { get; set; }
        public string StatusRhID { get; set; }
        public string ApprovedDateRh { get; set; }
        public string StatusManagerID { get; set; }
        public string ApprovedDateManager { get; set; }
        public string StatusClientID { get; set; }
        public string ApprovedDateClient { get; set; }
        public string ExpectedSalary { get; set; }
        public string ContractTypeID { get; set; }
        public bool IsEmployee { get; set; }
        public bool WillingToTravel { get; set; }
        public string MaritalStatusID { get; set; }
        public bool HaveChildren { get; set; }
        public bool IsSmoker { get; set; }
        public string AvailabilityToStart { get; set; }
        public string Observation { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
        public string ResultRh { get; set; }
        public string ResultManager { get; set; }
        public string ResultClient { get; set; }
    }
}
