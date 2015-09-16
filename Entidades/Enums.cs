using System;
using System.Collections.Generic;
using System.Text;

namespace JetDev.Cielo.Entidades
{
    public enum Ambiente
    {
        TesteCieloCheckout,
        TesteWebService,
        Producao,
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


    public enum SituacaoToken
    {
        Bloqueado = 0,
        Desbloqueado = 1
    }
}
