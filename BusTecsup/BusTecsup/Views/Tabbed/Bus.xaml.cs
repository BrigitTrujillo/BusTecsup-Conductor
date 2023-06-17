using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusTecsup.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bus : ContentPage
    {
        public Bus()
        {
            InitializeComponent();
        }
        private void OnMenuButtonClicked(object sender, EventArgs e)
        {
            MenuListView.IsVisible = !MenuListView.IsVisible; // Mostrar u ocultar la lista de opciones
        }
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