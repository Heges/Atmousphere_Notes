using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.Views;
using Notes.Data;
using System.IO;
using Notes.Controlls;
using Android.App;
using Android.OS;
using Xamarin.Essentials;
using Java.IO;


namespace Notes.Views
{
    public partial class App : Xamarin.Forms.Application
    {
        static DateBase notesDB;
        static AuthentificateHelper authenticateController;
        const string DBNAME = "NotesDatabase.db3";
        public static DateBase NotesDB 
        {
            get
            {
                if (notesDB == null)
                {
                    
                    notesDB = new DateBase(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), DBNAME));
                }
                return notesDB;
            }
        }

        public static AuthentificateHelper AuthenticateController
        {
            get
            {
                if (authenticateController == null)
                {
                    authenticateController = new AuthentificateHelper();
                }
                return authenticateController;
            }
        }

        public App()
        {
            InitializeComponent();
            
            MainPage = new AppShell();
            //MainPage = new MainPage();
            //var c = NotesDB;
            //c.SaveNotAsync(new Models.Note() { Date = DateTime.Now, Text = DateTime.UtcNow.ToString() });
        }

        protected async override void OnStart()
        {
            //var authResult = await WebAuthenticator.AuthenticateAsync(
            //    new Uri("AutorationURL"),
            //    new Uri("RedirectURL"));
            //Java.IO.File dbFile = new Java.IO.File(System.IO.Path.Combine(System.Environment.GetFolderPath‌​(Syst‌​em.Environmen‌​t.Speci‌​alFolder.LocalApplicationData), DBNAME));
            //var accessToken = authResult?.AccessToken;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
