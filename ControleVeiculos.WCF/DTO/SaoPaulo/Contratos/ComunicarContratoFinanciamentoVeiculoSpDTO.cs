using Lean.Test.Cloud.WCF.DTO.SaoPaulo.Autenticacao;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Lean.Test.Cloud.WCF.DTO.SaoPaulo.Contratos
{
    [MessageContract(WrapperName = "comunicarContratoFinanVeiculo")]
    public class ComunicarContratoFinanciamentoVeiculoSpDTO
    {
        [XmlElement(Namespace = "", ElementName = "contratoFinanVeiculo")]
        [MessageBodyMember]
        public ContratoFinanciamentoVeiculoSpDTO ContratoFinanciamentoVeiculo { get; set; }

        [XmlElement(Namespace = "", ElementName = "autenticaEmpresa")]
        [MessageBodyMember]
        public AutenticaEmpresaSpDTO AutenticaEmpresa { get; set; }
    }
}