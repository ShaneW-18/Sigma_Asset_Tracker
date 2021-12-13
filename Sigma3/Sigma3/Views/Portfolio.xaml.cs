using Sigma3.Objects;
using Sigma3.Services.Web;
using Sigma3.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Sigma3.Objects.User;

namespace Sigma3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Portfolio : ContentPage
    {
        private readonly User USER_LOGGED_IN;
        public Portfolio()
        {
            InitializeComponent();
            USER_LOGGED_IN = MainPage.USER_LOGGED_IN;
            this.BindingContext = new UserPortfolioObject();

        }
        async protected override void OnAppearing()
        {
          

                // Could convert to function
                if (USER_LOGGED_IN.UserPortfolio == null || USER_LOGGED_IN.UserPortfolio.Count == 0)
                {
                    this.NoUserPortfolio.IsEnabled = true;
                    this.NoUserPortfolio.IsVisible = true;
                    this.UserPortfolio.IsVisible = false;
                    this.UserPortfolio.IsEnabled = false;
                }
                else
                {
                    this.NoUserPortfolio.IsEnabled = false;
                    this.NoUserPortfolio.IsVisible = false;
                    this.UserPortfolio.IsVisible = true;
                    this.UserPortfolio.IsEnabled = true;
                }
                this.PORTFOLIO_BALANCE.Text = $"${StringUtils.ParseNumberWithCommas(USER_LOGGED_IN.PortfolioBalance)}";
                this.PortfolioListView.ItemsSource = await USER_LOGGED_IN.GetUserPortfolio();
         
           
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void AddPortfolioBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTooPortfolioPage());
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        private async void RefreshButton_Clicked(object sender, EventArgs e)
        {
            ToggleUI();
          
            if (USER_LOGGED_IN.UserPortfolio != null || USER_LOGGED_IN.UserPortfolio.Count > 0)
            {
                this.PORTFOLIO_BALANCE.Text = $"${StringUtils.ParseNumberWithCommas(USER_LOGGED_IN.PortfolioBalance)}";
                this.PortfolioListView.ItemsSource = await USER_LOGGED_IN.GetUserPortfolio();
            }
            ToggleUI();


        }

        private async void PortfolioListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var stock = e.CurrentSelection[0] as UserPortfolioObject;

            await Navigation.PushAsync(new StockViewPage( await SecuritiesApi.GetAsync(stock.SecuritySymbol)));
        }
        private void ToggleUI()
        {
            this.IsEnabled = !this.IsEnabled;
            this.IsBusy = !this.IsBusy;
            this.Indicator.IsRunning = !this.Indicator.IsRunning;
        }

    }
}