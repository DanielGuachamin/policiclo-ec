using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace policiclo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablaCiclistas : ContentPage
    {
        public TablaCiclistas()
        {
            InitializeComponent();
        }

        void volverButton_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Dashboard());
        }
    }
}