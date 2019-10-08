using ControleVeiculos.Domain.Command.Kilometragens;
using ControleVeiculos.Domain.Entities.Kilometragens;
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
