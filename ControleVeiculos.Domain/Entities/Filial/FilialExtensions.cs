using ControleVeiculos.Domain.Command.Filials;
using System;

namespace ControleVeiculos.Domain.Entities.Filial
{
    public static class FilialExtensions
    {
        public static Result<Filial> GetFilial(this Filial filial)
        {
            return Result.Ok(0, "", filial);
        }

        public static Filial Map(this Filial filial, MaintenanceFilialCommand command)
        {

            filial.filialID = command.FilialID;
            filial.nomeFilial = command.NomeFilial;
            filial.cidade = command.Cidade;
            filial.estado= command.Estado;
            
            return filial;
        }
    }
}
