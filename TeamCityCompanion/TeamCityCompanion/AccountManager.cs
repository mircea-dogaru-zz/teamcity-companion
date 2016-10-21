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

        public string Server { get; private set; }

        public bool IsGuest { get; private set; }
        
        public void Login(string server, string username, string password, bool isGuest)
        {
            Username = username;
            Password = password;
            Server = server;
            IsGuest = isGuest;
        }
    }
}
