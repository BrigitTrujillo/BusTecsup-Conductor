using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Net.Http;

namespace BusTecsup.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();

            map.MyLocationEnabled = true;

            GetAndDisplayCurrentLocation();

            if (Device.RuntimePlatform == Device.Android && Preferences.Get("LocationServiceRunning", false) == false)
            {
                StartService();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.Android && Preferences.Get("LocationServiceRunning", false) == false)
            {
                CheckAndRequestLocationPermission();
            }
        }
        private async void CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                var result = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (result == PermissionStatus.Granted)
                {
                    StartService();
                }
                else
                {
                    // El usuario no concedió los permisos de ubicación, puedes mostrar un mensaje o realizar una acción alternativa
                }
            }
            else
            {
                StartService();
            }
        }
        private async void GetAndDisplayCurrentLocation()
        {
            var location = await Geolocation.GetLocationAsync();

            if (location != null)


            {

                var ubicacion = new
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                };

                var json = JsonConvert.SerializeObject(ubicacion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync("http://tu-api.com/enviar-ubicacion", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // La ubicación se envió correctamente a la API
                    }
                    else
                    {
                        // Ocurrió un error al enviar la ubicación a la API
                    }
                }

                var MiUbi = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Mi Ubicación",
                    Position = new Position(location.Latitude, location.Longitude)
                };

                map.Pins.Add(MiUbi);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(MiUbi.Position, Distance.FromMeters(500)));
            }
        }

        private void OnMenuButtonClicked(object sender, EventArgs e)
        {
            MenuListView.IsVisible = !MenuListView.IsVisible; // Mostrar u ocultar la lista de opciones
        }

        // En la página modal del menú

        private void OnOptionSelected(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedOption = button.Text;

            // Realizar acción según la opción seleccionada

            MenuListView.IsVisible = false;
        }

        public class MenuItem
        {
            public string Text { get; set; }
            public string ImageSource { get; set; }
        }

        private void StartService()
        {
           var startServiceMessage = new Models.StartServiceMessage();
            MessagingCenter.Send(startServiceMessage, "ServiceStarted");
            Preferences.Set("LocationServiceRunning", true);
        }
    }
}
       



