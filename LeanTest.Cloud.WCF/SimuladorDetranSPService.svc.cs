//using System;
//using DevTrends.WCFDataAnnotations;
//using Lean.Test.Cloud.WCF.DTO.SaoPaulo.Contratos;
//using Lean.Test.Cloud.Domain.Services;
//using Lean.Test.Cloud.Domain;
//using Lean.Test.Cloud.Domain.Entities.Contratos;
//using Lean.Test.Cloud.Domain.Command.Contratos;
//using Lean.Test.Cloud.WCF.DTO.SaoPaulo.Gravames;
//using Lean.Test.Cloud.Domain.Command.Gravames;
//using Lean.Test.Cloud.Domain.Entities.Gravames;
//using Lean.Test.Cloud.WCF.Extensions;
//using Lean.Test.Cloud.Domain.Validations.Contratos;

//namespace Lean.Test.Cloud.WCF
//{
//    [ValidateDataAnnotationsBehavior]
//    public class SimuladorDetranSPService : ISimuladorDetranSPService
//    {
//        private readonly IContratoService _contratoService;
//        private readonly IGravameService _gravameService;

//        public SimuladorDetranSPService(IContratoService contratoService, IGravameService gravameService)
//        {
//            _contratoService = contratoService;
//            _gravameService = gravameService;
//        }

//        public ContratoFinanciamentoVeiculoResponseSpDTO ComunicarContratoFinanciamentoVeiculo(ComunicarContratoFinanciamentoVeiculoSpDTO contratoFinanVeiculoDTO)
//        {
//            try
//            {
//                if (contratoFinanVeiculoDTO == null || contratoFinanVeiculoDTO.ContratoFinanciamentoVeiculo == null)
//                    throw new ArgumentNullException(nameof(contratoFinanVeiculoDTO));

//                var contratoFinanVeiculoResponse = new ContratoFinanciamentoVeiculoResponseSpDTO();
//                contratoFinanVeiculoResponse.Retorno = new ContratoFinanciamentoVeiculoRetornoSpDTO();


//                var command = PrepareRegistrarContratoCommand(contratoFinanVeiculoDTO.ContratoFinanciamentoVeiculo);

//                Result<Contrato> contrato = _contratoService.RegistrarContrato(command, new ValidationContratoSPFactory());

//                if (contrato.IsSuccess)
//                {
//                    contratoFinanVeiculoResponse.Retorno.Codigo = contrato.Code;
//                    contratoFinanVeiculoResponse.Retorno.Descricao = contrato.Info;
//                    contratoFinanVeiculoResponse.Retorno.FlagTransacao = int.Parse(contrato.Value.FlagTransacao);
//                    contratoFinanVeiculoResponse.Retorno.Chassi = contrato.Value.Chassi;

//                    return contratoFinanVeiculoResponse;
//                }

//                contratoFinanVeiculoResponse.Retorno.Codigo = contrato.Code;
//                contratoFinanVeiculoResponse.Retorno.Descricao = contrato.Error;
//                contratoFinanVeiculoResponse.Retorno.FlagTransacao = int.Parse(contratoFinanVeiculoDTO.ContratoFinanciamentoVeiculo.FlagTransacao);
//                contratoFinanVeiculoResponse.Retorno.Chassi = contratoFinanVeiculoDTO.ContratoFinanciamentoVeiculo.Chassi;

//                return contratoFinanVeiculoResponse;
//            }
//            catch (Exception ex)
//            {
//                var contratoFinanVeiculoResponse = new ContratoFinanciamentoVeiculoResponseSpDTO();
//                contratoFinanVeiculoResponse.Retorno = new ContratoFinanciamentoVeiculoRetornoSpDTO();

//                contratoFinanVeiculoResponse.Retorno.Codigo = 1501;
//                contratoFinanVeiculoResponse.Retorno.Descricao = ex.Message;
//                contratoFinanVeiculoResponse.Retorno.FlagTransacao = int.Parse(contratoFinanVeiculoDTO.ContratoFinanciamentoVeiculo.FlagTransacao);
//                contratoFinanVeiculoResponse.Retorno.Chassi = contratoFinanVeiculoDTO.ContratoFinanciamentoVeiculo.Chassi;
                
