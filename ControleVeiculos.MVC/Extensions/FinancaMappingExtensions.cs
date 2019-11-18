using ControleVeiculos.Domain.Entities.Financas;
using ControleVeiculos.MVC.Models.Financas;

namespace ControleVeiculos.MVC.Extensions
{
    public static class FinancaMappingExtensions
    {
        public static FinancaModel ToModel(this Financa entity)
        {
            if (entity == null)
                return null;

            var model = new FinancaModel
            {
                FinancaID = entity.financaID,
                ValorCarro = entity.valorCarro,
                ValorSeguro = entity.valorSeguro,
                ValorAgua = entity.valorAgua,
                ValorLuz = entity.valorLuz,
                ValorInternet = entity.valorInternet,
                ValorManutencao = entity.valorManutencao,
                Salarios = entity.salarios,
                GastosExtras = entity.gastosExtras,

            };

            return model;
        }
    }
}