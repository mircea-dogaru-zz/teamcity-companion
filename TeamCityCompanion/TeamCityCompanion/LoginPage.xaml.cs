using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TeamCityCompanion
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void OnLogin()
        {
            AccountManager.Current.Login(Username.Text, Password.Text);
        }
    }
}
