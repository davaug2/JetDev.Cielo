using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    public class PortadorToken
    {
        public PortadorToken()
        {
        }

        public PortadorToken(string token)
        {
            this.Token = token;
        }

        [XmlElement("token")]
        public string Token { get; set; }
    }
}
