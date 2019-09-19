using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.ResumeVacancies;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.ResumeVacancies;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;
using Lean.Test.Cloud.Domain.Entities.Vacancies;
using Lean.Test.Cloud.Domain.Command.Vacancies;
using Lean.Test.Cloud.Domain.Command.Resumes;
using Lean.Test.Cloud.Domain.Entities.Resumes;

namespace Lean.Test.Cloud.ApplicationService
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

