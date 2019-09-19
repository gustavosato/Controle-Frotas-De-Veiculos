using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("VacanciesResumes")]
    public class ResumeVacancieDapper
    {
        [ExplicitKey]
        public int resumeID { get; set; }
        public int vacancieID { get; set; }
    }
}
