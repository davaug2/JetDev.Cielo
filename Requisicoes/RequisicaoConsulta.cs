using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Requisicoes
{
    [XmlRoot("requisicao-consulta")]
    public class RequisicaoConsulta : RequisicaoBase
    {
        public RequisicaoConsulta()
        {
            Id = Guid.NewGuid().ToString();
            Versao = "1.2.1";
        }
        public RequisicaoConsulta(string transacaoId) : this()
        {
            TransacaoId = transacaoId;
        }

        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("versao")]
        public string Versao { get; set; }
        [XmlElement("tid")]
        public string TransacaoId { get; set; }
        [XmlElement("dados-ec")]
        public ECData EC { get; set; }
    }
}
