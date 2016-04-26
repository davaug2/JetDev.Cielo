using JetDev.Cielo.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Requisicoes
{
    public class RequisicaoBase
    {
        [JsonIgnore]
        public string XMLRequisicao { get; set; }
        [JsonIgnore]
        public string XMLResposta { get; set; }
    }
}
