using ControleVeiculos.Domain.Entities.Multas;
using ControleVeiculos.MVC.Models.Multas;

namespace ControleVeiculos.MVC.Extensions
{
    public static class MultaMappingExtensions
    {
        public static MultaModel ToModel(this Multa entity)
        {
            if (entity == null)
                return null;

            var model = new MultaModel
            {
                MultaID = entity.multaID,
                VeiculoID = entity.veiculoID,
                FuncionarioID = entity.funcionarioID,
                
            };

            return model;
        }
    }
}