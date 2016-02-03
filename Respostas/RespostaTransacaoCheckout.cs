using JetDev.Cielo.Entidades;
using JetDev.Cielo.Entidades.Checkout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Respostas
{
    public class RespostaTransacaoCheckout : RespostaBase
    {
        [JsonProperty("Settings")]
        public ConfiguracaoTransacao Configuracao { get; set; }

        [JsonProperty("Message")]
        public string Mensagem { get; set; }
    }
}