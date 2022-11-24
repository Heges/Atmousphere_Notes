using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Controlls
{
    public class AuthentificateHelper
    {
        private string userName;
        public string UserName
        {
            get => userName; 
            set
            {
                isAuthenticated = !string.IsNullOrWhiteSpace(value);
                userName = value;
            }
        }

        public bool isAuthenticated { get; private set; }

        public AuthentificateHelper()
        {

        }
    }
}
