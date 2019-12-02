using ControleVeiculos.Domain.Entities.Sinistros;
using ControleVeiculos.MVC.Models.Sinistros;

namespace ControleVeiculos.MVC.Extensions
{
    public static class SinistroMappingExtensions
    {
        public static SinistroModel ToModel(this Sinistro entity)
        {
            if (entity == null)
                return null;

            var model = new SinistroModel
            {
                SinistroID = entity.sinistroID,
                Apolice = entity.apolice,
                Franquia = entity.franquia,
                TipoSinistro = entity.tipoSinistro,
            };

            return model;
        }
    }
}