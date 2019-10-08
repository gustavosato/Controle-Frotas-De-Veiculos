using ControleVeiculos.Domain.Command.Manutencoes;
using ControleVeiculos.Domain.Entities.Manutencoes;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IManutencaoRepository
    {
        void Add(Manutencao manutencao);
        void Update(Manutencao manutencao);
        Manutencao GetByID(int manutencaoID);
        List<Manutencao> GetAll(FilterManutencaoCommand command);
        void Delete(int manutencaoID);
    }
}
