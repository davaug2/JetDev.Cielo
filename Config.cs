using System;
using System.Collections.Generic;
using System.Text;

namespace JetDev.Cielo
{
    public static class Config
    {
        public delegate void LogAction(string transaction, string request, string response);
        private static event LogAction OnLogAction;
        private static Entidades.Ambiente ambiente = Entidades.Ambiente.TesteCieloIntegrado;
        private static string URLteste = "https://qasecommerce.cielo.com.br/servicos/ecommwsec.do";
        private static string URLproducao = "https://ecommerce.cielo.com.br/servicos/ecommwsec.do";
        private static string URLproducaoCieloCheckout = "https://cieloecommerce.cielo.com.br/api/public/v1/orders";

        public static Entidades.Ambiente Ambiente { get { return ambiente; } set { ambiente = value; } }
        public static string URLTeste { get { return URLteste; } set { URLteste = value; } }
        public static string URLProducao { get { return URLproducao; } set { URLproducao = value; } }
        public static string URLProducaoCieloCheckout { get { return URLproducaoCieloCheckout; } set { URLproducaoCieloCheckout = value; } }
        public static string NumeroEstabelecimento { get; set; }
        public static string ChaveEstabelecimento { get; set; }

        public static void SetLogAction(LogAction action)
        {
            if (OnLogAction == null)
                OnLogAction += action;
        }

        internal static void CallLogAction(string transaction, string request, string response)
        {
            if (OnLogAction != null)
                OnLogAction(transaction, request, response);
        }

    }
}
