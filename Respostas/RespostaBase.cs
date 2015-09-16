using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Respostas
{
    public class RespostaBase
    {
        public string XMLRequisicao { get; set; }
        public string XMLResposta { get; set; }
    }
}
