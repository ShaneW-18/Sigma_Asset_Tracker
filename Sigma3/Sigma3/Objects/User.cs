using System;
using System.Collections.Generic;
using System.Text;
using Sigma3.Services.Web;
using SQLite;
using static Sigma3.Views.AddTooPortfolioPage;

namespace Sigma3.Objects
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        public decimal PortfolioBalance { get; set; }
        public List<StockModel> UserFollowing { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Dictionary<string, UserSecurity> UserPortfolio { get; set; } = new Dictionary<string, UserSecurity>();

        
        /*
        public bool AddSecurity(ATPReturnVal stockModel)
        {
            Transactions.Add(new Transaction
            { 
                TransType = Transaction.TransactionType.BUY,
                TransactionId = new Guid().ToString(),

            });

            var symbol = stockModel.model.Symbol;
            if (UserPortfolio.ContainsKey(symbol))
            {
                UserPortfolio[symbol].AmountOwned += stockModel.AmountTransfered;
                UserPortfolio[symbol].AveragePrice = ();
            }
        }
        */

        
        public class UserSecurity
        {
            public StockModel Security { get; set; }
            public decimal AmountOwned { get; set; } = 0;
            public decimal AveragePrice { get; set; }
            public int BuyTimes { get; set; }
            public int SellTimes { get; set; }
        }



        async public void addFollowing(string symbole)
        {
           
            UserFollowing.Add(await YahooFinance.GetAsync(symbole));

        }
        async public void RemoveFollowing(string symbole)
        {

            UserFollowing.Remove(await YahooFinance.GetAsync(symbole));

        }

    }
}
