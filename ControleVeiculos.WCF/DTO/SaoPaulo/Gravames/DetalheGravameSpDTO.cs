using System.ServiceModel;
using System.Xml.Serialization;

namespace Lean.Test.Cloud.WCF.DTO.SaoPaulo.Gravames
{
    [MessageContract(WrapperNamespace = "")]
    public class DetalheGravameSpDTO
    {
        [XmlElement, MessageBodyMember]
        public string anoFabricacao { get; set; }
        [XmlElement, MessageBodyMember]
        public string anoModelo { get; set; }
        [XmlElement, MessageBodyMember]
        public string chassi { get; set; }
        [XmlElement, MessageBodyMember]
        public string idenRemarcacao { get; set; }
        [XmlElement, MessageBodyMember]
        public string cnpjAgente { get; set; }
        [XmlElement, MessageBodyMember]
        public string codigoAgente { get; set; }
        [XmlElement, MessageBodyMember]
        public string dataCancelamento { get; set; }
        [XmlElement, MessageBodyMember]
        public string dataVigenciaContrato { get; set; }
        [XmlElement, MessageBodyMember]
        public string dataEmissao { get; set; }
        [XmlElement, MessageBodyMember]
        public string dataQuitacao { get; set; }
        [XmlElement, MessageBodyMember]
        public string nomeFinanciado { get; set; }
        [XmlElement, MessageBodyMember]
        public string numeroContrato { get; set; }
        [XmlElement, MessageBodyMember]
        public string numeroDocumentoFinanciado { get; set; }
        [XmlElement, MessageBodyMember]
        public string numeroGravame { get; set; }
        [XmlElement, MessageBodyMember]
        public string placa { get; set; }
        [XmlElement, MessageBodyMember]
        public string renavam { get; set; }
        [XmlElement, MessageBodyMember]
        public string situacao { get; set; }
        [XmlElement, MessageBodyMember]
        public string tipoDocumentoFinanciado { get; set; }
        [XmlElement, MessageBodyMember]
        public string tipoGravame { get; set; }
        [XmlElement, MessageBodyMember]
        public string uFPlaca { get; set; }
        [XmlElement, MessageBodyMember]
        public string dataInclusaoGravame { get; set; }
        [XmlElement, MessageBodyMember]
        public string flagContrato { get; set; }
    }
}