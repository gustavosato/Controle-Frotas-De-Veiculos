using ControleVeiculos.Domain.Command.Veiculos;
using System;

namespace ControleVeiculos.Domain.Entities.Veiculos
{
    public static class VeiculoExtensions
    {
        public static Result<Veiculo> GetVeiculo(this Veiculo veiculo)
        {
            return Result.Ok(0, "", veiculo);
        }

        public static Veiculo Map(this Veiculo veiculo, MaintenanceVeiculoCommand command)
        {

            veiculo.veiculoID = command.VeiculoID;
            veiculo.modelo = command.Modelo;
            veiculo.cor = command.Cor;
            veiculo.placa = command.Placa;
            veiculo.status = command.Status;
            veiculo.ano = command.Ano;
            veiculo.manutencaoID = command.ManutencaoID;
            veiculo.abastecimentoID = command.AbastecimentoID;
            veiculo.numeroChassi = command.NumeroChassi;
            veiculo.motor = command.Motor;
            
            return veiculo;
        }
    }
}
