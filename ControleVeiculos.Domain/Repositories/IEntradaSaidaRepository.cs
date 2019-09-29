using ControleVeiculos.Domain.Command.EntradaSaidas;
using ControleVeiculos.Domain.Entities.EntradaSaidas;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IEntradaSaidaRepository
    {
        void Add(EntradaSaida entradaSaida);
        void Update(EntradaSaida entradaSaida);
        EntradaSaida GetByID(int entradaSaidaID);
        List<EntradaSaida> GetAll(FilterEntradaSaidaCommand command);
        void Delete(int entradaSaidaID);
    }
}
