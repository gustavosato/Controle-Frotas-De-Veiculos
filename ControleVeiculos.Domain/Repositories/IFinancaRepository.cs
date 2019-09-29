using ControleVeiculos.Domain.Command.Financas;
using ControleVeiculos.Domain.Entities.Financas;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IFinancaRepository
    {
        void Add(Financa financa);
        void Update(Financa financa);
        Financa GetByID(int financaID);
        List<Financa> GetAll(FilterFinancaCommand command);
        void Delete(int financaID);
    }
}
