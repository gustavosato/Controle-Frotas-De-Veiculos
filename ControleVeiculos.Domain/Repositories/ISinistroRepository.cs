using ControleVeiculos.Domain.Command.Sinistros;
using ControleVeiculos.Domain.Entities.Sinistros;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ISinistroRepository
    {
        void Add(Sinistro sinistro);
        void Update(Sinistro sinistro);
        Sinistro GetByID(int sinistroID);
        List<Sinistro> GetAll(FilterSinistroCommand command);
        void Delete(int sinistroID);
    }
}
