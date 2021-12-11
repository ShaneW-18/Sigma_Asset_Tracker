
using System;
using System.Text.Json.Serialization;
using Sigma3.Util;
namespace Sigma3.Objects
{
    public class StockModel
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("region")]
        public string Region;

        [JsonPropertyName("quoteType")]
        public string QuoteType;

        [JsonPropertyName("quoteSourceName")]
        public string QuoteSourceName;             

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("coinImageUrl")]
        public string CoinUrl { get; set; } 

        [JsonPropertyName("triggerable")]
        public bool Triggerable;

        [JsonPropertyName("currency")]
        public string Currency;

        [JsonPropertyName("fiftyDayAverage")]
        public double FiftyDayAverage;

        [JsonPropertyName("fiftyDayAverageChange")]
        public double FiftyDayAverageChange;

        [JsonPropertyName("fiftyDayAverageChangePercent")]
        public double FiftyDayAverageChangePercent;

        [JsonPropertyName("twoHundredDayAverage")]
        public double TwoHundredDayAverage;

        [JsonPropertyName("twoHundredDayAverageChange")]
        public double TwoHundredDayAverageChange;

        [JsonPropertyName("twoHundredDayAverageChangePercent")]
        public double TwoHundredDayAverageChangePercent;

        [JsonPropertyName("marketCap")]
        public long MarketCap;

        [JsonPropertyName("sourceInterval")]
        public int SourceInterval;

        [JsonPropertyName("exchangeDataDelayedBy")]
        public int ExchangeDataDelayedBy;

        [JsonPropertyName("tradeable")]
        public bool Tradeable;

        [JsonPropertyName("firstTradeDateMilliseconds")]
        public long FirstTradeDateMilliseconds;

        [JsonPropertyName("priceHint")]
        public int PriceHint;

        [JsonPropertyName("circulatingSupply")]
        public long CirculatingSupply;

        [JsonPropertyName("lastMarket")]
        public string LastMarket;

        [JsonPropertyName("volume24Hr")]
        public long Volume24Hr;

        [JsonPropertyName("volumeAllCurrencies")]
        public long VolumeAllCurrencies;

        [JsonPropertyName("fromCurrency")]
        public string FromCurrency;

        [JsonPropertyName("toCurrency")]
        public string ToCurrency;

        [JsonPropertyName("regularMarketChange")]
        public double RegularMarketChange { get; set; }

        [JsonPropertyName("regularMarketChangePercent")]
        public double RegularMarketChangePercent { get; set; }

        public string RegularMarketChangePercentProp
        {
            get { return $"{RegularMarketChangePercent.ToString("#,##0.00")}%"; }
        }


        [JsonPropertyName("regularMarketTime")]
        public int RegularMarketTime;

        [JsonPropertyName("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }

        [JsonPropertyName("regularMarketPrice")]
        public string RegularMarketPriceProp
        {
            get { return $"${RegularMarketPrice.ToString("#,##0.00")}"; }
        }
        [JsonPropertyName("regularMarketDayHigh")]
        public double RegularMarketDayHigh;

        [JsonPropertyName("regularMarketDayRange")]
        public string RegularMarketDayRange;

        [JsonPropertyName("regularMarketDayLow")]
        public double RegularMarketDayLow;

        [JsonPropertyName("regularMarketVolume")]
        public long RegularMarketVolume;

        [JsonPropertyName("regularMarketPreviousClose")]
        public double RegularMarketPreviousClose;

        [JsonPropertyName("fullExchangeName")]
        public string FullExchangeName;

        [JsonPropertyName("regularMarketOpen")]
        public double RegularMarketOpen;

        [JsonPropertyName("averageDailyVolume3Month")]
        public long AverageDailyVolume3Month;

        [JsonPropertyName("averageDailyVolume10Day")]
        public long AverageDailyVolume10Day;

        [JsonPropertyName("startDate")]
        public int StartDate;

        [JsonPropertyName("coinImageUrl")]
        public string CoinImageUrl;

        [JsonPropertyName("fiftyTwoWeekLowChange")]
        public double FiftyTwoWeekLowChange;

        [JsonPropertyName("fiftyTwoWeekLowChangePercent")]
        public double FiftyTwoWeekLowChangePercent;

        [JsonPropertyName("fiftyTwoWeekRange")]
        public string FiftyTwoWeekRange;

        [JsonPropertyName("fiftyTwoWeekHighChange")]
        public double FiftyTwoWeekHighChange;

        [JsonPropertyName("fiftyTwoWeekHighChangePercent")]
        public double FiftyTwoWeekHighChangePercent;

        [JsonPropertyName("fiftyTwoWeekLow")]
        public double FiftyTwoWeekLow;

        [JsonPropertyName("fiftyTwoWeekHigh")]
        public double FiftyTwoWeekHigh;

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("exchange")]
        public string Exchange;

        [JsonPropertyName("messageBoardId")]
        public string MessageBoardId;

        [JsonPropertyName("exchangeTimezoneName")]
        public string ExchangeTimezoneName;

        [JsonPropertyName("exchangeTimezoneShortName")]
        public string ExchangeTimezoneShortName;

        [JsonPropertyName("gmtOffSetMilliseconds")]
        public int GmtOffSetMilliseconds;

        [JsonPropertyName("market")]
        public string Market;

        [JsonPropertyName("esgPopulated")]
        public bool EsgPopulated;

        [JsonPropertyName("marketState")]
        public string MarketState;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        public string Color { get; set; }

        

        public string ColorProp
        {
            get
            {
                return RegularMarketChange > 0 ? "Green" : "Red";
            }
        }
   
    }
}
