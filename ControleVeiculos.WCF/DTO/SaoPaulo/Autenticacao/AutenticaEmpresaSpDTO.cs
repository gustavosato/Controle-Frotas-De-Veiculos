using System.ServiceModel;
using System.Xml.Serialization;

namespace ControleVeiculos.WCF.DTO.SaoPaulo.Autenticacao
{
    [MessageContract(WrapperName = "autenticaEmpresa")]
    public class AutenticaEmpresaSpDTO
    {
        [XmlElement(Namespace = "", ElementName = "cnpj"), MessageBodyMember]
        public string Cnpj { get; set; }
        [XmlElement(Namespace = "", ElementName = "senhaWebService"), MessageBodyMember]
        public string SenhaWebService { get; set; }
    }
}