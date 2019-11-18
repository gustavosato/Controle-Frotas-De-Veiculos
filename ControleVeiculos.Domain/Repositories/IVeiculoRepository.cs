using ControleVeiculos.Domain.Command.Veiculos;
using ControleVeiculos.Domain.Entities.Veiculos;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IVeiculoRepository
    {
        void Add(Veiculo veiculo);
        void Update(Veiculo veiculo);
        Veiculo GetByID(int veiculoID);
        List<Veiculo> GetAll(FilterVeiculoCommand command);
        void Delete(int veiculoID);
    }
}
