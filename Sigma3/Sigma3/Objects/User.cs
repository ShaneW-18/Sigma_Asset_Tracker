using System;
using System.Collections.Generic;
using Sigma3.Services.Web;
using Sigma3.Services;
using SQLite;
using Sigma3.Util;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;
using SQLiteNetExtensions.Attributes;

namespace Sigma3.Objects
{
    public class User
    {
        [PrimaryKey]
        public string Id { get; set; }

        public  bool porfolioHidden = false;
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        public decimal PortfolioBalance { get; set; } = 0;

       
    

        public string UserFollowingBlob { get; set; }


        private readonly List<SecuritiesModel> _userFollowing = new List<SecuritiesModel>();

        [TextBlob(nameof(UserFollowingBlob))]
        [Ignore]
        public List<SecuritiesModel> UserFollowing
        {
            get
            {
                if (String.IsNullOrWhiteSpace(UserFollowingBlob))
                {
                    return new List<SecuritiesModel>();
                }
                return JsonConvert.DeserializeObject<List<SecuritiesModel>>(UserFollowingBlob);
            }

            set
            {
                UserFollowingBlob = JsonConvert.SerializeObject(value);
                AppService.UpdateUser(this);
            }
        }

        public string TransactionsBlob { get; set; }


        private readonly List<TransactionModel> _transactionModels = new List<TransactionModel>();   

        [TextBlob(nameof(TransactionsBlob))]
        [Ignore]
        public List<TransactionModel> Transactions
        {
            get
            {
                if (String.IsNullOrEmpty(TransactionsBlob))
                {
                    return new List<TransactionModel>();
                }    
                return JsonConvert.DeserializeObject<List<TransactionModel>>(TransactionsBlob);

            }

            set
            {
                TransactionsBlob = JsonConvert.SerializeObject(value);
                AppService.UpdateUser(this);


            }
        }

        public string UserPortfolioBlob { get; set; }
        private Dictionary<string, UserSecurity> _userPortfolio = new Dictionary<string, UserSecurity>();

        [TextBlob(nameof(UserPortfolioBlob))]
        [Ignore]
        public Dictionary<string, UserSecurity> UserPortfolio
        {
            get
            {
                if (String.IsNullOrEmpty(UserPortfolioBlob))
                {
                    return new Dictionary<string, UserSecurity>();
                }
                return JsonConvert.DeserializeObject<Dictionary<string, UserSecurity>>(UserPortfolioBlob);

            }

            set
            {
                UserPortfolioBlob = JsonConvert.SerializeObject(value);
                AppService.UpdateUser(this);

            }
        }


        async public Task<bool> AddTransaction(TransactionModel transaction, SecuritiesModel model)
        {
            var Symbol = transaction.SecurityTraded;
            var isBuy = transaction.TransType.Equals("BUY");
            // slow 
            var ParseRegularMarketPrice = decimal.Parse(model.RegularMarketPrice.ToString());


            transaction.UserId = Id;
            var response =  await SigmaTransaction.SendPostAsync(transaction);

            AddTransaction(transaction);
            
            // I dont believe this is thread-safe.. ConcurrentDict would probably be better?
            if (UserPortfolio.ContainsKey(Symbol))
            {
                var element = _userPortfolio[Symbol];
                if (isBuy)
                {
                    element.BuyTimes += 1;
                    element.AmountOwned += transaction.AmountTransacted;
                    PortfolioBalance += (ParseRegularMarketPrice * transaction.AmountTransacted);
                    
                }
                else
                {
                    element.SellTimes += 1;
                    element.AmountOwned -= transaction.AmountTransacted;
                    PortfolioBalance -= (ParseRegularMarketPrice * transaction.AmountTransacted);

                    if (element.AmountOwned == 0)
                    {
                        PortfolioBalance = 0;
                        RemovePortfolio(Symbol);
                    }
                }
                UserPortfolio = _userPortfolio;

            }
            else
            {
                if (isBuy)
                {
                    AddUserPortfolio( new UserSecurity(Symbol, transaction.AmountTransacted, transaction.PricePerSecurity, 1, model.ShortName) );
                    PortfolioBalance += (ParseRegularMarketPrice * transaction.AmountTransacted);

                }
                else
                {
                    AddUserPortfolio ( new UserSecurity(transaction.AmountTransacted, Symbol, transaction.PricePerSecurity, 1, model.ShortName) );
                    PortfolioBalance -= (ParseRegularMarketPrice * transaction.AmountTransacted);

                }
            }



            return response != null;
         
        }

