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
using BusTecsup.ViewModels;

namespace BusTecsup.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        private InicioViewModel viewModel;

        public Inicio()
        {
            InitializeComponent();
            BindingContext = viewModel = new InicioViewModel();
            viewModel.Map = map;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.Android && await viewModel.CheckAndRequestLocationPermission())
            {
                await viewModel.GetAndDisplayCurrentLocation();

                var pin = viewModel.CurrentLocationPin;
                map.Pins.Add(pin);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMeters(500)));
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


    }
}
       



