using ControleVeiculos.Domain.Command.Clientes;
using System;

namespace ControleVeiculos.Domain.Entities.Clientes
{
    public static class ClienteExtensions
    {
        public static Result<Cliente> GetCliente(this Cliente cliente)
        {
            return Result.Ok(0, "", cliente);
        }

        public static Cliente Map(this Cliente cliente, MaintenanceClienteCommand command)
        {

            cliente.clienteID = command.ClienteID;
            cliente.nomeCliente = command.NomeCliente;
            cliente.ramo = command.Ramo;
            cliente.cidade = command.Cidade;
            cliente.estado = command.Estado;
            cliente.telefone = command.Telefone;
            cliente.email = command.Email;
            cliente.status = command.Status;

            return cliente;
        }
    }
}