//                return contratoFinanVeiculoResponse;
//            }
//        }

//        public ConsultaGravameResponseSpDTO ConsultarGravame(ConsultarGravameSpDTO gravameDTO)
//        {
//            try
//            {
//                if (gravameDTO == null)
//                    throw new ArgumentNullException(nameof(gravameDTO));

//                var consultaGravameResponse = new ConsultaGravameResponseSpDTO();
//                consultaGravameResponse.RetornoConsultaGravame = new RetornoConsultaGravameSpDTO();

//                if (string.IsNullOrEmpty(gravameDTO.ConsultaGravame.Chassi) && string.IsNullOrEmpty(gravameDTO.ConsultaGravame.Placa))
//                {
//                    consultaGravameResponse.RetornoConsultaGravame.Codigo = 0;
//                    consultaGravameResponse.RetornoConsultaGravame.Descricao = "É necessario o preechimento de pelo menos um dos campos (Chassi ou Placa).";
//                    return consultaGravameResponse;
//                }

//                var command = new ConsultarGravameCommand(gravameDTO.ConsultaGravame.Chassi,
//                                                          gravameDTO.ConsultaGravame.Placa,
//                                                          (TipoPesquisa)gravameDTO.ConsultaGravame.TipoPesquisa);

//                Result<Gravame> gravame = _gravameService.ConsultarGravame(command);

//                if (gravame.IsSuccess)
//                {
//                    consultaGravameResponse.RetornoConsultaGravame.Codigo = gravame.Code;
//                    consultaGravameResponse.RetornoConsultaGravame.DetalheGravame = new DetalheGravameSpDTO();
//                    consultaGravameResponse.RetornoConsultaGravame.DetalheGravame = gravame.Value.Map();

//                    return consultaGravameResponse;
//                }

//                consultaGravameResponse.RetornoConsultaGravame.Codigo = gravame.Code;
//                consultaGravameResponse.RetornoConsultaGravame.Descricao = gravame.Error;

//                return consultaGravameResponse;
//            }
//            catch
//            {
//                throw;
//            }
//        }


//        protected RegistrarContratoCommand PrepareRegistrarContratoCommand(ContratoFinanciamentoVeiculoSpDTO contratoDTO)
//        {
//            var registrarContratoCommand = new RegistrarContratoCommand();

