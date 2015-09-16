using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    public class Pedido
    {
        public Pedido()
        {
            CodigoMoeda = "986";
            DataHora = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            Idioma = "PT";
        }
        [XmlElement("numero")]
        public string Numero { get; set; }
        [XmlElement("valor")]
        public string ValorString
        {
            get { return Utils.ConverteValorParaCielo(Valor); }
            set { Valor = Utils.ConverteValorDeCielo(value); }
        }
        [XmlIgnore]
        public decimal Valor { get; set; }
        [XmlElement("moeda")]
        public string CodigoMoeda { get; set; }
        [XmlElement("data-hora")]
        public string DataHora { get; set; }
        [XmlElement("descricao")]
        public string Descricao { get; set; }
        [XmlElement("idioma")]
        public string Idioma { get; set; }
        [XmlElement("soft-descriptor")]
        public string DescricaoExtra { get; set; }
        
    }
}
