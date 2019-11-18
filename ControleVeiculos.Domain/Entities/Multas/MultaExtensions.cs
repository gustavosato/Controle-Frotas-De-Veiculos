using ControleVeiculos.Domain.Command.Multas;
using System;

namespace ControleVeiculos.Domain.Entities.Multas
{
    public static class MultaExtensions
    {
        public static Result<Multa> GetMulta(this Multa multa)
        {
            return Result.Ok(0, "", multa);
        }

        public static Multa Map(this Multa multa, MaintenanceMultaCommand command)
        {

            multa.multaID = command.MultaID;
            multa.veiculoID = command.VeiculoID;
            multa.funcionarioID = command.FuncionarioID;
            
            return multa;
        }
    }
}
