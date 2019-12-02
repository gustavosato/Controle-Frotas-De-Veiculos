using ControleVeiculos.Domain.Entities.Seguros;
using ControleVeiculos.MVC.Models.Seguros;

namespace ControleVeiculos.MVC.Extensions
{
    public static class SeguroMappingExtensions
    {
        public static SeguroModel ToModel(this Seguro entity)
        {
            if (entity == null)
                return null;

            var model = new SeguroModel
            {
                SeguroID = entity.seguroID,
                VeiculoID = entity.veiculoID,
                Apolice = entity.apolice,
                Seguradora = entity.seguradora,
                Franquia = entity.franquia,
                TipoSeguro = entity.tipoSeguro,
                DataContratacao = entity.dataContratacao,
                Vigencia = entity.vigencia,
                FimContratacao = entity.fimContratacao,
                Renovacao = entity.renovacao,
                TelefoneSeguradora = entity.telefoneSeguradora,
                PeriodoCarencia = entity.periodoCarencia,
                Indenizacao = entity.indenizacao,

            };

            return model;
        }
    }
}