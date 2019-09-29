using ControleVeiculos.Domain.Command.Clientes;
using ControleVeiculos.Domain.Entities.Clientes;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IClienteService : IDisposable
    {
        void Add(MaintenanceClienteCommand command);
        void Update(MaintenanceClienteCommand command);
        Result<Cliente> GetByID(int clienteID);
        IPagedList<Cliente> GetAll(FilterClienteCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int clienteID);
    }
}
