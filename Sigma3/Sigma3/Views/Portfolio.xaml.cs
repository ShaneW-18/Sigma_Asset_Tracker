using Sigma3.Objects;
using Sigma3.Util;
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
    public partial class Portfolio : ContentPage
    {
        private readonly User USER_LOGGED_IN;
        public Portfolio()
        {
            InitializeComponent();
            USER_LOGGED_IN = MainPage.USER_LOGGED_IN;

        }
        protected override void OnAppearing()
        {
            
            if (USER_LOGGED_IN.UserPortfolio == null || USER_LOGGED_IN.UserPortfolio.Count == 0)
            {
                this.NoUserPortfolio.IsEnabled = true;
                this.NoUserPortfolio.IsVisible = true;
                this.UserPortfolio.IsVisible = false;
                this.UserPortfolio.IsEnabled = false;
            }
            this.PORTFOLIO_BALANCE.Text = $"${StringUtils.ParseNumberWithCommas(USER_LOGGED_IN.PortfolioBalance)}";
            this.BindingContext = USER_LOGGED_IN.UserFollowing;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void AddPortfolioBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTooPortfolioPage());
        }

    }
}