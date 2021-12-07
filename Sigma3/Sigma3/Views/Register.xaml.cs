using Sigma3.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sigma3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        String email, password, vPassword, phoneNumber;
        public Register()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void ClickedRegister(object sender, EventArgs e)
        {
            Activity.IsRunning = true;
            
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if ((email.Equals("") | password.Equals("") | phoneNumber.Equals("") | !reg.IsMatch(email)) && password.Equals(vPassword))
            {
                await DisplayAlert("Alert", "Cridentials are wrong", "OK");
            }
            else
            {
                User u = new User(password, email, phoneNumber);
                await Navigation.PushAsync(new MainPage());
            }
            Activity.IsRunning = false;   


        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = Convert.ToString(Email.Text);
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            password = Convert.ToString(Password.Text); 
        }

        private void VPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            vPassword = Convert.ToString(VPassword.Text);
        }

        private void PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            phoneNumber = Convert.ToString(PhoneNumber.Text);   
        }
        protected override void OnAppearing()
        {
            VPassword.Text = "";
            Email.Text = "";
            Password.Text = "";
            PhoneNumber.Text = "";


        }
    }
}