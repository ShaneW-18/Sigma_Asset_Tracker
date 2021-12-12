using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Sigma3.Objects;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Sigma3.Services.Web
{
    public class SecuritiesApi
    {
        // ?symbol={symbol}
        private static readonly Dictionary<string, SecuritiesModel> CACHED_MODELS = new Dictionary<string, SecuritiesModel>();
        private static List<SecuritiesModel> CACHED_ACTIVE  = new List<SecuritiesModel>();
        private static List<SecuritiesModel> CACHED_GAINERS = new List<SecuritiesModel>();
        private static List<SecuritiesModel> CACHED_LOSERS  = new List<SecuritiesModel>();
        private static List<SecuritiesModel> CACHED_CRYPTO = new List<SecuritiesModel>();



        public static readonly string QUOTES_BASE_URL = "https://query1.finance.yahoo.com/v7/finance/quote";
        public static readonly string NASDAQ_URL = "https://pastecord.com/raw/atylizusyx";
        public static readonly string CNN_URL = "https://money.cnn.com/data/hotstocks/";

        async public static Task<SecuritiesModel> GetAsync(string symbol, bool refresh = false)
        {
            var symbolCased = symbol.ToUpper();

            if (!refresh)
            {
                if (CACHED_MODELS.ContainsKey(symbolCased))
                {
                    return CACHED_MODELS[symbolCased];
                }
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

        async public static Task<List<SecuritiesModel>> GetAsync(List<string> api)
        {
            var models = string.Join(",", api);
            var url = $"{QUOTES_BASE_URL}?symbols={models}";

            var handler = WebHandler.GetInstance();

            var Content = await handler.GetWebsiteContent(url);
            var RootObject = JsonConvert.DeserializeObject<Root>(Content);
            var newList = RootObject.QuoteResponse.Result;

            if (newList.Count == 0) return new List<SecuritiesModel>();

            newList.ForEach(stock =>
            {
                var symbol = stock.Symbol;
                CACHED_MODELS[symbol] = stock;
            });
            return newList;


        }

        async public static Task<HomePageModel> GetHomePageSecurities(bool refresh = false)
        {

            if (!refresh)
            {
                if (CACHED_ACTIVE.Count != 0)
                {
                    return new HomePageModel(CACHED_ACTIVE, CACHED_GAINERS, CACHED_LOSERS, CACHED_CRYPTO);
                }
            } 

            var Handler = WebHandler.GetInstance();
            var symbols = await Handler.GetInnerContentsByClassName(CNN_URL, Constants.CNN_SYMBOL_HTML_CLASS);
            var l = symbols.GetRange(3, 30);

            var models = new List<SecuritiesModel>();
            foreach (var item in l)
            {
                models.Add(await GetAsync(item, refresh));
            }

            var active = models.GetRange(0, 10);
            var gainers = models.GetRange(10, 10);
            var losers = models.GetRange(20, 10);

            CACHED_CRYPTO = new List<SecuritiesModel>()
            {
                CACHED_MODELS["ETH-USD"],
                CACHED_MODELS["LTC-USD"],
                CACHED_MODELS["SOL1-USD"],
                CACHED_MODELS["BTC-USD"],
                CACHED_MODELS["USDC-USD"],
                CACHED_MODELS["LTC-USD"]
            };
            CACHED_ACTIVE = active;
            CACHED_GAINERS = gainers;
            CACHED_LOSERS = losers;

            return new HomePageModel(active, gainers, losers, CACHED_CRYPTO);



        }

        async public static Task<List<SecuritiesModel>> GetUpdate(List<SecuritiesModel> stockModels)
        {
            if (stockModels.Count == 0) return new List<SecuritiesModel>();

            var handler = WebHandler.GetInstance();
            var models = string.Join(",", stockModels.Select(x => x.Symbol).ToArray());
            var url = $"{QUOTES_BASE_URL}?symbols={models}";

            var Content = await handler.GetWebsiteContent(url);
            var RootObject = JsonConvert.DeserializeObject<Root>(Content);
            var newList = RootObject.QuoteResponse.Result;

            if (newList.Count == 0) return new List<SecuritiesModel>();

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
            var content = await handler.GetWebsiteContent(NASDAQ_URL);
            return JsonConvert.DeserializeObject<List<Nasdaq>>(content);
        }


    }
    
}
