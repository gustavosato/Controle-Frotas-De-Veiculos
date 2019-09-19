using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Lean.Test.Cloud.WCF.DTO.SaoPaulo.Gravames
{
    [MessageContract(WrapperNamespace = "")]
    public class ConsultaGravameSpDTO
    {
        [XmlElement(ElementName = "chassi"), MessageBodyMember]
        public string Chassi { get; set; }

        [XmlElement(ElementName = "placa"), MessageBodyMember]
        public string Placa { get; set; }

        [XmlElement(ElementName = "tipoPesquisa", IsNullable = false, Type =(typeof(int))), MessageBodyMember]
        [Range(1, 2, ErrorMessage = "O tipo da pesquisa deve ser 1 [Chassi] ou 2 [Placa]")]
        public int TipoPesquisa { get; set; }
    }
}