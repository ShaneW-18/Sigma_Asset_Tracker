using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sigma3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTooPortfolioPage : ContentPage
    {
        private Button buttonSelected { get; set; }
        public AddTooPortfolioPage()
        {
            InitializeComponent();
           
            this.BuyButton.IsEnabled = true;
            this.buttonSelected = this.BuyButton;
            
        }

       

        private void BuyButton_Clicked(object sender, EventArgs e)
        {
            // Buy
            this.BuyButton.BackgroundColor = Color.LightGreen;
            this.BuyButton.FontAttributes = FontAttributes.Bold;
            this.BuyButton.TextColor = Color.White;
            this.BuyButton.BorderColor = Color.LightGreen;

            // Sell
            this.SellButton.BackgroundColor = Color.Black;
            this.SellButton.BorderColor = Color.Gray;
            this.SellButton.TextColor = Color.Gray;

            // Entry
            this.AmountEntry.Placeholder = "Amount Bought";

        }

        private void SellButton_Clicked(object sender, EventArgs e)
        {
            // Sell
            this.SellButton.BackgroundColor = Color.Red;
            this.SellButton.FontAttributes = FontAttributes.Bold;
            this.SellButton.TextColor = Color.White;
            this.SellButton.BorderColor = Color.LightGreen;

            // Buy
            this.BuyButton.BackgroundColor = Color.Black;
            this.BuyButton.BorderColor = Color.Gray;
            this.BuyButton.TextColor = Color.Gray;

            // Entry
            this.AmountEntry.Placeholder = "Amount Sold";
            
        }
       
    }
}