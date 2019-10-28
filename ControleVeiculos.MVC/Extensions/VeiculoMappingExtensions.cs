using ControleVeiculos.Domain.Entities.Veiculos;
using ControleVeiculos.MVC.Models.Veiculos;

namespace ControleVeiculos.MVC.Extensions
{
    public static class VeiculoMappingExtensions
    {
        public static VeiculoModel ToModel(this Veiculo entity)
        {
            if (entity == null)
                return null;

            var model = new VeiculoModel
            {
                VeiculoID = entity.veiculoID,
                Modelo = entity.modelo,
                Cor = entity.cor,
                Placa = entity.placa,
                Status = entity.status,
                Ano = entity.ano,
                ManutencaoID = entity.manutencaoID,
                AbastecimentoID = entity.abastecimentoID,
                NumeroChassi = entity.numeroChassi,
                Motor = entity.motor,

            };

            return model;
        }
    }
}