using Sigma3.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json;

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

   
        async public Task<Z> SendPostAsync<T, Z>(string url, T data = default(T))
        {
         
            
            var json  = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            var responseStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Z>(responseStr);

        }

    

        async public Task<List<string>> GetInnerContentsByClassName(string url, string className)
        {
            var content = await GetWebsiteContent(url);
            var html = new HtmlDocument();
            html.LoadHtml(content);
            var z = GetElementsByClassName(html, className);
            return GetElementsByClassName(html, className)
                .Select(x => x.InnerText)
                .ToList();
        }

        private static List<HtmlNode> GetElementsByClassName(HtmlDocument doc, String className)
        {
            var regex = new Regex("\\b" + Regex.Escape(className) + "\\b", RegexOptions.Compiled);

            return doc.DocumentNode
                .Descendants()
                .Where(n => n.NodeType == HtmlNodeType.Element)
                .Where(e => (e.Name == "div" || e.Name == "a") && regex.IsMatch(e.GetAttributeValue("class", "")))
                .ToList();
        }

    }
}
