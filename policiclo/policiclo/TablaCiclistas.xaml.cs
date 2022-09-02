using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace policiclo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablaCiclistas : ContentPage
    {

        Ciclista fireRealtime = new Ciclista();
        public TablaCiclistas()
        {
            InitializeComponent();
        }
        //LEER LOS DATOS
        protected override async void OnAppearing()
        {
            var leer_datos = await fireRealtime.GetAll();
            CiclistaListView.ItemsSource = leer_datos;
            //await DisplayAlert("Alert", leer_datos.ToString, "OK");
        }

        protected async void ShowMapAsync(object sender, EventArgs e)
        {
            var coor = ((Button)sender).CommandParameter;
            var coordi = coor + ";";
            string[] coordina = coordi.Split(';');
            var latitud = double.Parse(coordina[0]);
            var longitud = double.Parse(coordina[1]);
            MapLaunchOptions options = new MapLaunchOptions { Name = "Posicion ciclista" };
            await Map.OpenAsync(latitud, longitud, options);
        }
    }
}