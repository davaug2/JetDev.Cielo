using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Requisicoes
{
    [XmlRoot("requisicao-transacao")]
    public class RequisicaoTransacao : RequisicaoBase
    {
        public RequisicaoTransacao()
        {
            Id = Guid.NewGuid().ToString();
            Versao = "1.2.1";
            Capturar = true;
        }

        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("versao")]
        public string Versao { get; set; }
        [XmlElement("dados-ec")]
        public ECData EC { get; set; }
        [XmlElement("dados-portador")]
        public PortadorToken Portador { get; set; }
        [XmlElement("dados-pedido")]
        public Pedido Pedido { get; set; }
        [XmlElement("forma-pagamento")]
        public FormaPagamento FormaPagamento { get; set; }
        [XmlElement("url-retorno")]
        public string UrlRetorno { get; set; }
        [XmlElement("autorizar")]
        public int AuthorizeInt { get { return (int)Autorizacao; } set { Autorizacao = (TipoAutorizacao)value; } }
        [XmlIgnore]
        public TipoAutorizacao Autorizacao { get; set; }
        [XmlIgnore]
        public bool Capturar { get; set; }

        [XmlElement("capturar")] 
        public string CapturarString { get { return Capturar ? "true" : "false"; } set { Capturar = value == "true"; } }
    }
}
