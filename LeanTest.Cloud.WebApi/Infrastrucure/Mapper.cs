//using Lean.Test.Cloud.Domain.Command.Contratos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Lean.Test.Cloud.WebApi.Infrastrucure
//{
//    public static class Mapper
//    {
//        public static RegistrarContratoCommand PrepareRegistrarContratoCommand(string contrato)
//        {
//            RegistrarContratoCommand command = new RegistrarContratoCommand();

//            command.Chassi = contrato.Substring(1, 21);
//            command.IdenRemarcacao = contrato.Substring(22, 1);
//            command.UfLiberacaoCredito= contrato.Substring(23, 2);
//            command.UfPlaca = contrato.Substring(25, 2);
//            command.Placa = contrato.Substring(27, 7);
//            command.Renavam = contrato.Substring(34, 11);
//            command.AnoFabricacao = contrato.Substring(45, 4);
//            command.AnoModelo = contrato.Substring(49, 4);
//            command.CodAgente = contrato.Substring(53, 12);
//            command.NomeAgente = contrato.Substring(65, 40);
//            command.CnpjAgente = contrato.Substring(0, 0);
//            command.NumContrato = contrato.Substring(0, 0);
//            command.DataVigenciaContrato = contrato.Substring(0, 0);
//            command.QtdParcela = contrato.Substring(0, 0);
//            command.NumRestricao = contrato.Substring(0, 0);
//            command.TipoRestriFinan = contrato.Substring(0, 0);
//            command.CpfCgcDevedor = contrato.Substring(0, 0);
//            command.NomeDevedor = contrato.Substring(174, 40);
//            command.TxJurosMes = contrato.Substring(214, 6);
//            command.TxJurosAno = contrato.Substring(0, 0);
//            command.TxMulta = contrato.Substring(0, 0);
//            command.TxMora = contrato.Substring(0, 0);
//            command.VlrTxContrato = contrato.Substring(0, 0);
//            command.VlrTotalFinan = contrato.Substring(0, 0);
//            command.VlrIof = contrato.Substring(0, 0);
//            command.VlrParcelaFinan = contrato.Substring(0, 0);
//            command.VlrIof = contrato.Substring(0, 0);
//            command.DtVencPrimeiraParcela = contrato.Substring(0, 0);
//            command.DtVencUltimaParcela = contrato.Substring(0, 0);
//            command.DtLiberacaoCredito = contrato.Substring(0, 0);
//            command.UfLiberacaoCredito = contrato.Substring(0, 0);
//            command.MunLiberacaoCredito = contrato.Substring(0, 0);
//            command.Indice = contrato.Substring(0, 0);
//            command.NumGrupoConsorcio = contrato.Substring(0, 0);
//            command.NumCotaConsorcio = contrato.Substring(0, 0);
//            command.NumRegContrato = contrato.Substring(0, 0);
//            command.NumRegAditivo = contrato.Substring(0, 0);
//            command.DataAditivo = contrato.Substring(0, 0);
//            command.FlagTransacao = contrato.Substring(0, 0);
//            command.icAtivo = "1";

//            return command;
//        }
//    }
//}