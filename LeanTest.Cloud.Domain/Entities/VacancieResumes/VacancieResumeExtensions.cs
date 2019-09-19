using Lean.Test.Cloud.Domain.Command.VacanciesResumes;

namespace Lean.Test.Cloud.Domain.Entities.VacanciesResumes
{
    public static class VacancieResumeExtensions
    {
        public static Result<VacancieResume> GetVacancieResume(this VacancieResume vacancieResume)
        {
            return Result.Ok(0, "", vacancieResume);
        }

        public static VacancieResume Map(this VacancieResume vacancieResume, MaintenanceVacancieResumeCommand command)
        {
            vacancieResume.vacancieID = command.VacancieID;
            vacancieResume.resumeID = command.ResumeID;

            return vacancieResume;
        }
    }
}
