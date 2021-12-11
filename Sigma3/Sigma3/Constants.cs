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


        async public static Task<List<StockModel>> GetDefaultFollowing()
        {
            var appl = await YahooFinance.GetAsync("TSLA");
            var btc = await YahooFinance.GetAsync("BTC-USD");
            var nvda = await YahooFinance.GetAsync("NVDA");
            var dis = await YahooFinance.GetAsync("Dis");
            var twtr = await YahooFinance.GetAsync("TWTR");
            var bac = await YahooFinance.GetAsync("bac");


            return new List<StockModel>()
            {
                appl, btc, nvda, dis, twtr, bac
            };
        }

    }
}
