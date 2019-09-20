using ControleVeiculos.Domain.Command.Vacancies;
using ControleVeiculos.Domain.Entities.Vacancies;
using System.Collections.Generic;


namespace ControleVeiculos.Domain.Repositories
{
    public interface IVacancieRepository
    {
        void Add(Vacancie vacancie);
        void Update(Vacancie vacancie);
        Vacancie GetByID(int vacancieID);
        List<Vacancie> GetAll(FilterVacancieCommand command);
        List<Vacancie> GetAll(int vacancieID);
        void Delete(int vacancieID);
        string GetVacancieNameByID(int vacancieID);
    }
}
