using BankingExplorationApp.ViewModels;
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
            this.BindingContext = new topMoversModel();

        }
        protected override void OnAppearing()
        {
            this.PORTFOLIO_BALANCE.Text = $"${StringUtils.ParseNumberWithCommas(USER_LOGGED_IN.PortfolioBalance)}";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void AddPortfolioBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}