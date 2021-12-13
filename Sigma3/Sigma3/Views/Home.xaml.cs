using Sigma3.Objects;
using Sigma3.Util;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sigma3.Services.Web;
using Plugin.Connectivity;

namespace Sigma3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private readonly User USER_LOGGED_IN;
    
        public Home()
        {
            InitializeComponent();
            USER_LOGGED_IN = MainPage.USER_LOGGED_IN;
            this.BindingContext = new SecuritiesModel();
        }



        async protected override void OnAppearing()
        {
            if (!USER_LOGGED_IN.porfolioHidden)
            {
                this.USER_NAME.Text = $"Welcome {USER_LOGGED_IN.Name}! ";
                this.TODAYS_DATE.Text = DateTime.Now.ToString("d MMM, ddd");
                this.PORTFOLIO_BALANCE.Text = $"${StringUtils.ParseNumberWithCommas(USER_LOGGED_IN.PortfolioBalance)}";
            }
                await GetHome();
            
          
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        async private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            ToggleUI();
            if (!CrossConnectivity.Current.IsConnected)
                await DisplayAlert("Error", "no internet", "OK");
            else
                await GetHome(true);
            ToggleUI();

        }

        async private Task GetHome(bool refresh = false)
        {

                var list = await SecuritiesApi.GetHomePageSecurities(refresh);
                this.USER_NAME.Text = $"Welcome {USER_LOGGED_IN.Name}! ";
                this.TODAYS_DATE.Text = DateTime.Now.ToString("d MMM, ddd");
               
            

                this.TopGainers.ItemsSource = list.TopGainers;
                this.TopLosers.ItemsSource = list.TopLosers;
                this.MostActive.ItemsSource = list.MostActive;
                this.Crypto.ItemsSource = list.Crypto;
            

        }

        private void ToggleUI()
        {
            this.IsEnabled = !this.IsEnabled;
            this.IsBusy = !this.IsBusy;
            this.Indicator.IsRunning = !this.Indicator.IsRunning;
        }

        async private void AddToPortfolio_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTooPortfolioPage());

        }

        private async void MostActive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new StockViewPage(e.CurrentSelection[0] as SecuritiesModel));
        }

        private  void hidden_Clicked(object sender, EventArgs e)
        {
            USER_LOGGED_IN.porfolioHidden = !USER_LOGGED_IN.porfolioHidden;

            if (USER_LOGGED_IN.porfolioHidden)
            {
                this.PORTFOLIO_BALANCE.Text = "$--.--";
                this.Hidden.IconImageSource = "closedEye.png";
                
            }
            else
            {
                this.PORTFOLIO_BALANCE.Text = $"${StringUtils.ParseNumberWithCommas(USER_LOGGED_IN.PortfolioBalance)}";
                this.Hidden.IconImageSource = "openEye.png";
            }

        }
    }
}