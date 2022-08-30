using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Geo_proyecto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Noemi : ContentPage
    {
        double latin;
        double longi;

        public Noemi()
        {
            InitializeComponent();
            InitializePlugin();
        }

        private  async void InitializePlugin()
        {
            if (!CrossGeolocator.IsSupported)
            {
               await DisplayAlert("Error", "ha ocurrido un error en el plugin", "ok");
                return;
            }
            if (!CrossGeolocator.Current.IsGeolocationEnabled  || !CrossGeolocator.Current.IsGeolocationAvailable)
            {
              await  DisplayAlert("Advertencia", "Revisar las configuraciones del GPS", "ok");
                return;
            }
           await CrossGeolocator.Current.StartListeningAsync(new TimeSpan(0,0,2),0.10);
            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
           


        }

        private void Current_PositionChanged(object sender,Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {

            if (!CrossGeolocator.Current.IsListening)
            {
                return;
            }

            var position = CrossGeolocator.Current.GetPositionAsync();

            lat.Text = position.Result.Latitude.ToString();
            latin = double.Parse(lat.Text);
            log.Text = position.Result.Longitude.ToString();
            longi = double.Parse(log.Text);
        }

        private void Button_Regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private  async void mostrar_mapa(object sender, EventArgs e)

        { MapLaunchOptions options = new MapLaunchOptions { Name = "Mi posicion actual" };
            await Map.OpenAsync(latin, longi, options);
    


        }


    }
}