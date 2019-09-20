using ControleVeiculos.Domain.Command.Resumes;
using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Command.VacanciesResumes;
using ControleVeiculos.Domain.Entities.Resumes;
using ControleVeiculos.Domain.Entities.Vacancies;
using ControleVeiculos.Domain.Entities.VacanciesResumes;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IVacancieResumeRepository
    {
        void Add(VacancieResume VacancieResume);
        void Delete(int vacancieID, int resumeID);
        List<Resume> GetAllAssociateVacancieByResumeID(FilterResumeCommand command);
        List<Resume> GetAllNoAssociateVacancieByResumeID(FilterResumeCommand command);
        List<Resume> GetAllAssociateVacancieByResumeID(string vacancieID, string resumeID);
    }
}
