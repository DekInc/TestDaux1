using DauxTestModels;

using Newtonsoft.Json;

using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DauxService
{
    public class ClienteWebApi
    {
        private string BaseUrl;
        private string HeaderAuth;
        private ApiRequestEjemplo BodyContent;

        public ClienteWebApi(string url, string headerAuth, ApiRequestEjemplo bodyContent)
        {
            BaseUrl = url;
            HeaderAuth = headerAuth;
            BodyContent = bodyContent;
        }

        public string ObtenerResultado()
        {
            using (var handler = new HttpClientHandler() { CookieContainer = new CookieContainer() })
            {
                using (var client = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) })
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseUrl);

                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json charset=UTF-8"));
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //client.DefaultRequestHeaders.
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", HeaderAuth);
                    //client.DefaultRequestHeaders.Add("Accept", "*/*");

                    client.Timeout = TimeSpan.FromMilliseconds(10000);

                    //Task<HttpResponseMessage> res = client.PostAsync(BaseUrl, new StringContent(JsonConvert.SerializeObject(BodyContent)));

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("nombre", BodyContent.nombre),
                        new KeyValuePair<string, string>("apellido", BodyContent.apellido)
                    });

                    Task<HttpResponseMessage> res = client.PostAsync(BaseUrl, formContent);

                    string res2 = res.Result.Content.ReadAsStringAsync().Result;
                    return res2;
                }
            }
        }
    }
}
