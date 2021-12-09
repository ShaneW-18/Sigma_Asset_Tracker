using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sigma3.Objects;
using Newtonsoft.Json;

namespace Sigma3.Services.Web
{
    public class YahooFinance
    {
        // ?symbol={symbol}
        public static readonly string QUOTES_BASE_URL = "https://query1.finance.yahoo.com/v7/finance/quote";
        //https://query1.finance.yahoo.com/v8/finance/chart/ROKU?region=US&lang=en-US&includePrePost=false&interval=2m&useYfid=true&range=1d&corsDomain=finance.yahoo.com&.tsrc=finance

        async public static Task<StockModel> Get(string symbol)
        {
            var Handler = WebHandler.GetInstance();
            var URL = $"{QUOTES_BASE_URL}?symbols={symbol}";
            var content = await Handler.GetWebsiteContent(URL);
            var obj = JsonConvert.DeserializeObject<Root>(content);
            return obj.QuoteResponse.Result.Count == 0 ? null : obj.QuoteResponse.Result[0];
           
        }
    }
}
