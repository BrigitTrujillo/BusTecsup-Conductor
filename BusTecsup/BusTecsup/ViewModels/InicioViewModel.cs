using BusTecsup.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace BusTecsup.ViewModels
{
    public class InicioViewModel : INotifyPropertyChanged
    {
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

        public async Task GetAndDisplayCurrentLocation()
        {
            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                CurrentLocation = new Position(location.Latitude, location.Longitude);

                var conductor = new Conductor
                {
                    Latitud = location.Latitude.ToString(),
                    Longitud = location.Longitude.ToString()
                };

                var json = JsonConvert.SerializeObject(conductor);
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