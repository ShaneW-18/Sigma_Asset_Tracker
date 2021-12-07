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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void ClickedLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());

        }
        private async void ClickedRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());

        }
        protected override void OnAppearing()
        {
           
        }
    }
}