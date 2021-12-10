using Sigma3.Objects;
using Sigma3.Util;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            MainPage.PageTitle = "Home";
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
            this.BindingContext = USER_LOGGED_IN.UserFollowing;
     
        }


     
    }
}