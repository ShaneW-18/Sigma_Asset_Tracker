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
        private readonly User USER_LOGGED_IN = MainPage.USER_LOGGED_IN;
        SecuritiesModel stock1;
        private Task<SecuritiesModel> task;

        public StockViewPage(SecuritiesModel stock)
        {
           
            InitializeComponent();
            stock1 = stock;
            if (USER_LOGGED_IN.UserFollowing.Contains(stock))
            {
                star.IconImageSource = "filledStar.png";
            }
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


        private void star_Clicked(object sender, EventArgs e)
        {
            if (!USER_LOGGED_IN.UserFollowing.Contains(stock1))
            {
                USER_LOGGED_IN.AddFollowing(stock1.Symbol);
                star.IconImageSource = "filledStar.png";
            }
            else
            {
                USER_LOGGED_IN.RemoveFollowing(stock1.Symbol);
                star.IconImageSource = "star.png";
            }
            

            
 
        }
    }
}