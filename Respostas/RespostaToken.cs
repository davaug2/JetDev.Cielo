using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Respostas
{
    [XmlRoot("retorno-token")]
    public class RespostaToken : RespostaBase
    {
        public RespostaToken()
        {
        }

        [XmlElement("token")]
        public Token Token { get; set; }
    }
}
