using Sigma3.Objects;
using Sigma3.Services.Web;
using System;
using System.Collections.Generic;
using System.IO;
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
        private List<Nasdaq> nasdaq { get; set; } = new List<Nasdaq> ();
       

        String symbol = "";
        public SearchPage()
        {
            InitializeComponent();
        }



       async protected override void OnAppearing()
        {
            if (nasdaq.Count == 0)
            {
                await nas();
            }
        }
        private async void SearchStock_Clicked(object sender, EventArgs e)
        {
            try
            {
                SecurityModel s = await YahooFinance.GetAsync(symbol);
               

                    await Navigation.PushAsync(new StockViewPage(s));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                await DisplayAlert("Alert", "Symbol does not exist", "OK");
            }
        }

        async private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            this.Indicator.IsRunning = true;
            symbol = Convert.ToString(((Entry)sender).Text).ToUpper();

           
            var v =  nasdaq.Where(xr => xr.symbol.ToUpper().StartsWith(symbol.ToUpper()) | xr.companyName.ToUpper().StartsWith(symbol.ToUpper())).Take(30);

            this.SearchResults.ItemsSource = v;
            this.Indicator.IsRunning = false;
           
            
        }
        async protected Task nas()
        { 
             nasdaq = await YahooFinance.GetNasdaq();
        }
        private async void FollowingCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Nasdaq nas = e.CurrentSelection[0] as Nasdaq;
            StockModel stock = await YahooFinance.GetAsync(nas.symbol);
            await Navigation.PushAsync(new StockViewPage(stock));

        }
    }
}