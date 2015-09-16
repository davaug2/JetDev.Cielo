using System;
using System.Collections.Generic;
using System.Text;

namespace JetDev.Cielo
{
    public class ExcecaoPagamento : Exception
    {
        public ExcecaoPagamento()
        { }

        public Entidades.Erro Detalhe { get; set; }
        public string XmlRequisicao { get; set; }
        public string XmlResposta { get; set; }

        public ExcecaoPagamento(Entidades.Erro detalhe, string xmlRequisicao, string xmlResposta)
            : base(detalhe.Mensagem)
        {
            this.Detalhe = detalhe;
            this.XmlRequisicao = xmlRequisicao;
            this.XmlResposta = xmlResposta;
        }
    }
}
