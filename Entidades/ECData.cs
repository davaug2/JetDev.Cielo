using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    public class ECData
    {
        [XmlElement("numero")]
        public string Number { get; set; }
        [XmlElement("chave")]
        public string Key { get; set; }
    }
}
