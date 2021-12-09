using Sigma3.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sigma3.Services;
using Xamarin.Forms;
using System.Security.Cryptography;
using Xamarin.Forms.Xaml;
using Sigma3.Util;
using YahooFinanceApi;

namespace Sigma3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private int CurrentErrors = 0;
        private Dictionary<int, string> ErrorReasons = new Dictionary<int, string>();

        private List<Entry> TextEntries;
        public Register()
        {
          
            InitializeComponent();
            TextEntries = new List<Entry>()
            {
                Name,
                Email,
                Password,
                VPassword,
                PhoneNumber
            };
            NavigationPage.SetHasNavigationBar(this, false);
        }
        async private void ClickedRegister(object sender, EventArgs e)
        {
           for (int i = 0; i < TextEntries.Count; i++)
            {
                var entry = TextEntries[i].Text;

                if (String.IsNullOrWhiteSpace(entry))
                {
                    AddReason($"Your {TextEntries[i].Placeholder} field is empty");
                    continue;
                }

                switch (i)
                {
                    case 0:
                        var count = entry.Length;
                        if (count < 2)
                        {
                            AddReason("Your name is less than 2 characters");
                        }
                        else if (count >= 35)
                        {
                            AddReason("Your name is too long. Use an abreviation");
                        }
                        continue;
                    // Email
                    case 1:
                        if (!isValidEmail(entry))
                        {
                            AddReason("Your email is incorrect");
                        }
                     
                        // perform regex check

                        continue;
                        // Password
                    case 2:
                        if (!isValidPassword(entry))
                        {
                             AddReason("Password field must be contain a digit (4-20), a number, special char, upper and lower case letter at least once!");
                        }

                        continue;
                      // Password Validator
                    case 3:
                       if (!entry.Equals(Password.Text))
                       {
                            AddReason("Passwords do not match!");
                       }
                       continue;
                        // Phone Number
                    case 4:
                        if (!isValidPhoneNumber(entry))
                        {
                            AddReason("Phone number is not valid");
                        }
                        break;

                }

            }

            var keys = ErrorReasons.Keys;
            
           
            if (keys.Count > 0)
            {
                var builder = new StringBuilder();

                foreach (var key in keys)
                {
                    var error = ErrorReasons[key];
                    builder.Append(error)
                        .Append("\n");
                }


                await DisplayAlert($"You have {keys.Count} errors", builder.ToString(), "OK");
                // Should be 
                CurrentErrors = 0;
                ErrorReasons = new Dictionary<int, string>();
                return;
            }



            await AppService.AddUserAsync(new User
            {
                Name = Name.Text,
                Email = Email.Text,
                Password = StringUtils.HashString(Password.Text),
                PhoneNumber = PhoneNumber.Text,
                PortfolioBalance = 0
            });

            // add to database logic here
            await Navigation.PushAsync(new MainPage());
        }


        public void AddReason(string reason)
        {
            ErrorReasons.Add(CurrentErrors++, reason);
        }

        private bool isValidEmail(string email)
        {          
           return new Regex("^(.+)@(.+)$").IsMatch(email);
        }

        private bool isValidPhoneNumber(string phone)
        {
            return new Regex("^(\\+\\d{1,3}( )?)?((\\(\\d{1,3}\\))|\\d{1,3})[- .]?\\d{3,4}[- .]?\\d{4}$").IsMatch(phone);
        }

        private bool isValidPassword(string password)
        {
            return new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,20}$").IsMatch(password);
        }



        protected override void OnAppearing()
        {
            VPassword.Text = "";
            Email.Text = "";
            Password.Text = "";
            PhoneNumber.Text = "";


            // Should be 
            CurrentErrors = 0;
            ErrorReasons = new Dictionary<int, string>();
        }

        async private void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}