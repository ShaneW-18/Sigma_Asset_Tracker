using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sigma3.Objects;

namespace Sigma3.Services.Web
{
    public class SigmaTransaction
    {
        public readonly static string BASE_URL = "https://sigma.schoolbot.dev/api/transaction";

        async public static Task<Transaction> SendPostAsync(Transaction transaction)
        {
            var Handler = WebHandler.GetInstance();
            return await Handler.SendRequestAsync<Transaction, Transaction>(BASE_URL, "post", transaction);    
        }

        async public static Task<Transaction?> SendGetAsync(string id)
        {
            var Handler = WebHandler.GetInstance();
            var url = $"{BASE_URL}/{id}";
            return await Handler.SendRequestAsync<Transaction, Transaction>(url, "get");
        }
        async public static Task<bool> SendPutAsync(string id, Transaction transaction)
        {
            var Handler = WebHandler.GetInstance();
            var url = $"{BASE_URL}/{id}";
            var response =  await Handler.SendRequestAsync<Transaction, int>(url, "get", transaction);
            return response == 0;
        }

        async public static Task<bool> SendDeleteAsync(string id)
        {
            var Handler = WebHandler.GetInstance();
            var url = $"{BASE_URL}/{id}";
            var response =  await Handler.SendRequestAsync<Transaction, int>(url, "get");
            return response == 95; 
        }
    }
}
