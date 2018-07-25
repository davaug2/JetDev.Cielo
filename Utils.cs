using JetDev.Cielo.Entidades;
using JetDev.Cielo.Requisicoes;
using JetDev.Cielo.Respostas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace JetDev.Cielo
{
    public static class Utils
    {
        public const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
        public const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;

        private const string @namespace = "http://ecommerce.cbmp.com.br";
        private const string encodingDefault = "ISO-8859-1";

        internal static Tresposta Requisitar<Tresposta, TRequest>(TRequest request)
            where Tresposta : RespostaBase
            where TRequest : RequisicaoBase
        {
            return Requisitar<Tresposta, TRequest>(request, Config.Ambiente);
        }

        internal static Tresposta Requisitar<Tresposta, TRequest>(TRequest request, JetDev.Cielo.Entidades.Ambiente enviroment)
            where Tresposta : RespostaBase
            where TRequest : RequisicaoBase
        {
            if (enviroment == Ambiente.ProducaoCieloCheckout)
                return RequisitarJSON<Tresposta, TRequest>(request, enviroment);
            else
                return RequisitarXML<Tresposta, TRequest>(request, enviroment);
        }
        internal static TResposta RequisitarJSON<TResposta, TRequest>(TRequest request, JetDev.Cielo.Entidades.Ambiente enviroment)
            where TResposta : RespostaBase
            where TRequest : RequisicaoBase
        {
            var encoding = System.Text.Encoding.UTF8;// Encoding.GetEncoding(encodingDefault);
            request.XMLRequisicao = JsonConvert.SerializeObject(request, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            var postDataByte = encoding.GetBytes(request.XMLRequisicao);
            /*
            using (var wc = new WebClient())
            {
                wc.Headers.Add("MerchantID", Config.ChaveEstabelecimento);
                wc.Headers.Add("Content-Type", "text/json");
                var responseService = wc.UploadData(Config.URLProducaoCieloCheckout, "GET", postDataByte);
                request.XMLResposta = encoding.GetString(responseService);
            }
            */

            var wr = WebRequest.Create(Config.URLProducaoCieloCheckout);
            wr.Method = "POST";
            wr.Headers.Add("MerchantID", Config.ChaveEstabelecimento);
            wr.ContentType = "text/json";
            wr.ContentLength = postDataByte.Length;
            var stream = wr.GetRequestStream();
            stream.Write(postDataByte, 0, postDataByte.Length);
            stream.Flush();
            stream.Close();

            StreamReader sr = new StreamReader(wr.GetResponse().GetResponseStream(), encoding);
            var jsonResposta = sr.ReadToEnd();
            request.XMLResposta = jsonResposta;


            var resposta = JsonConvert.DeserializeObject<RespostaTransacaoCheckout>(request.XMLResposta);

            resposta.XMLRequisicao = request.XMLRequisicao;
            resposta.XMLResposta = request.XMLResposta;

            return (TResposta)(object)resposta;
        }
        internal static Tresposta RequisitarXML<Tresposta, TRequest>(TRequest request, JetDev.Cielo.Entidades.Ambiente enviroment)
            where Tresposta : RespostaBase
            where TRequest : RequisicaoBase
        {
            ServicePointManager.SecurityProtocol = Tls12;
            var encoding = Encoding.GetEncoding(encodingDefault);
            var xmlRequest = ParaXML<TRequest>(request);
            var postData = string.Format("mensagem={0}", xmlRequest);
            var postDataByte = encoding.GetBytes(postData);
            var url = string.Empty;
            switch (enviroment)
            {
                case JetDev.Cielo.Entidades.Ambiente.TesteCieloIntegrado:
                case JetDev.Cielo.Entidades.Ambiente.TesteWebService:
                    url = Config.URLTeste;
                    break;
                case JetDev.Cielo.Entidades.Ambiente.Producao:
                    url = Config.URLProducao;
                    break;
                default:
                    break;
            }
            var wr = WebRequest.Create(url);
            wr.Method = "POST";
            wr.ContentType = "application/x-www-form-urlencoded";
            wr.ContentLength = postDataByte.Length;
            var stream = wr.GetRequestStream();
            stream.Write(postDataByte, 0, postDataByte.Length);
            stream.Flush();
            stream.Close();


            StreamReader sr = new StreamReader(wr.GetResponse().GetResponseStream(), encoding);
            var XMLResposta = sr.ReadToEnd();

            if (!string.IsNullOrEmpty(XMLResposta) && XMLResposta.Contains("<erro"))
                throw new ExcecaoPagamento(DeXML<Erro>(XMLResposta), xmlRequest, XMLResposta);

            var resposta = DeXML<Tresposta>(XMLResposta);
            resposta.XMLRequisicao = xmlRequest;
            resposta.XMLResposta = XMLResposta;

            request.XMLRequisicao = xmlRequest;
            request.XMLResposta = XMLResposta;

            return resposta;
        }

        private static string ParaXML<T>(T obj) where T : class
        {
            var encoding = Encoding.GetEncoding(encodingDefault);
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms, encoding))
            using (var writer = new XmlTextWriter(sw))
            {
                var ser = new XmlSerializer(typeof(T), @namespace);
                writer.Formatting = System.Xml.Formatting.Indented;
                ser.Serialize(writer, obj);
                ser = null;
                return encoding.GetString(ms.ToArray());
            }
        }

        private static T DeXML<T>(string xml) where T : class
        {
            var encoding = Encoding.GetEncoding(encodingDefault);
            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                var ser = new XmlSerializer(typeof(T), @namespace);
                return ser.Deserialize(ms) as T;
            }
        }

        public static decimal ConverteValorDeCielo(string value)
        {
            var vl = value;
            if (vl.Length > 2)
                vl = vl.Insert(vl.Length - 2, ",");
            decimal castValue;
            if (decimal.TryParse(vl, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), out castValue))
                return castValue;
            else
                return 0;
        }

        public static string ConverteValorParaCielo(decimal value)
        {
            var strValue = value.ToString(System.Globalization.CultureInfo.GetCultureInfo("pt-BR")).Replace(".", "");
            var dataValue = strValue.Split(',');

            if (dataValue.Length == 1)
                return dataValue[0] + "00";
            if (dataValue.Length == 2 && dataValue[1].Length == 1)
                return dataValue[0] + dataValue[1] + "0";
            if (dataValue.Length == 2 && dataValue[1].Length == 2)
                return dataValue[0] + dataValue[1];
            if (dataValue.Length == 2 && dataValue[1].Length > 2)
                return dataValue[0] + dataValue[1].Substring(0, 2);
            else
                return "0";
        }

        public static DateTime ConverteDataDeCielo(string data)
        {
            var cultura = CultureInfo.GetCultureInfo("en-US");
            DateTime datetimeCast;
            if (DateTime.TryParse(data, cultura, DateTimeStyles.None, out datetimeCast))
                return datetimeCast;
            return DateTime.MinValue;
        }

        public static string ConverteDataParaCielo(DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}
