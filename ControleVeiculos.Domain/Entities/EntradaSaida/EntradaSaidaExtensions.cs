using ControleVeiculos.Domain.Command.EntradaSaidas;
using System;

namespace ControleVeiculos.Domain.Entities.EntradaSaida
{
    public static class EntradaSaidaExtensions
    {
        public static Result<EntradaSaida> GetEntradaSaida(this EntradaSaida entradaSaida)
        {
            return Result.Ok(0, "", entradaSaida);
        }

        public static EntradaSaida Map(this EntradaSaida entradaSaida, MaintenanceEntradaSaidaCommand command)
        {

            entradaSaida.entradaSaidaID = command.EntradaSaidaID;
            entradaSaida.emprestimoID = command.Disponibilidade;
            entradaSaida.servicosID = command.EmUso;
            entradaSaida.veiculoID = command.EmManutencao;
            
            return entradaSaida;
        }
    }
}
