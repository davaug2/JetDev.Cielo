using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Requisicoes
{
    [XmlRoot("requisicao-token")]
    public class RequisicaoToken : RequisicaoBase
    {
        public RequisicaoToken()
        {
            Id = Guid.NewGuid().ToString();
            Versao = "1.2.1";
        }

        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("versao")]
        public string Versao { get; set; }

        [XmlElement("dados-ec")]
        public ECData EC { get; set; }
        [XmlElement("dados-portador")]
        public RequisitarToken Portador { get; set; }
    }
}