//            registrarContratoCommand.FlagTransacao = contratoDTO.FlagTransacao;
//            registrarContratoCommand.Chassi = contratoDTO.Chassi;
//            registrarContratoCommand.IdenRemarcacao = contratoDTO.IdenRemarcacao;
//            registrarContratoCommand.Placa = contratoDTO.Placa;
//            registrarContratoCommand.UfPlaca = contratoDTO.UfPlaca;
//            registrarContratoCommand.Renavam = contratoDTO.Renavam;
//            registrarContratoCommand.AnoFabricacao = contratoDTO.AnoFabricacao;
//            registrarContratoCommand.AnoModelo = contratoDTO.AnoModelo;
//            registrarContratoCommand.CodAgente = contratoDTO.CodAgente;
//            registrarContratoCommand.NomeAgente = contratoDTO.NomeAgente;
//            registrarContratoCommand.CnpjAgente = contratoDTO.CnpjAgente;
//            registrarContratoCommand.NumContrato = contratoDTO.NumContrato;
//            registrarContratoCommand.DataVigenciaContrato = contratoDTO.DataVigenciaContrato;
//            registrarContratoCommand.QtdParcela = contratoDTO.QtdParcela;
//            registrarContratoCommand.NumRestricao = contratoDTO.NumRestricao;
//            registrarContratoCommand.TipoRestriFinan = contratoDTO.TipoRestriFinan;
//            registrarContratoCommand.NumAditivo = contratoDTO.NumAditivo;
//            registrarContratoCommand.DataAditivo = contratoDTO.DataAditivo;
//            registrarContratoCommand.TxJurosMes = contratoDTO.TxJurosMes;
//            registrarContratoCommand.TxJurosAno = contratoDTO.TxJurosAno;
//            registrarContratoCommand.TxMulta = contratoDTO.TxMulta;
//            registrarContratoCommand.TxMora = contratoDTO.TxMora;
//            registrarContratoCommand.VlrTxMora = contratoDTO.VlrTxMora;
//            registrarContratoCommand.VlrTxContrato = contratoDTO.VlrTxContrato;
//            registrarContratoCommand.VlrIof = contratoDTO.VlrIof;
//            registrarContratoCommand.Indice = contratoDTO.Indice;
//            registrarContratoCommand.VlrTotalFinan = contratoDTO.VlrTotalFinan;
//            registrarContratoCommand.VlrParcelaFinan = contratoDTO.VlrParcelaFinan;
//            registrarContratoCommand.DtVencPrimeiraParcela = contratoDTO.DtVencPrimeiraParcela;
//            registrarContratoCommand.DtVencUltimaParcela = contratoDTO.DtVencUltimaParcela;
//            registrarContratoCommand.DtLiberacaoCredito = contratoDTO.DtLiberacaoCredito;
//            registrarContratoCommand.UfLiberacaoCredito = contratoDTO.UfLiberacaoCredito;
//            registrarContratoCommand.MunLiberacaoCredito = contratoDTO.MunLiberacaoCredito;
//            registrarContratoCommand.NumGrupoConsorcio = contratoDTO.NumGrupoConsorcio;
//            registrarContratoCommand.NumCotaConsorcio = contratoDTO.NumCotaConsorcio;
//            registrarContratoCommand.NomeLogradouroCredor = contratoDTO.NomeLogradouroCredor;
//            registrarContratoCommand.NumImovelCredor = contratoDTO.NumImovelCredor;
//            registrarContratoCommand.ComplementoImovelCredor = contratoDTO.ComplementoImovelCredor;
//            registrarContratoCommand.BairroImovelCredor = contratoDTO.BairroImovelCredor;
//            registrarContratoCommand.CodMunCredor = contratoDTO.CodMunCredor;
//            registrarContratoCommand.UfCredor = contratoDTO.UfCredor;
//            registrarContratoCommand.CepCredor = contratoDTO.CepCredor;
//            registrarContratoCommand.DddCredor = contratoDTO.DddCredor;
//            registrarContratoCommand.NumTelCredor = contratoDTO.NumTelCredor;
//            registrarContratoCommand.CpfCgcDevedor = contratoDTO.CpfCgcDevedor;
//            registrarContratoCommand.NomeDevedor = contratoDTO.NomeDevedor;
//            registrarContratoCommand.NomeLograDevedor = contratoDTO.NomeLograDevedor;
//            registrarContratoCommand.NumImovelDevedor = contratoDTO.NumImovelDevedor;
//            registrarContratoCommand.CompleImovelDevedor = contratoDTO.CompleImovelDevedor;
//            registrarContratoCommand.BairroDevedor = contratoDTO.BairroDevedor;
//            registrarContratoCommand.CodMunDevedor = contratoDTO.CodMunDevedor;
//            registrarContratoCommand.UfDevedor = contratoDTO.UfDevedor;
//            registrarContratoCommand.CepDevedor = contratoDTO.CepDevedor;
//            registrarContratoCommand.DddDevedor = contratoDTO.DddDevedor;
//            registrarContratoCommand.NumTelDevedor = contratoDTO.NumTelDevedor;
//            registrarContratoCommand.NumRegContrato = contratoDTO.NumRegContrato;
//            registrarContratoCommand.NumRegAditivo = contratoDTO.NumRegAditivo;
//            registrarContratoCommand.IndicativoPenalidade = contratoDTO.IndicativoPenalidade;
//            registrarContratoCommand.Penalidade = contratoDTO.Penalidade;
//            registrarContratoCommand.IndicativoComissao = contratoDTO.IndicativoComissao;
//            registrarContratoCommand.icAtivo = "1";
//            registrarContratoCommand.DsSigla = "SP";

//            return registrarContratoCommand;
//        }
//    }
//}
