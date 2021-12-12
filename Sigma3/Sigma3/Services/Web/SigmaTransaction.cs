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

        async public static Task<TransactionModel> SendPostAsync(TransactionModel transaction)
        {
            transaction.Id = Guid.NewGuid().ToString();
            transaction.Time = DateTimeOffset.Now;

            var Handler = WebHandler.GetInstance();
            return await Handler.SendPostAsync<TransactionModel, TransactionModel>(BASE_URL, transaction);    
        }

    }
}
