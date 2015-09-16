using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Respostas
{
    [XmlRoot("transacao")]
    public class RespostaTransacao : RespostaBase
    {
        [XmlAttribute("versao")]
        public string Versao { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("url-autenticacao")]
        public string UrlAutenticacao { get; set; }

        [XmlElement("tid")]
        public string TransacaoId { get; set; }

        [XmlElement("pan")]
        public string PAN { get; set; }

        [XmlElement("status")]
        public string SituacaoString { get; set; }

        public SituacaoTransacao Situacao
        {
            get
            {
                int intCast;
                return int.TryParse(SituacaoString, out intCast) ? (SituacaoTransacao)intCast : default(SituacaoTransacao);
            }
            set
            {
                SituacaoString = ((int)value).ToString();
            }
        }

        [XmlElement("dados-pedido")]
        public Pedido Pedido { get; set; }

        [XmlElement("forma-pagamento")]
        public FormaPagamento FormaPagamento { get; set; }

        [XmlElement("autorizacao")]
        public Autorizacao Autorizacao { get; set; }

        [XmlElement("captura")]
        public Captura Captura { get; set; }

        [XmlArray("cancelamentos")]
        [XmlArrayItem("cancelamento", typeof(Cancelamento), IsNullable = false)]
        public List<Cancelamento> Cancelamentos { get; set; }
    }
}
