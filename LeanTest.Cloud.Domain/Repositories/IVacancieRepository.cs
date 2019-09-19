using Lean.Test.Cloud.Domain.Command.Vacancies;
using Lean.Test.Cloud.Domain.Entities.Vacancies;
using System.Collections.Generic;


namespace Lean.Test.Cloud.Domain.Repositories
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
