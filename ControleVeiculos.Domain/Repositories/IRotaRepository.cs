using ControleVeiculos.Domain.Command.Rotas;
using ControleVeiculos.Domain.Entities.Rotas;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IRotaRepository
    {
        void Add(Rota rota);
        void Update(Rota rota);
        Rota GetByID(int rotaID);
        List<Rota> GetAll(FilterRotaCommand command);
        void Delete(int rotaID);
    }
}
