using ControleVeiculos.Domain.Command.Status;
using System;

namespace ControleVeiculos.Domain.Entities.Status
{
    public static class StatusExtensions
    {
        public static Result<Status> GetStatus(this Status status)
        {
            return Result.Ok(0, "", status);
        }

        public static Status Map(this Status status, MaintenanceStatusCommand command)
        {

            status.statusID = command.StatusID;
            status.disponibilidade = command.Disponibilidade;
            status.emUso = command.EmUso;
            status.emManutencao = command.EmManutencao;
            status.reservado = command.Reservado;
            status.veiculoID = command.VeiculoID;
            
            return status;
        }
    }
}
