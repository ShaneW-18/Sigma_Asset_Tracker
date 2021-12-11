using Sigma3.Objects;
using Sigma3.Services.Web;
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
    public partial class SearchPage : ContentPage
    {
        String symbol = "";
        public SearchPage()
        {
            InitializeComponent();
        }

        private async void SearchStock_Clicked(object sender, EventArgs e)
        {
            try
            {
                StockModel s = await YahooFinance.GetAsync(symbol);
               

                    await Navigation.PushAsync(new StockViewPage(s));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                await DisplayAlert("Alert", "Symbol does not exist", "OK");
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            symbol = Convert.ToString(((Entry)sender).Text);
        }
    }
}