        // figure out a way to lazy load this
        async public Task<List<UserPortfolioObject>> GetUserPortfolio()
        {

          
            var keys = UserPortfolio.Keys;
            var values = new List<UserPortfolioObject>();
            foreach (var key in keys)
            {
                var item = await SecuritiesApi.GetAsync(key);
                values.Add(new UserPortfolioObject(item, UserPortfolio[key]));
            }
            return values;
        }

        public void RemovePortfolio(string symbol)
        {
            _userPortfolio.Remove(symbol);
             UserPortfolio = _userPortfolio;
            
            
        }

        public void AddTransaction(TransactionModel transaction)
        {
            _transactionModels.Add(transaction);
            Transactions = _transactionModels;
        }

        public void AddUserPortfolio(UserSecurity security)
        {
            _userPortfolio[security.Symbol] = security;
            UserPortfolio = _userPortfolio;
        }

        async public void AddFollowing(string symbole)
        {

            _userFollowing.Add(await SecuritiesApi.GetAsync(symbole));
            UserFollowing = _userFollowing;

        }
        public void RemoveFollowing(string symbol)
        {
            UserFollowing.RemoveAll(security => security.Symbol.Equals(symbol));

        }



        public class UserSecurity
        {
            public string Symbol { get; set; }
            public decimal AmountOwned { get; set; }
            public decimal AveragePrice { get; set; }
            public int BuyTimes { get; set; } = 0;
            public int SellTimes { get; set; } = 0;
            public string ShortName { get; set; }

            public UserSecurity(string symbol, decimal AmountOwned, decimal AveragePrice, int BuyTimes, int SellTimes)
            {
                this.Symbol = symbol;
                this.AmountOwned = AmountOwned;
                this.AveragePrice = AveragePrice;
                this.BuyTimes = BuyTimes;
                this.SellTimes = SellTimes;
            }

            public UserSecurity(string symbol, decimal AmountOwned, decimal AveragePrice, int BuyTimes, string name)
            {
                this.Symbol = symbol;
                this.AmountOwned = AmountOwned;
                this.AveragePrice = AveragePrice;
                this.BuyTimes = BuyTimes;
                this.ShortName = name;
            }
            public UserSecurity(decimal AmountOwned, string symbol, decimal AveragePrice, int SellTimes, string name)
            {
                this.Symbol = symbol;
                this.AmountOwned = AmountOwned;
                this.AveragePrice = AveragePrice;
                this.SellTimes = SellTimes;
                this.ShortName = name;
            }

            public UserSecurity()
            {

            }
        }

        public class UserPortfolioObject
        {
            public string SecurityName { get; set; }
            public string SecuritySymbol { get; set; }
            public string UnderName { get; set; }
            public string TotalOwned { get; set; }
            public string SecurityPrice { get; set; }
            public UserPortfolioObject(SecuritiesModel model, UserSecurity us)
            {
                var dec = decimal.Parse(model.RegularMarketPrice.ToString());
                this.SecurityPrice = model.RegularMarketPriceProp;
                // lol its 7am
                this.UnderName = $"{StringUtils.ParseNumberWithCommas(decimal.Parse(us.AmountOwned.ToString()))} | {model.RegularMarketPriceProp}";
                this.TotalOwned = $"${StringUtils.ParseNumberWithCommas (dec * us.AmountOwned)}";
                this.SecurityName = us.ShortName;
                this.SecuritySymbol = us.Symbol;
            }

            public UserPortfolioObject()
            {

            }
        }



   

    }
}
