using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sigma3.Objects;
using YahooFinanceApi;
using System.Linq;

namespace Sigma3
{
    public class Constans
    {
        public static readonly User DEMO_USER = new User
        {
            Name = "John Doe",
            PhoneNumber = "000-000-0000",
            Email = "Demo",
            Password = "Demo",
            PortfolioBalance = 0

        };

        public static readonly bool DEMO_ENABLED = true;


        async private Task<List<Security>> GetDefaultFollowing()
        {
            var k = await Yahoo
                 .Symbols("APPL", "NVDA", "ETH-USD", "BTC-USD")
                 .Fields(Field.Bid)
                 .QueryAsync();
            return k.Values.ToList();
                
        }
    }
}
