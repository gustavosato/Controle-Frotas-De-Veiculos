using ControleVeiculos.Domain.Command.Motoristas;
using ControleVeiculos.Domain.Entities.Motoristas;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IMotoristaRepository
    {
        void Add(Motorista motorista);
        void Update(Motorista motorista);
        Motorista GetByID(int motoristaID);
        List<Motorista> GetAll(FilterMotoristaCommand command);
        void Delete(int motoristaID);
    }
}
