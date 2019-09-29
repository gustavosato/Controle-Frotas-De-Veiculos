using ControleVeiculos.Domain.Command.Kilometragems;
using ControleVeiculos.Domain.Entities.Kilometragems;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IKilometragemRepository
    {
        void Add(Kilometragem kilometragem);
        void Update(Kilometragem kilometragem);
        Kilometragem GetByID(int kilometragemID);
        List<Kilometragem> GetAll(FilterKilometragemCommand command);
        void Delete(int kilometragemID);
    }
}
