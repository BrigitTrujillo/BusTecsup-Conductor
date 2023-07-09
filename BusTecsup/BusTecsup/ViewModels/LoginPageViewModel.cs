using BusTecsup.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Security;
using Xamarin.Essentials;
using System.Security.Cryptography.X509Certificates;

namespace BusTecsup.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {


        private readonly HttpClient httpClient;

        public string Usuario { get; set; }
        public string Contraseña { get; set; }

        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginPageViewModel()
        {

            httpClient = new HttpClient();

            LoginCommand = new Command(async () => await Login());



            // Verificar si el usuario ya ha iniciado sesión anteriormente
            var isLoggedIn = Preferences.Get("IsLoggedIn", false);
            if (isLoggedIn)
            {
                Application.Current.MainPage = new Views.Tabbed.ContainerTabbedPage();
            }
        }

        private async Task Login()
        {
            var conductor = new Conductor
            {
                Usuario = Usuario,
                Contraseña = Contraseña
            };

            var json = JsonConvert.SerializeObject(conductor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api-node-bus.onrender.com/api/conductores/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var authenticatedConductor = JsonConvert.DeserializeObject<Conductor>(responseContent);


                // Almacenar el estado de inicio de sesión
                Preferences.Set("IsLoggedIn", true);

                // Procesar el conductor autenticado, por ejemplo, almacenarlo en una sesión o pasar a otra página

                Application.Current.MainPage = new Views.Tabbed.ContainerTabbedPage();
            }
            else
            {
                // El login falló, mostrar mensaje de error
                var errorMessage = "Error en el login. Por favor, verifica tus credenciales.";
                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
            }
        }

    }
}