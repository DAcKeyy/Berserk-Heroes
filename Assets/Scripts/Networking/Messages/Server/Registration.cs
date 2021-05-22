namespace Berserk.Messaging.Messages
{
    public class Registration
    {
        public string AccessToken;
        public string Email;
        public string Password;
        public string Nickname;

        public Registration()
        {
            AccessToken = "";
            Email = "";
            Password = "";
            Nickname = "";
        }
        public Registration(string accessToken, string email, string password, string nickname)
        {
            AccessToken = accessToken;
            Email = email;
            Password = password;
            Nickname = nickname;
        }
        
        public Registration( string email, string password, string nickname)
        {
            Email = email;
            Password = password;
            Nickname = nickname;
        }
    }
}