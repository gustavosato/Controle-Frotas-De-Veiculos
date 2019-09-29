using ControleVeiculos.Domain.Command.Acessorios;
using ControleVeiculos.Domain.Entities.Acessorios;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IAcessorioRepository
    {
        void Add(Acessorio acessorio);
        void Update(Acessorio acessorio);
        Acessorio GetByID(int acessorioID);
        List<Acessorio> GetAll(FilterAcessorioCommand command);
        void Delete(int acessorioID);
    }
}
