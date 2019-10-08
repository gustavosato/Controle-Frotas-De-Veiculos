using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Clientes;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Clientes;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class ClienteService : BaseAppService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public void Add(MaintenanceClienteCommand command)
        {
            Cliente cliente = new Cliente();

            cliente = cliente.Map(command);

            _clienteRepository.Add(cliente);
        }

        public void Update(MaintenanceClienteCommand command)
        {
            Cliente cliente = new Cliente();

            cliente = cliente.Map(command);

            _clienteRepository.Update(cliente);
        }

        //public IList<Cliente> GetAll(int clienteID)
        //{
        //    var cliente = _clienteRepository.GetAll(clienteID);

        //    return new List<Cliente>(cliente);
        //}

        public Result<Cliente> GetByID(int clienteID)
        {
            var cliente = _clienteRepository.GetByID(clienteID);

            return Result.Ok<Cliente>(0, "", cliente);
        }

        public IPagedList<Cliente> GetAll(FilterClienteCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var cliente = _clienteRepository.GetAll(command);

            return new PagedList<Cliente>(cliente, pageIndex, pageSize);
        }

        public void Delete(int clienteID)
        {
            _clienteRepository.Delete(clienteID);
        }
       
    }
}

