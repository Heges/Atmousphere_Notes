using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.ViewModel;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceCatalog : ContentPage
    {
        public ServiceCatalog()
        {
            BindingContext = new ServiceViewModel();
            InitializeComponent();
        }
    }
}