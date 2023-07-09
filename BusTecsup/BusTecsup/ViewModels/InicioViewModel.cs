using BusTecsup.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace BusTecsup.ViewModels
{
    public class InicioViewModel : INotifyPropertyChanged
    {
        public InicioViewModel()
        {
            // Resto del código del constructor...
            RunLocationUpdateLoop(); // Inicia el bucle de actualización de ubicación
        }


        private string usuario;
        public string Usuario
        {
            get { return usuario; }
            set
            {
                if (usuario != value)
                {
                    usuario = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Usuario)));
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private Position currentLocation;
        public Position CurrentLocation
        {
            get { return currentLocation; }
            set
            {
                if (currentLocation != value)
                {
                    currentLocation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLocation)));
                    UpdatePin();
                 

                }
            }
        }

        private Pin currentLocationPin;
        public Pin CurrentLocationPin
        {
            get { return currentLocationPin; }
            set
            {
                if (currentLocationPin != value)
                {
                    currentLocationPin = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLocationPin)));
                }
            }
        }

        public Xamarin.Forms.GoogleMaps.Map Map { get; set; }

        public async Task<bool> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                var result = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                return result == PermissionStatus.Granted;
            }

            return true;
        }
        public async Task RunLocationUpdateLoop()
        {
            while (true)
            {
                await GetAndDisplayCurrentLocation();
                await Task.Delay(1000); // Espera 1 segundo antes de la siguiente actualización
            }
        }

        public async Task GetAndDisplayCurrentLocation()
        {
            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                CurrentLocation = new Position(location.Latitude, location.Longitude);

                var conductor = new Conductor
                {

                    Latitud = location.Latitude,
                    Longitud = location.Longitude
                };
                Debug.WriteLine(conductor);
                var json = JsonConvert.SerializeObject(conductor);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())


                {
                    var UrlAPI = $"https://api-node-bus.onrender.com/api/conductores/ubicacion/{Usuario}";
                    Debug.WriteLine(UrlAPI);
                    var response = await httpClient.PutAsync(UrlAPI, content);

                   

                    if (response.IsSuccessStatusCode)
                    {
                        // La ubicación se envió correctamente a la API

                        Debug.WriteLine("la aplicacion se envio correctamente a la api");
                    }
                    else
                    {
                        Debug.WriteLine("la aplicacion NO se envio correctamente a la api");
                        // Ocurrió un error al enviar la ubicación a la API
                    }
                }
            }
        }

        private void UpdatePin()
        {
            var pin = new Pin
            {
                Type = PinType.Place,
                Label = "Mi Ubicación",
                Position = CurrentLocation
            };

            CurrentLocationPin = pin;
        }
    }
}