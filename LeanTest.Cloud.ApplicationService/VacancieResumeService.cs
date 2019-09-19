using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.VacanciesResumes;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.VacanciesResumes;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;
using Lean.Test.Cloud.Domain.Entities.Vacancies;
using Lean.Test.Cloud.Domain.Command.Vacancies;
using Lean.Test.Cloud.Domain.Command.Resumes;
using Lean.Test.Cloud.Domain.Entities.Resumes;

namespace Lean.Test.Cloud.ApplicationService
{
    public class VacancieResumeService : BaseAppService, IVacancieResumeService
    {
        private readonly IVacancieResumeRepository _vacancieResumeRepository;

        public VacancieResumeService(IVacancieResumeRepository vacancieResumeRepository)
        {
            _vacancieResumeRepository = vacancieResumeRepository;
        }

        public void Add(MaintenanceVacancieResumeCommand command)
        {
            VacancieResume vacancieResume = new VacancieResume();

            vacancieResume = vacancieResume.Map(command);

            _vacancieResumeRepository.Add(vacancieResume);
        }

        public void Delete(int vacancieID, int resumeID)
        {
            _vacancieResumeRepository.Delete(vacancieID, resumeID);
        }

       public IPagedList<Resume> GetAllAssociateVacancieByResumeID(FilterResumeCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var vacancies = _vacancieResumeRepository.GetAllAssociateVacancieByResumeID(command);

            return new PagedList<Resume>(vacancies, pageIndex, pageSize);
        }

        public IPagedList<Resume> GetAllNoAssociateVacancieByResumeID(FilterResumeCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var vacancies = _vacancieResumeRepository.GetAllNoAssociateVacancieByResumeID(command);

            return new PagedList<Resume>(vacancies, pageIndex, pageSize);
        }

        public IList<Resume> GetAllAssociateVacancieByResumeID(string vacancieID, string resumeID)
        {
            var vacancies = _vacancieResumeRepository.GetAllAssociateVacancieByResumeID(vacancieID, resumeID);

            return new List<Resume>(vacancies);
        }

    }
}

