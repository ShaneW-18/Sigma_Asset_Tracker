using Sigma3.Objects;
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
    }
}