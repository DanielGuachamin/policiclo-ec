using Firebase.Auth;
using Newtonsoft.Json;
using Plugin.Geolocator;
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
    public partial class Dashboard : ContentPage
    {
        public string WebApiKey = "AIzaSyBLGOVxBDyNc9m75N-8g09KPohWabUBhhk";
        double latin;
        double longi;
        string ubication;

        Ciclista fireRealtime = new Ciclista();
        public Dashboard()
        {
            InitializeComponent();

            GetProfileInformationAndRefreshToken();

            InitializePlugin();
        }
        private async void InitializePlugin()
        {
            if (!CrossGeolocator.IsSupported)
            {
                await DisplayAlert("Error", "ha ocurrido un error en el plugin", "ok");
                return;
            }
            if (!CrossGeolocator.Current.IsGeolocationEnabled || !CrossGeolocator.Current.IsGeolocationAvailable)
            {
                await DisplayAlert("Advertencia", "Revisar las configuraciones del GPS", "ok");
                return;
            }
            await CrossGeolocator.Current.StartListeningAsync(new TimeSpan(0, 0, 2), 0.10);
            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
        }
        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
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
            ubication = lat.Text + ";" + longi;

        }
        private void Button_Regresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private async void mostrar_mapa(object sender, EventArgs e)
        {
            MapLaunchOptions options = new MapLaunchOptions { Name = "Mi posicion actual" };
            await Map.OpenAsync(latin, longi, options);
        }

        private async void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            try
            {
                var savedFirebaseAuth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedFirebaseAuth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));
                userName.Text = "Bienvenid@ a CicloPoli";
                UserCiclista.Text = savedFirebaseAuth.User.Email;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Alert", "Rayos el Token expiro", "OK");
            }
        }

        async void verTabla_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TablaCiclistas());
        }

        void cerrarButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        private void Empezar(object sender, EventArgs e)
        {
            Device.StartTimer(TimeSpan.FromSeconds(15), () =>
            {
                automatico();

                return true;
            });
        }

        private async void automatico()
        {
            var savedFirebaseAuth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
            CiclistaModel ciclistaModel = new CiclistaModel();
            ciclistaModel.Longitud = log.Text;
            ciclistaModel.Latitud = lat.Text;
            ciclistaModel.Nombre = savedFirebaseAuth.User.Email;
            ciclistaModel.Ubication = ubication;

            var data = await fireRealtime.SaveCoordenadas(ciclistaModel);
            if (!data)
            {
                await DisplayAlert("Error", "no se guardo la gps", "ok");
            }

        }
    }
}