using JetDev.Cielo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JetDev.Cielo.Respostas
{
    [XmlRoot("transacao")]
    public class RespostaTransacaoSimples : RespostaBase
    {
        [XmlAttribute("versao")]
        public string Versao { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("url-autenticacao")]
        public string UrlAutenticacao { get; set; }
        
        [XmlElement("tid")]
        public string TransacaoId { get; set; }

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
    }
}
