using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace policiclo
{
    public partial class MainPage : ContentPage
    {
        public string WebApiKey = "AIzaSyBLGOVxBDyNc9m75N-8g09KPohWabUBhhk";
        public MainPage()
        {
            InitializeComponent();
        }
        async void inicioButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserLoginEmail.Text, UserLoginPassword.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedContent);
                await Navigation.PushAsync(new Dashboard());
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Correo o contraseña inválida", "OK");
            }
        }

        async void openRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }
    }
}
