using Lean.Test.Cloud.WCF.DTO.SaoPaulo.Autenticacao;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Lean.Test.Cloud.WCF.DTO.SaoPaulo.Gravames
{
    [MessageContract(WrapperName = "consultarGravame")]
    public class ConsultarGravameSpDTO
    {
        [XmlElement(Namespace = "", ElementName = "consultaGravame"), MessageBodyMember]
        public ConsultaGravameSpDTO ConsultaGravame { get; set; }
        [XmlElement(Namespace = "", ElementName = "autenticaEmpresa"), MessageBodyMember]
        public AutenticaEmpresaSpDTO AutenticaEmpresa { get; set; }
    }
}