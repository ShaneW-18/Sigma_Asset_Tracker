using BankingExplorationApp.ViewModels;
using Sigma3.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;

namespace Sigma3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {

        public Home()
        {
            InitializeComponent();
            this.BindingContext = new topMoversModel();

        }
        async private void StockSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
        async private void Topmovers()
        {
           // var topMovers = await YahooFinanceApi.Yahoo.
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}