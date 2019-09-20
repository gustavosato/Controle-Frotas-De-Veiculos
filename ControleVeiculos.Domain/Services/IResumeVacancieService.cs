using ControleVeiculos.Domain.Command.Resumes;
using ControleVeiculos.Domain.Command.ResumeVacancies;
using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Entities.Resumes;
using ControleVeiculos.Domain.Entities.Vacancies;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IResumeVacancieService : IDisposable
    {
        void Add(MaintenanceResumeVacancieCommand command);
        void Delete(int resumeID, int vacancieID);
        IPagedList<Vacancie> GetAllAssociateResumeByVacancieID(FilterVacancieCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Vacancie> GetAllNoAssociateResumeByVacancieID(FilterVacancieCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
