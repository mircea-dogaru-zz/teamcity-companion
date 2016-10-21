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
            IsGuest.Toggled += IsGuest_Toggled;
            LoginButton.Clicked += async (s, e) =>
            {
                AccountManager.Current.Login(Server.Text, Username.Text, Password.Text, IsGuest.IsToggled);
                await Navigation.PushModalAsync(new Projects());
            };
        }

        private void IsGuest_Toggled(object sender, ToggledEventArgs e)
        {
            Username.IsEnabled = !e.Value;
            Password.IsEnabled = !e.Value;
        }
    }
}
