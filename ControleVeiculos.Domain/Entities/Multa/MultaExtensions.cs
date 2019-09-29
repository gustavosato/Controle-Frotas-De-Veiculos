using ControleVeiculos.Domain.Command.Multas;
using System;

namespace ControleVeiculos.Domain.Entities.Multa
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
            multa.clienteID = command.ClienteID;
            multa.cnhID = command.CnhID;
            
            return multa;
        }
    }
}
