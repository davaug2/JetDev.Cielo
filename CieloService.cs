using JetDev.Cielo.Entidades;
using JetDev.Cielo.Requisicoes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace JetDev.Cielo
{
    public static class ServicoCielo
    {
        public static TokenDados ObterTokenCartao(Requisicoes.RequisicaoToken requiscaoToken)
        {
            return ObterTokenCartao(requiscaoToken, Config.Ambiente);
        }
        public static TokenDados ObterTokenCartao(Requisicoes.RequisicaoToken requiscaoToken, JetDev.Cielo.Entidades.Ambiente ambiente)
        {
            if (requiscaoToken.EC == null)
                requiscaoToken.EC = ObterECData(ambiente);

            var resposta = Utils.Requisitar<Respostas.RespostaToken, Requisicoes.RequisicaoToken>(requiscaoToken, ambiente);
            Config.CallLogAction(null, resposta.XMLRequisicao, resposta.XMLResposta);
            return resposta != null && resposta.Token != null ? resposta.Token.Dados : null;
        }
        public static Respostas.RespostaTransacao AutorizarPagamento(Requisicoes.RequisicaoTransacaoCartao transacao)
        {
            return AutorizarPagamento(transacao, Config.Ambiente);
        }
        public static Respostas.RespostaTransacao AutorizarPagamento(Requisicoes.RequisicaoTransacaoCartao transacao, JetDev.Cielo.Entidades.Ambiente ambiente)
        {
            if (!transacao.FormaPagamento.Validar())
                throw new Exception("Requisição inválida");
            if (transacao.EC == null)
                transacao.EC = ObterECData(ambiente);
            var resposta = Utils.Requisitar<Respostas.RespostaTransacao, Requisicoes.RequisicaoTransacaoCartao>(transacao, ambiente);

            Config.CallLogAction(resposta.TransacaoId, resposta.XMLRequisicao, resposta.XMLResposta);

            return resposta;
        }
        public static Respostas.RespostaTransacao AutorizarPagamento(Requisicoes.RequisicaoTransacao transacao)
        {
            return AutorizarPagamento(transacao, Config.Ambiente);
        }
        public static Respostas.RespostaTransacao AutorizarPagamento(Requisicoes.RequisicaoTransacao transacao, JetDev.Cielo.Entidades.Ambiente ambiente)
        {
            if (!transacao.FormaPagamento.Validar())
                throw new Exception("Requisição inválida");
            if (transacao.EC == null)
                transacao.EC = ObterECData(ambiente);
            var resposta = Utils.Requisitar<Respostas.RespostaTransacao, Requisicoes.RequisicaoTransacao>(transacao, ambiente);

            Config.CallLogAction(resposta.TransacaoId, resposta.XMLRequisicao, resposta.XMLResposta);

            return resposta;
        }
        public static Respostas.RespostaTransacao Capturar(string transacaoId)
        {
            return Capturar(transacaoId, null);
        }
        public static Respostas.RespostaTransacao Capturar(string transacaoId, decimal? value)
        {
            if (!value.HasValue)
            {
                var status = ObterSituacao(transacaoId);
                if (status == null)
                    throw new Exception("Transação não encontrada");

                var autorizantionValue = status != null ? status.Autorizacao.Valor : 0;
                var capturedValues = status.Captura != null ? status.Captura.Valor : 0;

                value = autorizantionValue - capturedValues;

                if (value.Value <= 0)
                    throw new Exception("Valor não disponível para esta transação");
            }



            var taken = new RequisicaoCaptura(transacaoId, ObterECData());
            var resposta = Utils.Requisitar<Respostas.RespostaTransacao, Requisicoes.RequisicaoCaptura>(taken);

            Config.CallLogAction(resposta.TransacaoId, resposta.XMLRequisicao, resposta.XMLResposta);

            return resposta;
        }

        public static Respostas.RespostaTransacao CancelarTransacao(string transacaoId)
        {
            return CancelarTransacao(new Requisicoes.RequisicaoCancelamento() { transacaoId = transacaoId });
        }
        public static Respostas.RespostaTransacao CancelarTransacao(Requisicoes.RequisicaoCancelamento transacao)
        {
            if (transacao.Valor == 0)
                transacao.Valor = ObterSituacao(transacao.transacaoId).Pedido.Valor;

            if (transacao.EC == null)
                transacao.EC = ObterECData();

            var resposta = Utils.Requisitar<Respostas.RespostaTransacao, Requisicoes.RequisicaoCancelamento>(transacao);
            Config.CallLogAction(resposta.TransacaoId, resposta.XMLRequisicao, resposta.XMLResposta);
            return resposta;
        }
        public static Respostas.RespostaTransacaoSimples ObterUrlAutenticacao(Requisicoes.RequisicaoTransacao transacao)
        {
            return ObterUrlAutenticacao(transacao, Config.Ambiente);
        }
        public static Respostas.RespostaTransacaoSimples ObterUrlAutenticacao(Requisicoes.RequisicaoTransacao transacao, JetDev.Cielo.Entidades.Ambiente ambiente)
        {
            if (!transacao.FormaPagamento.Validar())
                throw new Exception("Requisição inválida");
            if (transacao.EC == null)
                transacao.EC = ObterECData(ambiente);

            var resposta = Utils.Requisitar<Respostas.RespostaTransacaoSimples, Requisicoes.RequisicaoTransacao>(transacao, ambiente);
            Config.CallLogAction(resposta.TransacaoId, resposta.XMLRequisicao, resposta.XMLResposta);
            return resposta;
        }
        public static Respostas.RespostaTransacao ObterSituacao(string transacaoId)
        {
            return ObterSituacao(new Requisicoes.RequisicaoConsulta(transacaoId), Config.Ambiente);
        }
        public static Respostas.RespostaTransacao ObterSituacao(string transacaoId, JetDev.Cielo.Entidades.Ambiente ambiente)
        {
            return ObterSituacao(new Requisicoes.RequisicaoConsulta(transacaoId), ambiente);
        }
        public static Respostas.RespostaTransacao ObterSituacao(Requisicoes.RequisicaoConsulta selectRequest)
        {
            return ObterSituacao(selectRequest, Config.Ambiente);
        }
        public static Respostas.RespostaTransacao ObterSituacao(Requisicoes.RequisicaoConsulta consulta, JetDev.Cielo.Entidades.Ambiente ambiente)
        {
            if (string.IsNullOrEmpty(consulta.TransacaoId))
                throw new Exception("ID da transação é obrigatório");
            if (consulta.EC == null)
                consulta.EC = ObterECData(ambiente);

            var resposta = Utils.Requisitar<Respostas.RespostaTransacao, Requisicoes.RequisicaoConsulta>(consulta, ambiente);
            Config.CallLogAction(resposta.TransacaoId, resposta.XMLRequisicao, resposta.XMLResposta);
            return resposta;
        }
        private static ECData ObterECData()
        {
            return ObterECData(Config.Ambiente);
        }
        private static ECData ObterECData(Entidades.Ambiente enviroment)
        {
            string chave = !string.IsNullOrEmpty(Config.ChaveEstabelecimento) ? Config.ChaveEstabelecimento.ToString() : string.Empty;
            string numero = !string.IsNullOrEmpty(Config.NumeroEstabelecimento) ? Config.NumeroEstabelecimento.ToString() : string.Empty;

            if (enviroment == Entidades.Ambiente.TesteCieloIntegrado)
            {
                chave = "e84827130b9837473681c2787007da5914d6359947015a5cdb2b8843db0fa832";
                numero = "1001734898";
            }
            else if (enviroment == Entidades.Ambiente.TesteWebService)
            {
                chave = "25fbb99741c739dd84d7b06ec78c9bac718838630f30b112d033ce2e621b34f3";
                numero = "1006993069";
            }
            return new ECData() { Key = chave, Number = numero };
        }
    }
}
