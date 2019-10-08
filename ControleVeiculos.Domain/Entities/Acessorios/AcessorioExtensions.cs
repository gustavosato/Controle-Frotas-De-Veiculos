using ControleVeiculos.Domain.Command.Acessorios;
using System;

namespace ControleVeiculos.Domain.Entities.Acessorios
{
    public static class AcessorioExtensions
    {
        public static Result<Acessorio> GetAcessorio(this Acessorio acessorio)
        {
            return Result.Ok(0, "", acessorio);
        }

        public static Acessorio Map(this Acessorio acessorio, MaintenanceAcessorioCommand command)
        {

            acessorio.acessorioID = command.AcessoriosID;
            acessorio.gps = command.Gps;
            acessorio.airBag = command.AirBag;
            acessorio.arCondicionado = command.ArCondicionado;
            acessorio.direcao = command.Direcao;
            acessorio.travasEletricas = command.TravasEletricas;
            acessorio.vidroEletrico = command.VidroEletrico;
            acessorio.alarme = command.Alarme;

            return acessorio;
        }
    }
}
