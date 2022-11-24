using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Notes;
using Notes.Views;
using Xamarin.Forms;
using Notes.Controlls;

namespace Notes.ViewModel
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        public Action DisplayInvalidLoginPrompt;
        public Action OnSuccsess;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }

        public async void OnSubmit()
        {
            var user = await App.NotesDB.GetUserAsync(Email, Password);
            if (user == null)
            {
                DisplayInvalidLoginPrompt();
            }
            else
            {
                OnSuccsess();
            }
        }
    }
}
