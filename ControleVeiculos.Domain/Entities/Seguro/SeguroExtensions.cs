using ControleVeiculos.Domain.Command.Seguros;
using System;

namespace ControleVeiculos.Domain.Entities.Seguro
{
    public static class SeguroExtensions
    {
        public static Result<Seguro> GetSeguro(this Seguro seguro)
        {
            return Result.Ok(0, "", seguro);
        }

        public static Seguro Map(this Seguro seguro, MaintenanceSeguroCommand command)
        {

            seguro.seguroID = command.SeguroID;
            seguro.apolice = command.Apolice;
            seguro.seguradora = command.Seguradora;
            seguro.franquia = command.Franquia;
            seguro.tipoSeguro = command.TipoSeguro;
            seguro.dataContratacao = command.DataContratacao;
            seguro.vigencia = command.Vigencia;
            seguro.fimContratacao = command.FimContratacao;
            seguro.renovacao = command.Renovacao;
            seguro.telefoneSeguradora = command.TelefoneSeguradora;
            seguro.periodoCarencia = command.PeriodoCarencia;
            seguro.indenizacao = command.Indenizacao;
            seguro.sinistroID = command.SinistroID;
            seguro.veiculoID = command.VeiculoID;

            return seguro;
        }
    }
}
