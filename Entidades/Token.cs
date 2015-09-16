using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    public class Token
    {
        public Token()
        {
        }

        [XmlElement("dados-token")]
        public TokenDados Dados { get; set; }
    }
}
