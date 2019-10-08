using ControleVeiculos.Domain.Command.Abastecimentos;
using System;

namespace ControleVeiculos.Domain.Entities.Abastecimentos
{
    public static class AbastecimentoExtensions
    {
        public static Result<Abastecimento> GetAbastecimento(this Abastecimento abastecimento)
        {
            return Result.Ok(0, "", abastecimento);
        }

        public static Abastecimento Map(this Abastecimento abastecimento, MaintenanceAbastecimentoCommand command)
        {

            abastecimento.abastecimentoID = command.AbastecimentoID;
            abastecimento.tipoCombustivel = command.TipoCombustivel;
            abastecimento.responsavel = command.Responsavel;
            abastecimento.data = command.Data;
            abastecimento.kmAtual = command.KmAtual;
            abastecimento.veiculoID = command.VeiculoID;
            
            return abastecimento;
        }
    }
}
