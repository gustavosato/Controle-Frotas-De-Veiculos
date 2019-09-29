using ControleVeiculos.Domain.Command.Multas;
using ControleVeiculos.Domain.Entities.Multas;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IMultaRepository
    {
        void Add(Multa multa);
        void Update(Multa multa);
        Multa GetByID(int multaID);
        List<Multa> GetAll(FilterMultaCommand command);
        void Delete(int multaID);
    }
}
