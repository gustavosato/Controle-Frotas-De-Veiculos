using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.ResumeVacancies;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.ResumeVacancies;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;
using ControleVeiculos.Domain.Entities.Vacancies;
using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Command.Resumes;
using ControleVeiculos.Domain.Entities.Resumes;

namespace ControleVeiculos.ApplicationService
{
    public class ResumeVacancieService : BaseAppService, IResumeVacancieService
    {
        private readonly IResumeVacancieRepository _resumeVacancieRepository;

        public ResumeVacancieService(IResumeVacancieRepository resumeVacancieRepository)
        {
            _resumeVacancieRepository = resumeVacancieRepository;
        }

        public void Add(MaintenanceResumeVacancieCommand command)
        {
            ResumeVacancie resumeVacancie = new ResumeVacancie();

            resumeVacancie = resumeVacancie.Map(command);

            _resumeVacancieRepository.Add(resumeVacancie);
        }

        public void Delete(int resumeID, int vacancieID)
        {
            _resumeVacancieRepository.Delete(resumeID, vacancieID);
        }

       public IPagedList<Vacancie> GetAllAssociateResumeByVacancieID(FilterVacancieCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var resumes = _resumeVacancieRepository.GetAllAssociateResumeByVacancieID(command);

            return new PagedList<Vacancie>(resumes, pageIndex, pageSize);
        }

        public IPagedList<Vacancie> GetAllNoAssociateResumeByVacancieID(FilterVacancieCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var resumes = _resumeVacancieRepository.GetAllNoAssociateResumeByVacancieID(command);

            return new PagedList<Vacancie>(resumes, pageIndex, pageSize);
        }

        //public IList<Resume> GetAllAssociateVacancieByResumeID(string vacancieID, string resumeID)
        //{
        //    var vacancies = _vacancieResumeRepository.GetAllAssociateVacancieByResumeID(vacancieID, resumeID);

        //    return new List<Resume>(vacancies);
        //}

    }
}

