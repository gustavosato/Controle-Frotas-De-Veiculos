using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.VacanciesResumes;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.VacanciesResumes;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;
using ControleVeiculos.Domain.Entities.Vacancies;
using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Command.Resumes;
using ControleVeiculos.Domain.Entities.Resumes;

namespace ControleVeiculos.ApplicationService
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

