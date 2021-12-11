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
        private static readonly Dictionary<string, StockModel> CACHED_MODELS = new Dictionary<string, StockModel>();
        public static readonly string QUOTES_BASE_URL = "https://query1.finance.yahoo.com/v7/finance/quote";
        public static readonly string NASDAQ_URL = "https://pastecord.com/raw/atylizusyx";
        //https://query1.finance.yahoo.com/v8/finance/chart/ROKU?region=US&lang=en-US&includePrePost=false&interval=2m&useYfid=true&range=1d&corsDomain=finance.yahoo.com&.tsrc=finance

        async public static Task<StockModel> GetAsync(string symbol)
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
            var stock = obj.QuoteResponse.Result[0];
            CACHED_MODELS[symbolCased] = stock;

            return obj.QuoteResponse.Result.Count == 0 ? null : stock;
        }

        async public static Task<List<StockModel>> GetAsync(params string[] args)
        {
            return null;
        }

        async public static Task<List<StockModel>> GetUpdate(List<StockModel> stockModels)
        {
            if (stockModels.Count == 0) return new List<StockModel>();

            var handler = WebHandler.GetInstance();
            var models = string.Join(",", stockModels.Select(x => x.Symbol).ToArray());
            var url = $"{QUOTES_BASE_URL}?symbols={models}";

            var Content = await handler.GetWebsiteContent(url);
            var RootObject = JsonConvert.DeserializeObject<Root>(Content);
            var newList = RootObject.QuoteResponse.Result;

            if (newList.Count == 0) return new List<StockModel>();

            newList.ForEach(stock =>
            {
                var symbol = stock.Symbol;
                CACHED_MODELS[symbol] = stock;
            });

            return newList;
             
        }
        async public static Task<List<Nasdaq>> GetNasdaq()
        {
            var handler = WebHandler.GetInstance();
            var URL = NASDAQ_URL;
            var content = await handler.GetWebsiteContent(URL);

            var list = new List<Nasdaq>();
            list = JsonConvert.DeserializeObject<List<Nasdaq>>(content);
            return list;
        }
    }
    
}
