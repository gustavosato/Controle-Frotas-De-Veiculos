using ControleVeiculos.Domain.Command.Cnhs;
using ControleVeiculos.Domain.Entities.Cnhs;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ICnhRepository
    {
        void Add(Cnh cnh);
        void Update(Cnh cnh);
        Cnh GetByID(int cnhID);
        List<Cnh> GetAll(FilterCnhCommand command);
        void Delete(int cnhID);
    }
}
