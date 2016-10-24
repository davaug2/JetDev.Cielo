using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    public class Autorizacao
    {
        [XmlElement("codigo")]
        public string Codigo { get; set; }
        [XmlElement("mensagem")]
        public string Mensagem { get; set; }
        [XmlElement("dataHora")]
        public string DataHora { get; set; }
        [XmlElement("valor")]
        public string ValorString
        {
            get { return Utils.ConverteValorParaCielo(Valor); }
            set { Valor = Utils.ConverteValorDeCielo(value); }
        }
        [XmlElement("arp")]
        public string ARP { get; set; }
        [XmlElement("lr")]
        public string LR { get; set; }
        [XmlElement("nsu")]
        public string NSU { get; set; }
        [XmlIgnore]
        public decimal Valor { get; set; }

    }
}
