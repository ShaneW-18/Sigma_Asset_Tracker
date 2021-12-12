using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sigma3.Objects;
using System.Linq;
using Sigma3.Services.Web;
using Sigma3.Util;

namespace Sigma3
{
    public class Constants
    {
        public static User DEMO_USER = new User
        {
            Name = "John Doe",
            PhoneNumber = "000-000-0000",
            Email = "Demo",
            Password = "Demo",
            PortfolioBalance = 0,
            Id = StringUtils.HashString("Demo")

        };

        public static readonly bool DEMO_ENABLED = true;
        public static readonly int LONGEST_STOCK_TICKER_LENGTH = 12;
        public static readonly string CNN_SYMBOL_HTML_CLASS = "wsod_symbol";


        async public static Task<List<SecuritiesModel>> GetDefaultFollowing()
        {
            var btc = await SecuritiesApi.GetAsync("BTC-USD");
            var ltc = await SecuritiesApi.GetAsync("LTC-USD");
            var eth = await SecuritiesApi.GetAsync("ETH-USD");
            var sol = await SecuritiesApi.GetAsync("SOL1-USD");
            var usdc = await SecuritiesApi.GetAsync("USDC-USD");




            var appl = await SecuritiesApi.GetAsync("TSLA");
            var dis = await SecuritiesApi.GetAsync("DIS");
            var twtr = await SecuritiesApi.GetAsync("TWTR");
            var bac = await SecuritiesApi.GetAsync("BAC");
            var nvda = await SecuritiesApi.GetAsync("NVDA");



            return new List<SecuritiesModel>()
            {
                btc, ltc, eth, sol, usdc, appl, dis, twtr, bac, nvda
            } ?? new List<SecuritiesModel>();
        }

    }
}
