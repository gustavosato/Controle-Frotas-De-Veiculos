using ControleVeiculos.Domain.Command.Kilometragems;
using System;

namespace ControleVeiculos.Domain.Entities.Kilometragem
{
    public static class KilometragemExtensions
    {
        public static Result<Kilometragem> GetKilometragem(this Kilometragem kilometragem)
        {
            return Result.Ok(0, "", kilometragem);
        }

        public static Kilometragem Map(this Kilometragem kilometragem, MaintenanceKilometragemCommand command)
        {

            kilometragem.kilometragemID = command.KilometragemID;
            kilometragem.kilometragemTotal = command.KilometragemTotal;
            kilometragem.veiculoID = command.VeiculoID;
            
            return kilometragem;
        }
    }
}
