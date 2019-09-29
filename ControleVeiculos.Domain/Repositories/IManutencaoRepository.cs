using ControleVeiculos.Domain.Command.Manutencaos;
using ControleVeiculos.Domain.Entities.Manutencaos;
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
