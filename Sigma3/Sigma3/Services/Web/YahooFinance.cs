using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Sigma3.Objects;
using Newtonsoft.Json;

namespace Sigma3.Services.Web
{
    public class YahooFinance
    {
        // ?symbol={symbol}
        private static readonly Dictionary<string, SecurityModel> CACHED_MODELS = new Dictionary<string, SecurityModel>();
        public static readonly string QUOTES_BASE_URL = "https://query1.finance.yahoo.com/v7/finance/quote";
        //https://query1.finance.yahoo.com/v8/finance/chart/ROKU?region=US&lang=en-US&includePrePost=false&interval=2m&useYfid=true&range=1d&corsDomain=finance.yahoo.com&.tsrc=finance

        async public static Task<SecurityModel> GetAsync(string symbol)
        {
            var symbolCased = symbol.ToUpper();
            
            if (CACHED_MODELS.ContainsKey(symbolCased))
            {
                return CACHED_MODELS[symbolCased];
            }

            var Handler = WebHandler.GetInstance();
            var URL = $"{QUOTES_BASE_URL}?symbols={symbolCased}";

            var content = await Handler.GetWebsiteContent(URL);
            var obj = JsonConvert.DeserializeObject<Root>(content);

            if (obj.QuoteResponse.Result.Count == 0)
            {
                return null;
            }

            var stock = obj.QuoteResponse.Result[0];
            CACHED_MODELS[symbolCased] = stock;

            return stock;
        }

        async public static Task<List<SecurityModel>> GetAsync(params string[] args)
        {
            return null;
        }

        async public static Task<List<SecurityModel>> GetUpdate(List<SecurityModel> stockModels)
        {
            if (stockModels.Count == 0) return new List<SecurityModel>();

            var handler = WebHandler.GetInstance();
            var models = string.Join(",", stockModels.Select(x => x.Symbol).ToArray());
            var url = $"{QUOTES_BASE_URL}?symbols={models}";

            var Content = await handler.GetWebsiteContent(url);
            var RootObject = JsonConvert.DeserializeObject<Root>(Content);
            var newList = RootObject.QuoteResponse.Result;

            if (newList.Count == 0) return new List<SecurityModel>();

            newList.ForEach(stock =>
            {
                var symbol = stock.Symbol;
                CACHED_MODELS[symbol] = stock;
            });

            return newList;
             
        }
    }
}
