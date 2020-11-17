using BancoInter.Model;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace BancoInter
{
    public class ServiceAsync
    {
        private string _url = "https://apis.bancointer.com.br/openbanking/v1/certificado/boletos";
        private HttpClient _httpClient;

        public ServiceAsync(string numContaCorrente, string caminhoCertificado, string senha)
        {
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var handler = new WebRequestHandler();
            handler.ClientCertificates.Add(new X509Certificate2(caminhoCertificado, senha));

            _httpClient = new HttpClient(handler);

            _httpClient.DefaultRequestHeaders.Add("x-inter-conta-corrente", numContaCorrente);
        }

        #region Common
        private async Task<T> Post<T>(string url, string body)
        {
            using (var response = await _httpClient
                .PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json")))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase, new Exception(apiResponse, new Exception(body)));
                }

                return JsonConvert.DeserializeObject<T>(apiResponse);
            }
        }

        private async Task<T> Get<T>(string url)
        {
            using (var response = await _httpClient
                .GetAsync(url))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase, new Exception(apiResponse));
                }

                return JsonConvert.DeserializeObject<T>(apiResponse);
            }
        }

        private async Task<byte[]> GetInBytes(string url)
        {
            using (var response = await _httpClient
                 .GetAsync(url))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase, new Exception(apiResponse));
                }

                return Convert.FromBase64String(apiResponse);
            }
        }

        private string GetUrl(string param)
        {
            return $"{_url}{param}";
        }

        private string QueryString<T>(T obj)
        {
            var props = typeof(T).GetProperties().ToList();
            var querystring = string.Join("&", props.Select(a => a.Name + "=" + GetPropertieValue<T>(a, obj)));
            return querystring;
        }

        private string GetPropertieValue<T>(PropertyInfo a, T obj)
        {
            var value = a.GetValue(obj);
            if (value.GetType() == typeof(DateTime))
                return ((DateTime)value).ToString("yyyy-MM-dd");
            return value.ToString();
        }
        #endregion

        #region Post
        public virtual async Task<NovoBoleto.Response> NovoBoleto(NovoBoleto boleto)
        {
            boleto.isValid();

            var url = GetUrl(string.Empty);

            var jsonBody = JsonConvert.SerializeObject(boleto);

            return await Post<NovoBoleto.Response>(url, jsonBody);
        }

        public virtual async Task<BaixaBoleto.Response> BaixaBoleto(string nossoNumero, BaixaBoleto boleto)
        {
            var url = GetUrl($"/{nossoNumero}/baixas");

            var jsonBody = JsonConvert.SerializeObject(boleto);

            return await Post<BaixaBoleto.Response>(url, jsonBody);
        }
        #endregion

        #region Get
        public virtual async Task<byte[]> BoletoPdf(string nossoNumero)
        {
            var url = GetUrl($"/{nossoNumero}/pdf");

            return await GetInBytes(url);
        }

        public virtual async Task<BoletoDetalhado.Response> BoletoDetalhado(string nossoNumero)
        {
            var url = GetUrl($"/{nossoNumero}");

            return await Get<BoletoDetalhado.Response>(url);
        }

        public virtual async Task<FiltroBoleto.Response> ListaBoletos(FiltroBoleto filtroBoleto)
        {
            var url = GetUrl($"?{QueryString<FiltroBoleto>(filtroBoleto)}");

            return await Get<FiltroBoleto.Response>(url);
        }
        #endregion

    }
}
