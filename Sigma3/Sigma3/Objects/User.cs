using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Sigma3.Objects;
using Sigma3.Services.Web;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Sigma3.Objects
{
    public class User
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        public decimal PortfolioBalance { get; set; }
        public List<StockModel> UserFollowing { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Dictionary<string, UserSecurity> UserPortfolio { get; set; } = new Dictionary<string, UserSecurity>();


        async public Task<bool> AddTransaction(Transaction transaction)
        {
            var Symbol = transaction.SecurityTraded;
            var isBuy = transaction.TransType.HasFlag(Objects.TransactionType.BUY);


            transaction.UserId = Id;
            var response =  await SigmaTransaction.SendPostAsync(transaction);
            Transactions.Add(transaction);
            
            // I dont believe this is thread-safe.. ConcurrentDict would probably be better?
            if (UserPortfolio.ContainsKey(Symbol))
            {
                var element = UserPortfolio[Symbol];
                if (isBuy)
                { 
                    element.BuyTimes += 1;
                    element.AmountOwned += transaction.AmountTraded;
                    
                }
                else
                {
                    element.SellTimes += 1;
                    element.AmountOwned -= transaction.AmountTraded;
                }

            }
            else
            {
                var PriceBoughtAt = transaction.
                UserPortfolio[Symbol] = new UserSecurity(Symbol, 0.0, )
            }



            return response != null;
         
        }
        

        
        public class UserSecurity
        {
            public string symbol { get; set; }
            public decimal AmountOwned { get; set; }
            public decimal AveragePrice { get; set; }
            public int BuyTimes { get; set; }
            public int SellTimes { get; set; }

            public UserSecurity(string symbol, decimal AmountOwned, decimal AveragePrice, int BuyTimes, int SellTimes)
            {
                this.symbol = symbol;
                this.AmountOwned = AmountOwned;
                this.AveragePrice = AveragePrice;
                this.BuyTimes = BuyTimes;
                this.SellTimes = SellTimes;

            }
        }

  


        
    }
}
