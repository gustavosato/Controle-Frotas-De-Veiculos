using Lean.Test.Cloud.Domain.Command.Resumes;
using Lean.Test.Cloud.Domain.Command.Vacancies;
using Lean.Test.Cloud.Domain.Command.VacanciesResumes;
using Lean.Test.Cloud.Domain.Entities.Resumes;
using Lean.Test.Cloud.Domain.Entities.Vacancies;
using Lean.Test.Cloud.Domain.Entities.VacanciesResumes;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IVacancieResumeService : IDisposable
    {
        void Add(MaintenanceVacancieResumeCommand command);
        void Delete(int vacancieID, int resumeID);
        IPagedList<Resume> GetAllAssociateVacancieByResumeID(FilterResumeCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Resume> GetAllNoAssociateVacancieByResumeID(FilterResumeCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Resume> GetAllAssociateVacancieByResumeID(string vacancieID, string resumeID);

    }
}
