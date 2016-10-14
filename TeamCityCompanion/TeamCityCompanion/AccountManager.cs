using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamCityCompanion
{
    public class AccountManager
    {
        private static readonly AccountManager Instance = new AccountManager();

        public static AccountManager Current => Instance;

        public string Username { get; private set; }
        public string Password { get; private set; }
        
        public void Login(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
