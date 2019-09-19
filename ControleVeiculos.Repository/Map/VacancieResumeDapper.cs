using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("VacanciesResumes")]
    public class VacancieResumeDapper
    {
        [ExplicitKey]
        public int vacancieID { get; set; }
        public int resumeID { get; set; }
    }
}
