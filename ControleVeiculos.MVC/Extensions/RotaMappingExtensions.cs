using ControleVeiculos.Domain.Entities.Rotas;
using ControleVeiculos.MVC.Models.Rotas;

namespace ControleVeiculos.MVC.Extensions
{
    public static class RotaMappingExtensions
    {
        public static RotaModel ToModel(this Rota entity)
        {
            if (entity == null)
                return null;

            var model = new RotaModel
            {
                RotaID = entity.rotaID,
                Cidade = entity.cidade,
                Estado = entity.estado,
                Distancia = entity.distancia,
                DataIda = entity.dataIda,
                DataVolta = entity.dataVolta,
                Pedagio = entity.pedagio,
            };

            return model;
        }
    }
}