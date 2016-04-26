using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JetDev.Cielo.Entidades.Checkout
{
    public class Carrinho
    {
        [JsonProperty(PropertyName = "Discount")]
        public Desconto Desconto { get; set; }
        [JsonProperty(PropertyName = "Items")]
        public List<Item> Itens { get; set; }
    }

    public class Consumidor
    {
        [JsonProperty(PropertyName = "Identity")]
        public string Documento { get; set; }
        [JsonProperty(PropertyName = "FullName")]
        public string Nome { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "Phone")]
        public string Telefone { get; set; }
    }

    public class Frete
    {
        [JsonIgnore]
        public TipoFrete Tipo { get; set; }
        [JsonProperty(PropertyName = "Type")]
        public string TipoString
        {
            get
            {
                switch (Tipo)
                {
                    case TipoFrete.Correios: return "Correios";
                    case TipoFrete.Gratis: return "Free";
                    case TipoFrete.ValorFixo: return "FixedAmount";
                    case TipoFrete.RetiradaLoja: return "WithoutShippingPickUp";
                    case TipoFrete.SemFrete: return "WithoutShipping";
                }
                return null;
            }
            set
            {
                if (value == "Correios") Tipo = TipoFrete.Correios;
                else if (value == "Free") Tipo = TipoFrete.Gratis;
                else if (value == "FixedAmount") Tipo = TipoFrete.ValorFixo;
                else if (value == "WithoutShippingPickUp") Tipo = TipoFrete.RetiradaLoja;
                else if (value == "WithoutShipping") Tipo = TipoFrete.SemFrete;
            }
        }
    }

    public class Desconto
    {
        [JsonIgnore]
        public TipoDesconto Tipo { get; set; }
        [JsonIgnore]
        public decimal Valor { get; set; }
        [JsonProperty(PropertyName = "Type")]
        public string TipoString
        {
            get
            {
                switch (Tipo)
                {
                    case TipoDesconto.Percentual: return "Percent";
                    case TipoDesconto.Valor: return "Amount";
                }
                return null;
            }
            set
            {
                if (value == "Percent")
                    Tipo = TipoDesconto.Percentual;
                else if (value == "Amount")
                    Tipo = TipoDesconto.Valor;
            }
        }
        [JsonProperty(PropertyName = "Value")]
        public string ValorString
        {
            get { return Utils.ConverteValorParaCielo(this.Valor); }
            set { this.Valor = Utils.ConverteValorDeCielo(value); }
        }
    }
    public class Item
    {
        [JsonProperty(PropertyName = "Name")]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Descricao { get; set; }

        [JsonIgnore]
        public decimal ValorUnitario { get; set; }

        [JsonProperty(PropertyName = "UnitPrice")]
        public string ValorUnitarioString
        {
            get { return Utils.ConverteValorParaCielo(this.ValorUnitario); }
            set { this.ValorUnitario = Utils.ConverteValorDeCielo(value); }
        }

        [JsonProperty(PropertyName = "Quantity")]
        public int Quantidade { get; set; }

        [JsonIgnore]
        public TipoItem Tipo { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string TipoString
        {
            get
            {
                switch (Tipo)
                {
                    case TipoItem.MaterialFisico: return "Asset";
                    case TipoItem.ProdutosDigitais: return "Digital";
                    case TipoItem.Servicos: return "Service";
                    case TipoItem.OutrosPagamentos: return "Payment";
                }
                return null;
            }
            set
            {
                if (value == "Asset") this.Tipo = TipoItem.MaterialFisico;
                else if (value == "Digital") this.Tipo = TipoItem.ProdutosDigitais;
                else if (value == "Service") this.Tipo = TipoItem.Servicos;
                else if (value == "Payment") this.Tipo = TipoItem.OutrosPagamentos;
            }
        }

        [JsonProperty(PropertyName = "Sku")]
        public string Sku { get; set; }

        [JsonIgnore]
        public decimal Peso { get; set; }

        [JsonProperty(PropertyName = "Weight")]
        public string PesoString
        {
            get { return this.Peso > 0 ? Utils.ConverteValorParaCielo(this.Peso) : null; }
            set { this.Peso = Utils.ConverteValorDeCielo(value); }
        }
    }

    public class ConfiguracaoTransacao
    {
        [JsonProperty("CheckoutUrl")]
        public string Url { get; set; }

        [JsonProperty("Profile")]
        public string Perfil { get; set; }

        [JsonProperty("Version")]
        public string Versao { get; set; }
    }

    public class AtualizacaoStatus
    {
        [JsonProperty("checkout_cielo_order_number")]
        public string TransacaoId { get; set; }
        [JsonIgnore]
        public decimal Valor { get; set; }
        [JsonProperty("order_number")]
        public string NumeroPedido { get; set; }
        [JsonProperty("payment_status")]
        public SituacaoPagamentoCheckout Situacao { get; set; }

        [JsonProperty("amount")]
        public string ValorString
        {
            get { return Utils.ConverteValorParaCielo(this.Valor); }
            set { this.Valor = Utils.ConverteValorDeCielo(value); }
        }
    }

    public class NotificacaoTransacao : AtualizacaoStatus
    {
        [JsonProperty("amount")]
        public string ValorString
        {
            get { return Utils.ConverteValorParaCielo(this.Valor); }
            set { this.Valor = Utils.ConverteValorDeCielo(value); }
        }


        [JsonProperty("created_date")]
        public DateTime DataCriacao { get; set; }
        [JsonProperty("customer_name")]
        public string NomeConsumidor { get; set; }
        [JsonProperty("customer_phone")]
        public string TelefoneConsumidor { get; set; }
        [JsonProperty("customer_identity")]
        public string DocumentoConsumidor { get; set; }
        [JsonProperty("customer_email")]
        public string EmailConsumidor { get; set; }


        [JsonProperty("shipping_type")]
        public string TipoFrete { get; set; }
        [JsonIgnore]
        public decimal ValorFrete { get; set; }
        [JsonProperty("shipping_price")]
        public string ValorFreteString
        {
            get { return Utils.ConverteValorParaCielo(this.ValorFrete); }
            set { this.ValorFrete = Utils.ConverteValorDeCielo(value); }
        }


        [JsonProperty("payment_method_type")]
        public string MetodoPagamento { get; set; }
        [JsonProperty("payment_method_brand")]
        public string Bandeira { get; set; }
        [JsonProperty("payment_maskedcreditcard")]
        public string NumeroCartaoCredito { get; set; }
        [JsonProperty("payment_installments")]
        public string NumeroParcelas { get; set; }
    }
}
