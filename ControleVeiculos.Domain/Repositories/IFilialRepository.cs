using ControleVeiculos.Domain.Command.Filials;
using ControleVeiculos.Domain.Entities.Filials;
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
