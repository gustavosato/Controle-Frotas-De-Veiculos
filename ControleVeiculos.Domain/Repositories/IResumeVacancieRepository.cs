using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Entities.Vacancies;
using ControleVeiculos.Domain.Entities.ResumeVacancies;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IResumeVacancieRepository
    {
        void Add(ResumeVacancie ResumeVacancie);
        void Delete(int resumeID, int vacancieID);
        List<Vacancie> GetAllAssociateResumeByVacancieID(FilterVacancieCommand command);
        List<Vacancie> GetAllNoAssociateResumeByVacancieID(FilterVacancieCommand command);
    }
}
