using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Sigma3.Objects;
using Sigma3.Services.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sigma3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTooPortfolioPage : ContentPage
    {
        private Button buttonSelected { get; set; } 
        private List<Entry> Entries { get; set; }

        public AddTooPortfolioPage(SecuritiesModel stock = null)
        {

            // lol

            var portfolio = MainPage.USER_LOGGED_IN.UserPortfolio;

            InitializeComponent();
           
            this.BuyButton.IsEnabled = true;
            this.buttonSelected = this.BuyButton;


            // Didnt feel like setting it in the XAML
            this.BuyButton.BackgroundColor = Color.LightGreen;
            this.BuyButton.FontAttributes = FontAttributes.Bold;
            this.BuyButton.TextColor = Color.White;
            this.BuyButton.BorderColor = Color.LightGreen;
            this.BuyButton.BorderWidth = 1;


            this.AddTransActionButton.BackgroundColor = Color.LightGreen;
            this.AddTransActionButton.FontAttributes = FontAttributes.Bold;
            this.AddTransActionButton.TextColor = Color.White;
            this.AddTransActionButton.BorderColor = Color.LightGreen;

            if (portfolio.Count == 0)
            {
                this.SellButton.IsEnabled = false;
            }

            this.SellButton.BackgroundColor = Color.Black;
            this.SellButton.BorderColor = Color.Gray;
            this.SellButton.TextColor = Color.Gray;

          
        }

       

        /**
         * There is 1000% a better way to do this
         */
        private void BuyButton_Clicked(object sender, EventArgs e)
        {
            // Buy
            this.BuyButton.BackgroundColor = Color.LightGreen;
            this.BuyButton.FontAttributes = FontAttributes.Bold;
            this.BuyButton.TextColor = Color.White;
            this.BuyButton.BorderColor = Color.LightGreen;
            this.BuyButton.BorderWidth = 1;


            // Sell
            this.SellButton.BackgroundColor = Color.Black;
            this.SellButton.BorderColor = Color.Gray;
            this.SellButton.TextColor = Color.Gray;
            this.SellButton.BorderWidth = 0;


            // Add TransactionModel
            this.AddTransActionButton.BackgroundColor = Color.LightGreen;
            this.AddTransActionButton.FontAttributes = FontAttributes.Bold;
            this.AddTransActionButton.TextColor = Color.White;
            this.AddTransActionButton.BorderColor = Color.LightGreen;

            // Entry
            this.AmountEntry.Placeholder = "Security Purchased";


            // Button
            this.buttonSelected = BuyButton;

        }

        private void SellButton_Clicked(object sender, EventArgs e)
        {
            // Sell
            this.SellButton.BackgroundColor = Color.Red;
            this.SellButton.FontAttributes = FontAttributes.Bold;
            this.SellButton.TextColor = Color.White;
            this.SellButton.BorderColor = Color.LightGreen;
            this.SellButton.BorderWidth = 1;


            // Buy
            this.BuyButton.BackgroundColor = Color.Black;
            this.BuyButton.BorderColor = Color.Gray;
            this.BuyButton.TextColor = Color.Gray;
            this.BuyButton.BorderWidth = 0;


            // Add TransactionModel
            this.AddTransActionButton.BackgroundColor = Color.Red;
            this.AddTransActionButton.FontAttributes = FontAttributes.Bold;
            this.AddTransActionButton.TextColor = Color.White;
            this.AddTransActionButton.BorderColor = Color.LightGreen;

            // Entry
            this.AmountEntry.Placeholder = "Security Sold";

            // Button
            this.buttonSelected = SellButton;

        }


        
        
        async private void AddTransActionButton_Clicked(object sender, EventArgs e)
        {
                ToggleUI();
            if (!CrossConnectivity.Current.IsConnected)
                await DisplayAlert("Error", "no internet", "OK");
            else
            {
                var errors = await HandleValidation();

                if (!String.IsNullOrWhiteSpace(errors.Errors))
                {
                    await DisplayAlert("Error occured", errors.Errors, "Try again");
                    ToggleUI();
                    return;
                }


                var transaction = new TransactionModel()
                {
                    TransType = buttonSelected.Text == "Sell" ? "SELL" : "BUY",
                    AmountTransacted = decimal.Parse(AmountEntry.Text),
                    PricePerSecurity = decimal.Parse(PricePerAssetEntry.Text),
                    SecurityTraded = SecurityTransfered.Text
                };

                var sucess = await MainPage.USER_LOGGED_IN.AddTransaction(transaction, errors.StockModel);

                if (sucess)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "API error occured", "Ok");
                }
            }
                ToggleUI();
        }

        


        
        async private Task<ATPObj> HandleValidation()
        {
            var builder = new StringBuilder(); 

            if ( String.IsNullOrWhiteSpace(PricePerAssetEntry.Text) )
            {
                builder.Append("Price Field is Empty")
                    .Append("\n");
            }

            if (!(decimal.TryParse(PricePerAssetEntry.Text, out var pricePerAsset)))
            {
                builder.Append("Price Entry is not a number")
                    .Append("\n");
            }

            var symbol = SecurityTransfered.Text;
            if ( String.IsNullOrWhiteSpace( symbol ) )
            {
                builder.Append("Asset Field is empty")
                    .Append("\n");
                return new ATPObj(builder.ToString(), null);
            }


            
            if (symbol.Length >= Constants.LONGEST_STOCK_TICKER_LENGTH)
            {
                builder.Append("Your Asset symbol is too large & most likely doesnt exist")
                    .Append("\n");
            }


            var asset = await SecuritiesApi.GetAsync(symbol);

            if (asset == null)
            {
                builder.Append($"{symbol} is either not supported or doesnt exist")
                    .Append("\n");
                
            }


            if (String.IsNullOrWhiteSpace(AmountEntry.Text))
            {
                builder.Append("Amount Field is Empty")
                    .Append("\n");
                return new ATPObj(builder.ToString(), null);
            }

            if (!(decimal.TryParse(PricePerAssetEntry.Text, out var amount)))
            {
                builder.Append("Amount Entry is not a number")
                    .Append("\n");
            }

            if (asset == null)
            {
                return new ATPObj(builder.ToString(), null);
            }

            CanUserDoAction(asset, buttonSelected.Text, builder);

            return new ATPObj(builder.ToString(), asset);
           
        }
        

        // Probably bad should return a bool
        private void CanUserDoAction(SecuritiesModel model, string action, StringBuilder builder)
        {
            if (action.Equals("sell", StringComparison.OrdinalIgnoreCase))
            {
                var portfolio = MainPage.USER_LOGGED_IN.UserPortfolio;
                var symbol = model.Symbol;

                var exist = portfolio.Values.FirstOrDefault(security => security.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));

                if (exist == null)
                {
                    builder.Append("You do not own this security")
                        .Append("\n");
                    return;
                }
                var AttemptSelling = decimal.Parse(AmountEntry.Text);

                if (AttemptSelling > exist.AmountOwned)
                {
                    builder.Append("You do not have enough of this security to make this transaction")
                        .Append("\n");
                }
            }
            else if (action.Equals("buy", StringComparison.OrdinalIgnoreCase))
            {
                var amountBought =  long.Parse(AmountEntry.Text);
                if (amountBought > (model.MarketCap / 2))
                {
                    builder.Append("It is highly unlikely you own half of this company")
                        .Append("\n");
                }

            }
        }

        private void ToggleUI()
        {
            this.IsEnabled = !this.IsEnabled;
            this.IsBusy = !this.IsBusy;
            this.Indicator.IsRunning = !this.Indicator.IsRunning;
        }


        public class ATPObj
        {
            public string Errors { get; set; }
            public SecuritiesModel StockModel { get; set; }

            public ATPObj(string errors, SecuritiesModel model)
            {
                this.Errors = errors;
                this.StockModel = model;
            }
        }   
    }
}