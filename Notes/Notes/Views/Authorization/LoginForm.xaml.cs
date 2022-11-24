using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.ViewModel;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginForm : ContentPage
    {
        public LoginForm()
        {
            var vm = new LoginViewModel();
            BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            vm.OnSuccsess += ClearStack;
            InitializeComponent();

            _Email.Completed += (object sender, EventArgs e) =>
            {
                _Password.Focus();
            };

            _Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
            AutoLogin();
        }

        public void AutoLogin()
        {
            App.AuthenticateController.UserName = "User";
            Shell.Current.GoToAsync($"//{ nameof(NotesNews)}");
        }

        public void ClearStack()
        {
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page != null)
                {
                    Navigation.RemovePage(page);
                }
            }

            if (BindingContext is LoginViewModel vm)
            {
                App.AuthenticateController.UserName = vm.Email;
            }
            Shell.Current.GoToAsync($"//{ nameof(NotesNews)}");
        }
    }
}