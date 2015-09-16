using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Entidades
{
    public class FormaPagamento
    {
        public FormaPagamento()
        {
            Parcelas = 1;
        }
        [XmlElement("bandeira")]
        public string BandeiraString { get { return IdentificaBandeira(Bandeira); } set { Bandeira = IdentificaBandeira(value); } }
        [XmlElement("produto")]
        public string ProdutoString { get { return IdentificaProduto(Produto); } set { Produto = IdentificaProduto(value); } }
        [XmlIgnore]
        public Bandeiras Bandeira { get; set; }
        [XmlIgnore]
        public ProdutoCielo Produto { get; set; }
        [XmlElement("parcelas")]
        public int Parcelas { get; set; }

        public bool Validar()
        { 
            if (Bandeira == Bandeiras.Discovery)
                Produto = ProdutoCielo.Credito_a_vista;

            if (Produto == ProdutoCielo.Debito && Bandeira != Bandeiras.Visa && Bandeira != Bandeiras.Mastercard)
                return false;

            if (Produto == ProdutoCielo.Credito_a_vista || Produto == ProdutoCielo.Debito) 
                Parcelas = 1;

            return true;
        }

        private Bandeiras IdentificaBandeira(string type)
        {
            switch (type)
            {
                case "visa":
                    return Bandeiras.Visa;
                case "mastercard":
                    return Bandeiras.Mastercard;
                case "amex":
                    return Bandeiras.AmericanExpress;
                case "elo":
                    return Bandeiras.Elo;
                case "diners":
                    return Bandeiras.Diners;
                case "discover":
                    return Bandeiras.Discovery;
                case "jcb":
                    return Bandeiras.JCB;
                case "aura":
                    return Bandeiras.Aura;
            }
            return default(Bandeiras);
        }
        private string IdentificaBandeira(Bandeiras type)
        {
            switch (type)
            {
                case Bandeiras.Visa:
                    return "visa";
                case Bandeiras.Mastercard:
                    return "mastercard";
                case Bandeiras.AmericanExpress:
                    return "amex";
                case Bandeiras.Elo:
                    return "elo";
                case Bandeiras.Diners:
                    return "diners";
                case Bandeiras.Discovery:
                    return "discover";
                case Bandeiras.JCB:
                    return "jcb";
                case Bandeiras.Aura:
                    return "aura";
                default:
                    return string.Empty;
            }
        }

        private string IdentificaProduto(ProdutoCielo product)
        {
            switch (product)
            {
                case ProdutoCielo.Credito_a_vista:
                    return "1";
                case ProdutoCielo.Credito_parcelado_loja:
                    return "2";
                case ProdutoCielo.Credito_parcelado_emissor:
                    return "3";
                case ProdutoCielo.Debito:
                    return "A";
                default:
                    return null;
            }
        }

        private ProdutoCielo IdentificaProduto(string product)
        {
            switch (product)
            {
                case "1":
                    return ProdutoCielo.Credito_a_vista;
                case "2":
                    return ProdutoCielo.Credito_parcelado_loja;
                case "3":
                    return ProdutoCielo.Credito_parcelado_emissor;
                case "A":
                    return ProdutoCielo.Debito;
                default:
                    return default(ProdutoCielo);
            }
        }
    }
}
