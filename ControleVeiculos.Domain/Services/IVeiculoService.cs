using ControleVeiculos.Domain.Command.Veiculos;
using ControleVeiculos.Domain.Entities.Veiculos;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IVeiculoService : IDisposable
    {
        void Add(MaintenanceVeiculoCommand command);
        void Update(MaintenanceVeiculoCommand command);
        Result<Veiculo> GetByID(int logID);
        IPagedList<Veiculo> GetAll(FilterVeiculoCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int logID);
    }
}
