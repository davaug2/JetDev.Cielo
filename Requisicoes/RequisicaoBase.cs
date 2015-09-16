using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Requisicoes
{
    public class RequisicaoBase
    {
        public string XMLRequisicao { get; set; }
        public string XMLResposta { get; set; }
    }
}
