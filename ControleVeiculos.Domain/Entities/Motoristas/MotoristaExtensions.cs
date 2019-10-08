using ControleVeiculos.Domain.Command.Motoristas;
using System;

namespace ControleVeiculos.Domain.Entities.Motoristas
{
    public static class MotoristaExtensions
    {
        public static Result<Motorista> GetMotorista(this Motorista motorista)
        {
            return Result.Ok(0, "", motorista);
        }

        public static Motorista Map(this Motorista motorista, MaintenanceMotoristaCommand command)
        {

            motorista.motoristaID = command.MotoristaID;
            motorista.nomeMotorista = command.NomeMotorista;
            motorista.numeroCnh = command.NumeroCnh;
            
            return motorista;
        }
    }
}
