using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    public class Captura
    {
        [XmlElement("codigo")]
        public string Codigo { get; set; }
        [XmlElement("mensagem")]
        public string Mensagem { get; set; }
        [XmlIgnore]
        public DateTime DataHora { get; set; }
        [XmlIgnore]
        public decimal Valor { get; set; }
        [XmlElement("valor")]
        public string ValorString
        {
            get { return Utils.ConverteValorParaCielo(Valor); }
            set { Valor = Utils.ConverteValorDeCielo(value); }
        }


        [XmlElement("data-hora")]
        public string DataHoraString
        {
            get { return Utils.ConverteDataParaCielo(DataHora); }
            set { DataHora = Utils.ConverteDataDeCielo(value); }
        }
    }
}
