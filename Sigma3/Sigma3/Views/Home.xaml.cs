using BankingExplorationApp.ViewModels;
using Sigma3.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooFinanceApi;

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
        

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void topMoversListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}