using ControleVeiculos.Domain.Command.Abastecimentos;
using ControleVeiculos.Domain.Entities.Abastecimentos;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IAbastecimentoRepository
    {
        void Add(Abastecimento abastecimento);
        void Update(Abastecimento abastecimento);
        Abastecimento GetByID(int abastecimentoID);
        List<Abastecimento> GetAll(FilterAbastecimentoCommand command);
        void Delete(int abastecimentoID);
    }
}
