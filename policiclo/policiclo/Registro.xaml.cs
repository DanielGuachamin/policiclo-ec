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
    public partial class Registro : ContentPage
    {
        public string WebApiKey = "AIzaSyBLGOVxBDyNc9m75N-8g09KPohWabUBhhk";
        public Registro()
        {
            InitializeComponent();
        }

        async void registrarButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            try
            {
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(userNewEmail.Text, userNewPassword.Text);
                await authProvider.UpdateProfileAsync(auth.FirebaseToken, userNewEmail.Text, "");
                await App.Current.MainPage.DisplayAlert("Alert", "Revise su correo para validar la cuenta", "OK");
            }
            catch (Exception ex)
            {
                var errorMessage="";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                if (ex.Message.Contains("MISSING_PASSWORD"))
                {
                    errorMessage = "Debe ingresar una contraseña";
                }
                else if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    errorMessage = "Ya existe una persona registrada con este correo";

                }else if (ex.Message.Contains("INVALID_EMAIL"))
                {
                    errorMessage = "Verifique el correo ingresado";

                }


                else if (ex.Message.Contains("WEAK_PASSWORD"))
                {
                    errorMessage = "La contraseña debe tener al menos 6 caractéres.";

                }
                else 
                {
                   errorMessage = "Verifique las credenciales de registro";

                }
                
                    System.Diagnostics.Debug.WriteLine(errorMessage);
                
                // let errorMesa;
                // errorMesa="mostrar"
                await DisplayAlert("Error", errorMessage, "OK");
                //                await DisplayAlert("Alert", ex.Message.ToString(), "OK");
                // await App.Current.MainPage.DisplayAlert("Alert", "Debe ingresar correo y contraseña válidos", "OK");
            }
        }
        void volverButton_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}