using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma3.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sigma3.Objects;
using Sigma3.Util;
using Sigma3.Services.Web;
using Newtonsoft.Json;
using SQLite;
using Plugin.Connectivity;

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

        async private void ClickedLogin(object sender, EventArgs e)
        {
            ToggleUI();
            if (Constants.DEMO_ENABLED)
            {
                Constants.DEMO_USER.UserFollowing = await Constants.GetDefaultFollowing();
                await SecuritiesApi.GetHomePageSecurities();
                ToggleUI();
                await Navigation.PushAsync(new MainPage(Constants.DEMO_USER));
                return;
            }
            var errors = await HandleLogin();
            var User = errors.User;


            if (!String.IsNullOrEmpty(errors.Errors))
            {
                await DisplayAlert("Error", errors.Errors, "OK");
                ToggleUI();
                return;
            }

            if (User.UserFollowing.Count == 0)
            {
                User.UserFollowing = await Constants.GetDefaultFollowing();
            }

            await SecuritiesApi.GetHomePageSecurities();
            ToggleUI();
            await Navigation.PushAsync(new MainPage(User));


        }

        async private Task<LoginObj> HandleLogin()
        {
            var builder = new StringBuilder();
            var email = Email.Text;
            var password = Password.Text;



            if (String.IsNullOrWhiteSpace(email))
            {
                builder.Append("Email field is empty")
                    .Append("\n");
            }

            if (String.IsNullOrWhiteSpace(password))
            {
                builder.Append("Password field is empty");
            }

            if (!String.IsNullOrWhiteSpace(builder.ToString()))
            {
                return new LoginObj(null, builder.ToString());
            }

            var hashed = StringUtils.HashString(password);


            // Probbaly bad
            var UserMaybe = await AppService.GetUserByEmailAsync(email, hashed);

            return UserMaybe == null ? new LoginObj(UserMaybe, "Email or password is incorrect") : new LoginObj(UserMaybe, "");
        }

        private async void ClickedRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());

        }
        protected void OnAppearing()
        {
            Email.Text = "";
            Password.Text = "";
        }

        public class LoginObj
        {
            public User User { get; set; }
            public string Errors { get; set; }

            public LoginObj(User user, string Errors)
            {
                this.User = user;
                this.Errors = Errors;
            }

            public LoginObj() { }
        }

        private void ToggleUI()
        {
            this.IsEnabled = !this.IsEnabled;
            this.IsBusy = !this.IsBusy;
            this.Indicator.IsRunning = !this.Indicator.IsRunning;
        }


    }
}
