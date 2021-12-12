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
        private List<Nasdaq> Nasdaq { get; set; } = new List<Nasdaq>();

        public SearchPage()
        {
            InitializeComponent();
        }



       async protected override void OnAppearing()
        {
            if (Nasdaq.Count == 0)
            {
                await nas();
            }
        }
        private async void SearchStock_Clicked(object sender, EventArgs e)
        {

            if (this.SearchEntry == null || String.IsNullOrWhiteSpace(this.SearchEntry.Text)) return;
            var Symbol = this.SearchEntry.Text;

            var security = await SecuritiesApi.GetAsync(Symbol);

            if (security == null)
            {
                await DisplayAlert("Alert", "Symbol does not exist", "OK");
                return;
            }
            
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            this.Indicator.IsRunning = true;
            var entry = sender as Entry;
            var Symbol = entry.Text;

           
            var v =  Nasdaq.Where(xr => xr.symbol.ToUpper().StartsWith(Symbol.ToUpper()) || xr.companyName.ToUpper().StartsWith(Symbol.ToUpper())).Take(30);

            this.SearchResults.ItemsSource = v;
            this.Indicator.IsRunning = false;
           
            
        }
        async protected Task nas()
        { 
             Nasdaq = await SecuritiesApi.GetNasdaq();
        }
        private async void FollowingCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Nasdaq nas = e.CurrentSelection[0] as Nasdaq;
            var stock = await SecuritiesApi.GetAsync(nas.symbol);
            await Navigation.PushAsync(new StockViewPage(stock));

        }

        private async void SearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var stock = e.CurrentSelection[0] as Nasdaq;
            
            await Navigation.PushAsync(new StockViewPage(await SecuritiesApi.GetAsync(stock.symbol)));
        }
    }
}