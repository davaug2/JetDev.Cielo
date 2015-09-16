using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Requisicoes
{
    [XmlRoot("requisicao-cancelamento")]
    public class RequisicaoCancelamento : RequisicaoBase
    {
        public RequisicaoCancelamento()
        {
            Id = Guid.NewGuid().ToString();
            Versao = "1.2.1";
        }

        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlElement("tid")]
        public string transacaoId { get; set; }
        [XmlAttribute("versao")]
        public string Versao { get; set; }
        [XmlElement("dados-ec")]
        public ECData EC { get; set; }
        [XmlElement("valor")]
        public string ValorString
        {
            get { return Utils.ConverteValorParaCielo(Valor); }
            set { Valor = Utils.ConverteValorDeCielo(value); }
        }
        [XmlIgnore]
        public decimal Valor { get; set; }

    }
}
