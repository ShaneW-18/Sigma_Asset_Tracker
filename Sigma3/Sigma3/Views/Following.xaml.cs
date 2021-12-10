using Sigma3.Objects;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sigma3.Services.Web;

namespace Sigma3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Following : ContentPage
    {
        private readonly User USER_LOGGED_IN;

        public Following()
        {
            USER_LOGGED_IN = MainPage.USER_LOGGED_IN;
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        protected override void OnAppearing()
        {

            this.FollowingCollectionView.ItemsSource = USER_LOGGED_IN.UserFollowing;

        }

        async private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            ToggleUI();
            var list = await YahooFinance.GetUpdate(USER_LOGGED_IN.UserFollowing);
            USER_LOGGED_IN.UserFollowing = list;
            this.FollowingCollectionView.ItemsSource = USER_LOGGED_IN.UserFollowing;
            ToggleUI();
        }

        private void ToggleUI()
        {
            this.IsEnabled = !this.IsEnabled;
            this.IsBusy = !this.IsBusy;
            this.Indicator.IsRunning = !this.Indicator.IsRunning;
        }
    }
}