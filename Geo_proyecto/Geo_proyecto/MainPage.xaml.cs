using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Geo_proyecto
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void btn_Denisse(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Noemi());
        }

        private void btn_Noemi(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Noemi());
        }
    }
}
