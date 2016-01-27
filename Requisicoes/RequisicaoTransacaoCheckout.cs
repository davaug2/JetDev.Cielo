using JetDev.Cielo.Entidades;
using JetDev.Cielo.Entidades.Checkout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Requisicoes
{
    public class RequisicaoTransacaoCheckout : RequisicaoBase
    {
        public RequisicaoTransacaoCheckout()
        {
        }

        [JsonProperty(PropertyName = "OrderNumber")]
        public string NumeroPedido { get; set; }


        [JsonProperty(PropertyName = "SoftDescriptor")]
        public string TextoFatura { get; set; }


        [JsonProperty(PropertyName = "Cart")]
        public Carrinho Carrinho { get; set; }

        [JsonProperty(PropertyName = "Shipping")]
        public Frete Frete { get; set; }

        [JsonProperty(PropertyName = "Customer")]
        public Consumidor Consumidor { get; set; }       
    }
}
