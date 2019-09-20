using ControleVeiculos.Domain.Command.Resumes;
using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Command.VacanciesResumes;
using ControleVeiculos.Domain.Entities.Resumes;
using ControleVeiculos.Domain.Entities.Vacancies;
using ControleVeiculos.Domain.Entities.VacanciesResumes;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
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
