using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sigma3.Services.Web
{
    public class WebHandler
    {
        private static WebHandler Handler = null;
        private static readonly object LockObject = new object();
        private static readonly HttpClient client = new HttpClient();


        private WebHandler()
        {

        }

        public static WebHandler GetInstance()
        {
            if (Handler == null)
            {
                lock (LockObject)
                {
                    if (Handler == null)
                    {
                        return Handler = new WebHandler();
                    }
                }
            }
            return Handler;
        }

        async public Task<string> GetWebsiteContent(string url)
        {
            var content = await client.GetStringAsync(url);
            return content;
        }

        async public Task<Z> SendPostAsync<T, Z>(string url, T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var data  = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);
            var responseStr = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Z>(responseStr);
        }

        async public Task<Z> SendRequestAsync<T, Z>(string url, string requestType, T data = default(T))
        {
            var request = CraftRequest(requestType, url, data);
            var response = await client.SendAsync(request);
            var responseStr =  await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Z>(responseStr);
        }

        private HttpRequestMessage CraftRequest<T>(string requestType, string url, T data)
        {
            var request = requestType.ToLower();
            var Method = request switch
            {
                "post" => HttpMethod.Post,
                "put" => HttpMethod.Post,
                "get" => HttpMethod.Get,
                "delete" => HttpMethod.Delete,
                _ => HttpMethod.Get
            };

            var company = JsonSerializer.Serialize(data);

            var returnMessage = new HttpRequestMessage(Method, url);
            if (!data.Equals(default(T)))
            {
                returnMessage.Content = new StringContent(company, Encoding.UTF8, "application/json");
            }
            return returnMessage;
        }

    }
}
