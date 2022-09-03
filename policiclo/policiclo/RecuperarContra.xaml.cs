using Firebase.Auth;
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
    public partial class RecuperarContra : ContentPage
    {
        public string WebApiKey = "AIzaSyBLGOVxBDyNc9m75N-8g09KPohWabUBhhk";
        public RecuperarContra()
        {
            InitializeComponent();
        }

        private async void recoverButton_Clicked(object sender, EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            string email = TxtCorreo.Text;
            try
            {
                await authProvider.SendPasswordResetEmailAsync(email);
                await DisplayAlert("Correo enviado", "Se envió un enlace a su correo, revise la carpeta de SPAM", "OK");
            }
            catch
            {
                await DisplayAlert("Alert", "Debe completar los campos", "OK");
            }
            
        }
    }
}