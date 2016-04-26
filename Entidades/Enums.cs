using System;
using System.Collections.Generic;
using System.Text;

namespace JetDev.Cielo.Entidades
{
    public enum Ambiente
    {
        TesteWebService,
        Producao,
        ProducaoCieloCheckout,
        TesteCieloIntegrado
    }
    public enum Bandeiras
    {
        Visa, 
        Mastercard,
        AmericanExpress,
        Elo,
        Diners,
        Discovery,
        JCB,
        Aura
    }
    public enum ProdutoCielo
    {
        Credito_a_vista = 1,
        Credito_parcelado_loja = 2,
        Credito_parcelado_emissor = 3,
        Debito = 'A'
    }
    public enum TipoAutorizacao
    { 
        Nao_Autorizar = 0,
        Autorizar_somente_se_autenticada = 1,
        Autorizar_autenticada_e_nao_autenticada = 2,
        Autorizar_sem_autenticacao = 3,
        TransacaoRecorrente= 4
    }

    public enum SituacaoTransacao
    {
        Criada = 0,
        EmProcessamento = 1,
        Autenticada = 2,
        NaoAutenticada = 3,
        Autorizada = 4,
        NaoAutorizada = 5,
        Capturada = 6,
        Cancelada = 9,
        EmAutenticacao = 10,
        EmCancelamento  = 12
    }

    public enum SituacaoPagamentoCheckout
    {
        Pendente = 1,
        Pago = 2,
        Negado = 3,
        Cancelado = 5,
        NaoFinalizado = 6,
        Autorizado = 7
    }

    public enum SituacaoToken
    {
        Bloqueado = 0,
        Desbloqueado = 1
    }

    public enum TipoDesconto
    {
        Valor, Percentual
    }

    public enum TipoItem
    {
        MaterialFisico,
        ProdutosDigitais,
        Servicos,
        OutrosPagamentos
    }

    public enum TipoFrete
    {
        Correios,
        ValorFixo,
        Gratis,
        RetiradaLoja,
        SemFrete
    }



}
