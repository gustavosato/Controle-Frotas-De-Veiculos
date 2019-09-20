namespace ControleVeiculos.Domain.Command.Resumes
{
    public class FilterResumeCommand
    {
        public string Summary { get; set; }
        public string TimeExperience { get; set; }
        public string FunctionID { get; set; }
        public string FunctionLevelID { get; set; }
        public string StatusRhID { get; set; }
        public string StatusManagerID { get; set; }
        public string StatusClientID { get; set; }
        public string ContractTypeID { get; set; }
        public string VacancieID { get; set; }
        public string ResumeID { get; set; }

    }
}
