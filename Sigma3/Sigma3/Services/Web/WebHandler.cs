using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sigma3.Services.Web
{
    public class WebHandler
    {
        private static WebHandler Handler = null;
        private static readonly object LockObject = new object();

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
            var client = new HttpClient();
            var content = await client.GetStringAsync(url);
            return content;
        }


    }
}
