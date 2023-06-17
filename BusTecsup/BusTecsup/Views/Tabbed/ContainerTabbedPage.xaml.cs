using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;


namespace BusTecsup.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContainerTabbedPage : Xamarin.Forms.TabbedPage
    {
        public ContainerTabbedPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSmoothScrollEnabled(true);
        }
    }
}