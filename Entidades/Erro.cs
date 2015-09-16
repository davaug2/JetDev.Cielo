using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    [XmlRoot("erro")]
    public class Erro
    {
        [XmlElement("codigo")]
        public string Codigo { get; set; }
        [XmlElement("mensagem")]
        public string Mensagem { get; set; }
    }

}
