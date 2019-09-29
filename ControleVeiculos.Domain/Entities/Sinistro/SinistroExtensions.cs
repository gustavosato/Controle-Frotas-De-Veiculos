using ControleVeiculos.Domain.Command.Sinistros;
using System;

namespace ControleVeiculos.Domain.Entities.Sinistro
{
    public static class SinistroExtensions
    {
        public static Result<Sinistro> GetSinistro(this Sinistro sinistro)
        {
            return Result.Ok(0, "", sinistro);
        }

        public static Sinistro Map(this Sinistro sinistro, MaintenanceSinistroCommand command)
        {

            sinistro.sinistroID = command.SinistroID;
            sinistro.apolice = command.Apolice;
            sinistro.franquia = command.Franquia;
            sinistro.tipoSinistro = command.TipoSinistro;
           
            return sinistro;
        }
    }
}
