using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    public class TokenDados
    {
        public TokenDados()
        {
        }

        [XmlElement("codigo-token")]
        public string Token { get; set; }

        [XmlElement("status")]
        public string SituacaoString { get; set; }

        [XmlIgnore]
        public SituacaoToken Status { get { return (SituacaoToken)int.Parse(SituacaoString); } set { SituacaoString = ((int)value).ToString(); } }

        [XmlElement("numero-cartao-truncado")]
        public string NumeroCartaoTruncado { get; set; }
    }
}
