using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    public class Portador
    {
        public Portador()
        {
            Indicator = "1";
        }
        [XmlIgnore]
        public string Token { get; set; }
        [XmlIgnore]
        public string CNPJCPF { get; set; }
        [XmlIgnore]
        public string TipoPessoa { get; set; }
        [XmlElement("numero")]
        public string NumeroCartao { get; set; }

        [XmlElement("validade")]
        public string ValidadeCartaoString
        {
            get { return ValidadeCartao.ToString("yyyyMM"); }
            set { ValidadeCartao = TransformaValidadeCartao(value); }
        }
        [XmlElement("nome-portador")]
        public string Nome { get; set; }
        [XmlIgnore]
        public string CodigoSeguranca { get; set; }
        [XmlIgnore]
        public string Indicator { get; set; }
        [XmlIgnore]
        public DateTime ValidadeCartao { get; set; }

        private DateTime TransformaValidadeCartao(string cardValidateString)
        { 
            int year; int month;
            if (!string.IsNullOrEmpty(cardValidateString)
                && cardValidateString.Length >= 6
                && int.TryParse(cardValidateString.Substring(0, 4), out year)
                && int.TryParse(cardValidateString.Substring(4, 2), out month))
                return new DateTime(year, month, 0);
            else
                return DateTime.MinValue;
            
        }
    }
}
