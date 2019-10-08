using ControleVeiculos.Domain.Command.Filiais;
using ControleVeiculos.Domain.Entities.Filiais;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IFilialRepository
    {
        void Add(Filial filial);
        void Update(Filial filial);
        Filial GetByID(int filialID);
        List<Filial> GetAll(FilterFilialCommand command);
        void Delete(int filialID);
    }
}
