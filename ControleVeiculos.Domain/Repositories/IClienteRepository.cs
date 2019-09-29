using ControleVeiculos.Domain.Command.Clientes;
using ControleVeiculos.Domain.Entities.Clientes;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IClienteRepository
    {
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        Cliente GetByID(int clienteID);
        List<Cliente> GetAll(FilterClienteCommand command);
        void Delete(int clienteID);
    }
}
