using Lean.Test.Cloud.Domain.Command.Vacancies;
using Lean.Test.Cloud.Domain.Entities.Vacancies;
using Lean.Test.Cloud.Domain.Entities.ResumeVacancies;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IResumeVacancieRepository
    {
        void Add(ResumeVacancie ResumeVacancie);
        void Delete(int resumeID, int vacancieID);
        List<Vacancie> GetAllAssociateResumeByVacancieID(FilterVacancieCommand command);
        List<Vacancie> GetAllNoAssociateResumeByVacancieID(FilterVacancieCommand command);
    }
}
