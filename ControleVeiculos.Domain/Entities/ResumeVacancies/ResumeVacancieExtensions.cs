using ControleVeiculos.Domain.Command.ResumeVacancies;

namespace ControleVeiculos.Domain.Entities.ResumeVacancies
{
    public static class ResumeVacancieExtensions
    {
        public static Result<ResumeVacancie> GetResumeVacancie(this ResumeVacancie resumeVacancie)
        {
            return Result.Ok(0, "", resumeVacancie);
        }

        public static ResumeVacancie Map(this ResumeVacancie resumeVacancie, MaintenanceResumeVacancieCommand command)
        {
            resumeVacancie.resumeID = command.ResumeID;
            resumeVacancie.vacancieID = command.VacancieID;

            return resumeVacancie;
        }
    }
}
