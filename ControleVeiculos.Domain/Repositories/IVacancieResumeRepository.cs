using Lean.Test.Cloud.Domain.Command.Resumes;
using Lean.Test.Cloud.Domain.Command.Vacancies;
using Lean.Test.Cloud.Domain.Command.VacanciesResumes;
using Lean.Test.Cloud.Domain.Entities.Resumes;
using Lean.Test.Cloud.Domain.Entities.Vacancies;
using Lean.Test.Cloud.Domain.Entities.VacanciesResumes;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IVacancieResumeRepository
    {
        void Add(VacancieResume VacancieResume);
        void Delete(int vacancieID, int resumeID);
        List<Resume> GetAllAssociateVacancieByResumeID(FilterResumeCommand command);
        List<Resume> GetAllNoAssociateVacancieByResumeID(FilterResumeCommand command);
        List<Resume> GetAllAssociateVacancieByResumeID(string vacancieID, string resumeID);
    }
}
