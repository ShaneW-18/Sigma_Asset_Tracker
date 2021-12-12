using Sigma3.Objects;
using Sigma3.Util;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sigma3.Services.Web;

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

        

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            
        }

        private void topMoversListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }


        async protected override void OnAppearing()
        {
            this.USER_NAME.Text = $"Welcome {USER_LOGGED_IN.Name}! ";
            this.TODAYS_DATE.Text = DateTime.Now.ToString("d MMM, ddd");
            this.PORTFOLIO_BALANCE.Text = $"${StringUtils.ParseNumberWithCommas(USER_LOGGED_IN.PortfolioBalance)}";

            var list = await SecuritiesApi.GetHomePageSecurities();
            this.TopGainers.ItemsSource = list.TopGainers;
            this.TopLosers.ItemsSource = list.TopLosers;
            this.MostActive.ItemsSource = list.MostActive;
            this.Crypto.ItemsSource = list.Crypto;
          
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}