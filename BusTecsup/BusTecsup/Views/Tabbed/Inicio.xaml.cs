using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace BusTecsup.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
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
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if (status != PermissionStatus.Granted)
            {
                // El usuario no otorgó el permiso de ubicación, manejar la situación aquí
            }
            else
            {
                // El usuario otorgó el permiso de ubicación, puedes acceder a la ubicación aquí
            }
        }




    }
}