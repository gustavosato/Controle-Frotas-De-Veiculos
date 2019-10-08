using ControleVeiculos.Domain.Command.Manutencoes;
using System;

namespace ControleVeiculos.Domain.Entities.Manutencoes
{
    public static class ManutencaoExtensions
    {
        public static Result<Manutencao> GetManutencao(this Manutencao manutencao)
        {
            return Result.Ok(0, "", manutencao);
        }

        public static Manutencao Map(this Manutencao manutencao, MaintenanceManutencaoCommand command)
        {

            manutencao.manutencaoID = command.ManutencaoID;
            manutencao.responsavel = command.Responsavel;
            manutencao.dataManutencao = command.DataManutencao;
            manutencao.descricao = command.Descricao;
            manutencao.veiculoID = command.VeiculoID;

            return manutencao;
        }
    }
}
