using Sigma3.Objects;
using Sigma3.Util;
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
    public partial class StockViewPage : ContentPage
    {
        public StockViewPage(StockModel stock)
        {
            InitializeComponent();
            Price.Text =  stock.RegularMarketPriceProp;
            Symbol.Text = stock.Symbol;
            Name.Text = stock.ShortName;
            PreviousCloseT.Text = "$" + StringUtils.ParseNumberWithCommas(stock.RegularMarketPreviousClose).ToString();
            OpenT.Text = "$" + StringUtils.ParseNumberWithCommas(stock.RegularMarketOpen).ToString();
            MarketCapT.Text = "$" + StringUtils.ParseNumberWithCommas((double)stock.MarketCap).ToString();
            YearLowT.Text = "$" + StringUtils.ParseNumberWithCommas(stock.FiftyTwoWeekLowChange).ToString() ;
            YearHighT.Text = "$" + StringUtils.ParseNumberWithCommas(stock.FiftyTwoWeekHighChange).ToString();
            VolT.Text = StringUtils.ParseNumberWithCommas((double)stock.AverageDailyVolume10Day).ToString();

        }
    }
}