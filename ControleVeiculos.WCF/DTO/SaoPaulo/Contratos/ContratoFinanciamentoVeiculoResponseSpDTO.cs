using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Xml.Serialization;

namespace Lean.Test.Cloud.WCF.DTO.SaoPaulo.Contratos
{
    [MessageContract(WrapperNamespace = "", WrapperName = "ContratoFinanciamentoVeiculoResponse")]
    public class ContratoFinanciamentoVeiculoResponseSpDTO
    {
        [XmlElement(ElementName = "retorno"), MessageBodyMember]
        public ContratoFinanciamentoVeiculoRetornoSpDTO Retorno { get; set; }
    }

    [MessageContract(WrapperNamespace = "")]
    public class ContratoFinanciamentoVeiculoRetornoSpDTO
    {
        [XmlElement(ElementName = "codigo"), MessageBodyMember]
        public int Codigo { get; set; }
        [XmlElement(ElementName = "descricao"), MessageBodyMember]
        public string Descricao { get; set; }
        [XmlElement(ElementName = "flagTransacao"), MessageBodyMember]
        public int FlagTransacao { get; set; }
        [XmlElement(ElementName = "chassi"), MessageBodyMember]
        public string Chassi { get; set; }
    }
}