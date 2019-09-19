using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using System.Xml.Serialization;

namespace Lean.Test.Cloud.WCF.DTO.SaoPaulo.Gravames
{
    [MessageContract(WrapperNamespace = "http://ws.sircof.gever.detran.prodesp.sp.gov.br/", WrapperName = "consultarGravameResponse")]
    public class ConsultaGravameResponseSpDTO
    {
        [XmlElement(ElementName = "RetornoConsultaGravame", Namespace = ""), MessageBodyMember]
        public RetornoConsultaGravameSpDTO RetornoConsultaGravame { get; set; }
    }

    [MessageContract(WrapperNamespace = "")]
    public class RetornoConsultaGravameSpDTO
    {
        [XmlElement(ElementName = "codigo"), MessageBodyMember]
        public int Codigo { get; set; }

        //[DataMember(EmitDefaultValue = false)]
        [XmlElement(ElementName = "descricao", IsNullable = false), MessageBodyMember]
        public string Descricao { get; set; }

        //[DataMember(EmitDefaultValue = false)]
        [XmlElement(ElementName = "detalheGravame", IsNullable = false), MessageBodyMember]
        public DetalheGravameSpDTO DetalheGravame { get; set; }
    }
}