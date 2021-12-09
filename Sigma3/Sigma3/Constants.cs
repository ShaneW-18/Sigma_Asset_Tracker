using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sigma3.Objects;
using System.Linq;
using Sigma3.Services.Web;

namespace Sigma3
{
    public class Constants
    {
        public static readonly User DEMO_USER = new User
        {
            Name = "John Doe",
            PhoneNumber = "000-000-0000",
            Email = "Demo",
            Password = "Demo",
            PortfolioBalance = 0,
        };

        public static readonly bool DEMO_ENABLED = true;


        async public static Task<List<StockModel>> GetDefaultFollowing()
        {
            var appl = await YahooFinance.Get("TSLA");
            var btc = await YahooFinance.Get("BTC-USD");
            var nvda = await YahooFinance.Get("NVDA");
            var dis = await YahooFinance.Get("Dis");


            return new List<StockModel>()
            {
                appl, btc, nvda, dis
            };
        }

    }
}
