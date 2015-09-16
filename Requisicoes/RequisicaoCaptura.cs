using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Requisicoes
{
    [XmlRoot("requisicao-captura")]
    public class RequisicaoCaptura : RequisicaoBase
    {
        public RequisicaoCaptura()
        {
            Id = Guid.NewGuid().ToString();
            Versao = "1.2.1";
        }

        public RequisicaoCaptura(string transacaoId, ECData ec) : this()
        {
            this.transacaoID = transacaoId;
            this.EC = ec;
        }
        
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("versao")]
        public string Versao { get; set; }
        [XmlElement("tid")]
        public string transacaoID { get; set; }
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
