using ControleVeiculos.Domain.Command.Veiculos;
using ControleVeiculos.Domain.Entities.Veiculos;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IVeiculoService : IDisposable
    {
        void Add(MaintenanceVeiculoCommand command);
        void Update(MaintenanceVeiculoCommand command);
        Result<Veiculo> GetByID(int veiculoID);
        IPagedList<Veiculo> GetAll(FilterVeiculoCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Veiculo> GetAll(int veiculoID);
        void Delete(int veiculoID);
    }
}
