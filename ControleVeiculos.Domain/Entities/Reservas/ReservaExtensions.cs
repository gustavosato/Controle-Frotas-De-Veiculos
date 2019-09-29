using ControleVeiculos.Domain.Command.Reservas;
using System;

namespace ControleVeiculos.Domain.Entities.Reservas
{
    public static class ReservaExtensions
    {
        public static Result<Reserva> GetReserva(this Reserva reserva)
        {
            return Result.Ok(0, "", reserva);
        }

        public static Reserva Map(this Reserva reserva, MaintenanceReservaCommand command)
        {

            reserva.reservaID = command.ReservaID;
            reserva.dataReserva = command.DataReserva;
            reserva.finalidade = command.Finalidade;
            reserva.destino = command.Destino;
            reserva.numeroCnh = command.NumeroCnh;
            reserva.veiculoID = command.VeiculoID;
            reserva.funcionarioID = command.FuncionarioID;
            
            return reserva;
        }
    }
}
