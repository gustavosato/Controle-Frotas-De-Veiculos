using ControleVeiculos.Domain.Command.Financas;
using System;

namespace ControleVeiculos.Domain.Entities.Financas
{
    public static class FinancaExtensions
    {
        public static Result<Financa> GetFinanca(this Financa financa)
        {
            return Result.Ok(0, "", financa);
        }

        public static Financa Map(this Financa financa, MaintenanceFinancaCommand command)
        {

            financa.financaID = command.FinancaID;
            financa.valorCarro = command.ValorCarro;
            financa.valorSeguro = command.ValorSeguro;
            financa.valorAgua = command.ValorAgua;
            financa.valorLuz = command.ValorLuz;
            financa.valorInternet = command.ValorInternet;
            financa.salarios = command.Salarios;
            financa.gastosExtras = command.GastosExtras;

            return financa;
        }
    }
}
