using BankingExplorationApp.ViewModels;
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
    public partial class Following : ContentPage
    {
        public Following()
        {
            InitializeComponent();
            this.BindingContext = new topMoversModel();
        }
    }
}