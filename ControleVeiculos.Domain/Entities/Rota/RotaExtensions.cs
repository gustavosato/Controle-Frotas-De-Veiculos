using ControleVeiculos.Domain.Command.Rotas;
using System;

namespace ControleVeiculos.Domain.Entities.Rota
{
    public static class RotaExtensions
    {
        public static Result<Rota> GetRota(this Rota rota)
        {
            return Result.Ok(0, "", rota);
        }

        public static Rota Map(this Rota rota, MaintenanceRotaCommand command)
        {

            rota.rotaID = command.RotaID;
            rota.cidade = command.Cidade;
            rota.estado = command.Estado;
            rota.distancia = command.Distancia;
            rota.pedagio = command.Pedagio;
            rota.dataIda = command.DataIda;
            rota.dataVolta = command.DataVolta;

            return rota;
        }
    }
}
