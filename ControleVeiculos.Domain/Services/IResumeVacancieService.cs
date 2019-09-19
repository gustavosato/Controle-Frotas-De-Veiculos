using Lean.Test.Cloud.Domain.Command.Resumes;
using Lean.Test.Cloud.Domain.Command.ResumeVacancies;
using Lean.Test.Cloud.Domain.Command.Vacancies;
using Lean.Test.Cloud.Domain.Entities.Resumes;
using Lean.Test.Cloud.Domain.Entities.Vacancies;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IResumeVacancieService : IDisposable
    {
        void Add(MaintenanceResumeVacancieCommand command);
        void Delete(int resumeID, int vacancieID);
        IPagedList<Vacancie> GetAllAssociateResumeByVacancieID(FilterVacancieCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Vacancie> GetAllNoAssociateResumeByVacancieID(FilterVacancieCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
