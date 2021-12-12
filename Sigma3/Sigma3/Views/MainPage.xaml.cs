using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Sigma3.Objects;
using Xamarin.Essentials;

namespace Sigma3
{
    public partial class MainPage : TabbedPage
    {
        public static User USER_LOGGED_IN { get; set; }
        public MainPage(User user)
        {
            USER_LOGGED_IN = user;
            InitializeComponent();
            BarBackgroundColor = Color.Black;
            BarTextColor = Color.Black;
        }

        public MainPage()
        {
            InitializeComponent();
            BarBackgroundColor = Color.Black;
            BarTextColor = Color.Black;
        }
    }
}
