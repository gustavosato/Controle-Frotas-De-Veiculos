using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Entities.Vacancies;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IVacancieService : IDisposable
    {
        void Add(MaintenanceVacancieCommand command);
        void Update(MaintenanceVacancieCommand command);
        Result<Vacancie> GetByID(int vacancieID);
        IPagedList<Vacancie> GetAll(FilterVacancieCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Vacancie> GetAll(int vacancieID);
        void Delete(int vacancieID);
        string GetVacancieNameByID(int contatctID);
    }
}
