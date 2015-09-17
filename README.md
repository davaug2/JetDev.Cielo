# JetDev.Cielo

Helper para integração com a CIELO.

Integração prevê Solicitação de URL para pagamento (BuyPage Cielo), Solicitação para pagamento local (BuyPage Loja), Cancelamento de Lançamentos, Captura manual de transações, Consulta de situação de transações, Geração de token para transações recorrentes - Compatível com .Net Framework 2.0 ou superior.

NUGET: https://www.nuget.org/packages/JetDev.Cielo


#Configuração
(Informação estatica, colocar antes das requisições ou no global.asax)

    JetDev.Cielo.Config.ChaveEstabelecimento = "25fbb99741c739dd84d7b06ec78c9bac718838630f30b112d033ce2e621b34f3";
    JetDev.Cielo.Config.NumeroEstabelecimento = "1006993069";

#Integração usando a página de pagamento da Cielo

    try
    {
        var codigoVenda = 10;
        var requisicao = new JetDev.Cielo.Requisicoes.RequisicaoTransacao();
        requisicao.UrlRetorno = string.Format("http://suapagina.com.br/retorno?CodigoVenda={0}", codigoVenda);
        requisicao.Autorizacao = TipoAutorizacao.Autorizar_autenticada_e_nao_autenticada;
        requisicao.Pedido = new Pedido();
        requisicao.Pedido.Descricao = "Descrição do pedido";
        requisicao.Pedido.DescricaoExtra = "MINHA_EMPRESA"; // Descrição que aparece na fatura
        requisicao.Pedido.Valor = 100.00M;
        requisicao.Pedido.Numero = codigoVenda.ToString();
        requisicao.FormaPagamento = new FormaPagamento()
        {
            Produto = ProdutoCielo.Credito_a_vista,
            Bandeira = Bandeiras.Mastercard,
        };
        requisicao.Capturar = false; // Para captura automática coloque true

        var retorno = JetDev.Cielo.ServicoCielo.ObterUrlAutenticacao(requisicao, Ambiente.Producao);
        var id = retorno.TransacaoId; // Guardar o código da transação
        Response.Redirect(retorno.UrlAutenticacao); // Redirecione o usuário para a pagina da Cielo
    }
    catch (JetDev.Cielo.ExcecaoPagamento ex)
    {
        // tratamento de erro
    }
    
#Registrando o cartão de crédito na CIELO (Tokenização)

    var requisicao = new JetDev.Cielo.Requisicoes.RequisicaoToken();
    requisicao.Portador = new RequisitarToken()
    { 
        NomeImpresso = "FULANO",
        NumeroCartao = "4444444444444444444",
        ValidadeCartao = new DateTime(2020,01,01)
    };
    
    var retorno = JetDev.Cielo.ServicoCielo.ObterTokenCartao(requisicao);
    if (retorno.Status == SituacaoToken.Desbloqueado) 
    {
        var numeroCartao = retorno.NumeroCartaoTruncado; // Número para mostrar na tela
        var token = retorno.Token // Token para utilizar na compra
    }

#Usando cartão registrado na CIELO
(Processamento transparende de cartão de crédito

    var requisicao = new JetDev.Cielo.Requisicoes.RequisicaoTransacao();
    requisicao.UrlRetorno = string.Format("http://suapagina.com.br/retorno?CodigoVenda={0}", codigoVenda);
    requisicao.Autorizacao = TipoAutorizacao.Autorizar_autenticada_e_nao_autenticada;
    requisicao.Pedido = new Pedido();
    requisicao.Pedido.Descricao = "Descrição do pedido";
    requisicao.Pedido.DescricaoExtra = "MINHA_EMPRESA"; // Descrição que aparece na fatura
    requisicao.Pedido.Valor = 100.00M;
    requisicao.Pedido.Numero = codigoVenda.ToString();
    requisicao.FormaPagamento = new FormaPagamento()
    {
        Produto = ProdutoCielo.Credito_a_vista,
        Bandeira = Bandeiras.Mastercard,
    };
    requisicao.Capturar = false; // Para captura automática coloque true

    var retorno = JetDev.Cielo.ServicoCielo.AutorizarPagamento(requisicao);

    if(retorno.Situacao == SituacaoTransacao.Autorizada) 
    {
        var id = retorno.TransacaoId; // Guardar o código da transação
        // Ok Pagou
    }

#Obter situação da transação

    var retorno = JetDev.Cielo.ServicoCielo.ObterSituacao("45654564564654");
    if(retorno.Situacao == SituacaoTransacao.Autorizada) 
    {
        // Ok Pagou
    }
    
#Capturar o pagamento manualmente
  Depois de autorizado (ou seja, tem limite no cartão), ele precisa ser capturado, que é "colocar a mão na grana".

    var retorno = JetDev.Cielo.ServicoCielo.Capturar("45654564564654");
    if(retorno.Situacao == SituacaoTransacao.Capturada) 
    {
        // Ok o dinheiro vai chegar
    }

