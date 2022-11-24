using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.Views;
using Xamarin.Essentials;

namespace Notes
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddNoteNewsPage), typeof(AddNoteNewsPage));
            Routing.RegisterRoute(nameof(LoginForm), typeof(LoginForm));
            Routing.RegisterRoute(nameof(GalleryView), typeof(GalleryView));
        }
    }

}