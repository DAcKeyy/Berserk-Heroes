using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berserk.Messaging.Messages
{
    public class Authorization
    {
        public bool isRestorePassword;
        public string AccessToken;
        public string Email;
        public string Password;
        public string Nickname;

        public Authorization()
        {
            isRestorePassword = false;
            AccessToken = "";
            Email = "";
            Password = "";
            Nickname = "";
        }
        public Authorization(bool passRestore, string accessToken, string email, string password, string nickname)
        {
            isRestorePassword = passRestore;
            AccessToken = accessToken;
            Email = email;
            Password = password;
            Nickname = nickname;
        }
        
        public Authorization(string accessToken, string email, string password, string nickname)
        {
            AccessToken = accessToken;
            Email = email;
            Password = password;
            Nickname = nickname;
        }
        
        public Authorization(bool passRestore, string email, string password, string nickname)
        {
            isRestorePassword = passRestore;
            Email = email;
            Password = password;
            Nickname = nickname;
        }
        
        public Authorization(string email, string password, string nickname)
        {
            Email = email;
            Password = password;
            Nickname = nickname;
        }
    }
}