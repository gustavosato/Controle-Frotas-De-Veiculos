using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Lean.Test.Cloud.WCF.DTO.SaoPaulo.Contratos
{
    [MessageContract(WrapperNamespace = "")]
    public class ContratoFinanciamentoVeiculoSpDTO
    {
        [XmlElement(ElementName = "flagTransacao"), MessageBodyMember]
        [Required(AllowEmptyStrings = false)]
        [Range(1, 4)]
        public string FlagTransacao { get; set; }

        [XmlElement(ElementName = "chassi"), MessageBodyMember]
        [Required(AllowEmptyStrings = false)]
        public string Chassi { get; set; }

        [XmlElement(ElementName = "idenRemarcacao"), MessageBodyMember]
        public string IdenRemarcacao { get; set; }

        [XmlElement(ElementName = "placa"), MessageBodyMember]
        public string Placa { get; set; }

        [XmlElement(ElementName = "ufPlaca"), MessageBodyMember]
        public string UfPlaca { get; set; }

        [XmlElement(ElementName = "renavam"), MessageBodyMember]
        public string Renavam { get; set; }

        [XmlElement(ElementName = "anoFabricacao"), MessageBodyMember]
        public string AnoFabricacao { get; set; }

        [XmlElement(ElementName = "anoModelo"), MessageBodyMember]
        public string AnoModelo { get; set; }

        [XmlElement(ElementName = "codAgente"), MessageBodyMember]
        public string CodAgente { get; set; }

        [XmlElement(ElementName = "nomeAgente"), MessageBodyMember]
        public string NomeAgente { get; set; }

        [XmlElement(ElementName = "cnpjAgente"), MessageBodyMember]
        public string CnpjAgente { get; set; }

        [XmlElement(ElementName = "numContrato"), MessageBodyMember]
        public string NumContrato { get; set; }

        [XmlElement(ElementName = "dataVigenciaContrato"), MessageBodyMember]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "AAAAMMDD")]
        public string DataVigenciaContrato { get; set; }

        [XmlElement(ElementName = "qtdParcela"), MessageBodyMember]
        public string QtdParcela { get; set; }

        [XmlElement(ElementName = "numRestricao"), MessageBodyMember]
        public string NumRestricao { get; set; }

        [XmlElement(ElementName = "tipoRestriFinan"), MessageBodyMember]
        public string TipoRestriFinan { get; set; }

        [XmlElement(ElementName = "numAditivo"), MessageBodyMember]
        public string NumAditivo { get; set; }

        [XmlElement(ElementName = "dataAditivo"), MessageBodyMember]
        public string DataAditivo { get; set; }

        [XmlElement(ElementName = "txJurosMes"), MessageBodyMember]
        public string TxJurosMes { get; set; }

        [XmlElement(ElementName = "txJurosAno"), MessageBodyMember]
        public string TxJurosAno { get; set; }

        [XmlElement(ElementName = "vlrTxMulta"), MessageBodyMember]
        public string VlrTxMulta { get; set; }

        [XmlElement(ElementName = "txMulta"), MessageBodyMember]
        public string TxMulta { get; set; }

        [XmlElement(ElementName = "txMora"), MessageBodyMember]
        public string TxMora { get; set; }

        [XmlElement(ElementName = "vlrTxMora"), MessageBodyMember]
        public string VlrTxMora { get; set; }

        [XmlElement(ElementName = "vlrTxContrato"), MessageBodyMember]
        public string VlrTxContrato { get; set; }

        [XmlElement(ElementName = "vlrIof"), MessageBodyMember]
        public string VlrIof { get; set; }

        [XmlElement(ElementName = "indice"), MessageBodyMember]
        public string Indice { get; set; }

        [XmlElement(ElementName = "vlrTotalFinan"), MessageBodyMember]
        public string VlrTotalFinan { get; set; }

        [XmlElement(ElementName = "vlrParcelaFinan"), MessageBodyMember]
        public string VlrParcelaFinan { get; set; }

        [XmlElement(ElementName = "dtVencPrimeiraParcela"), MessageBodyMember]
        public string DtVencPrimeiraParcela { get; set; }

        [XmlElement(ElementName = "dtVencUltimaParcela"), MessageBodyMember]
        public string DtVencUltimaParcela { get; set; }

        [XmlElement(ElementName = "dtLiberacaoCredito"), MessageBodyMember]
        public string DtLiberacaoCredito { get; set; }

        [XmlElement(ElementName = "ufLiberacaoCredito"), MessageBodyMember]
        public string UfLiberacaoCredito { get; set; }

        [XmlElement(ElementName = "munLiberacaoCredito"), MessageBodyMember]
        public string MunLiberacaoCredito { get; set; }

        [XmlElement(ElementName = "numGrupoConsorcio"), MessageBodyMember]
        public string NumGrupoConsorcio { get; set; }

        [XmlElement(ElementName = "numCotaConsorcio"), MessageBodyMember]
        public string NumCotaConsorcio { get; set; }

        [XmlElement(ElementName = "nomeLogradouroCredor"), MessageBodyMember]
        public string NomeLogradouroCredor { get; set; }

        [XmlElement(ElementName = "numImovelCredor"), MessageBodyMember]
        public string NumImovelCredor { get; set; }

        [XmlElement(ElementName = "complementoImovelCredor"), MessageBodyMember]
        public string ComplementoImovelCredor { get; set; }

        [XmlElement(ElementName = "bairroImovelCredor"), MessageBodyMember]
        public string BairroImovelCredor { get; set; }

        [XmlElement(ElementName = "codMunCredor"), MessageBodyMember]
        public string CodMunCredor { get; set; }

        [XmlElement(ElementName = "ufCredor"), MessageBodyMember]
        public string UfCredor { get; set; }

        [XmlElement(ElementName = "cepCredor"), MessageBodyMember]
        public string CepCredor { get; set; }

        [XmlElement(ElementName = "dddCredor"), MessageBodyMember]
        public string DddCredor { get; set; }

        [XmlElement(ElementName = "numTelCredor"), MessageBodyMember]
        public string NumTelCredor { get; set; }

        [XmlElement(ElementName = "cpfCgcDevedor"), MessageBodyMember]
        public string CpfCgcDevedor { get; set; }

        [XmlElement(ElementName = "nomeDevedor"), MessageBodyMember]
        public string NomeDevedor { get; set; }

        [XmlElement(ElementName = "nomeLograDevedor"), MessageBodyMember]
        public string NomeLograDevedor { get; set; }

        [XmlElement(ElementName = "numImovelDevedor"), MessageBodyMember]
        public string NumImovelDevedor { get; set; }

        [XmlElement(ElementName = "compleImovelDevedor"), MessageBodyMember]
        public string CompleImovelDevedor { get; set; }

        [XmlElement(ElementName = "bairroDevedor"), MessageBodyMember]
        public string BairroDevedor { get; set; }

        [XmlElement(ElementName = "codMunDevedor"), MessageBodyMember]
        public string CodMunDevedor { get; set; }

        [XmlElement(ElementName = "ufDevedor"), MessageBodyMember]
        public string UfDevedor { get; set; }

        [XmlElement(ElementName = "cepDevedor"), MessageBodyMember]
        public string CepDevedor { get; set; }

        [XmlElement(ElementName = "dddDevedor"), MessageBodyMember]
        public string DddDevedor { get; set; }

        [XmlElement(ElementName = "numTelDevedor "), MessageBodyMember]
        public string NumTelDevedor { get; set; }

        [XmlElement(ElementName = "numRegContrato"), MessageBodyMember]
        public string NumRegContrato { get; set; }

        [XmlElement(ElementName = "numRegAditivo"), MessageBodyMember]
        public string NumRegAditivo { get; set; }

        [XmlElement(ElementName = "indicativoPenalidade"), MessageBodyMember]
        public string IndicativoPenalidade { get; set; }

        [XmlElement(ElementName = "penalidade"), MessageBodyMember]
        public string Penalidade { get; set; }

        [XmlElement(ElementName = "indicativoComissao"), MessageBodyMember]
        public string IndicativoComissao { get; set; }

        [XmlElement(ElementName = "comissao"), MessageBodyMember]
        public string Comissao { get; set; }

        [XmlElement(ElementName = "assinaturaEletronica"), MessageBodyMember]
        public string AssinaturaEletronica { get; set; }

    }
}