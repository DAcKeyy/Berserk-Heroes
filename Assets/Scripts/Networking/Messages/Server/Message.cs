using System;
using Berserk.Messaging;
using Berserk.Networking.Messages;

namespace Berserk.Messaging.Messages
{
    public class Message
    {
        public uint UserID { get; set; }
        public string AccessToken { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool Vaild { get; set; }
        public JsonMessage MessageBody { get; set; }
        
        public Message()
        {
            Text = "";
            UserID = 0;
            AccessToken = "";
            MessageBody = new JsonMessage();
            Date = DateTime.Now;
        }

        public Message(string text, uint userID, string accessToken, JsonMessage messageBody)
        {
            Text = text;
            UserID = userID;
            AccessToken = accessToken;
            MessageBody = messageBody;
            Date = DateTime.Now;
        }

        public Message(string text, bool isValid ,uint userID, string accessToken, JsonMessage messageBody)
        {
            Text = text;
            Vaild = isValid;
            UserID = userID;
            AccessToken = accessToken;
            MessageBody = messageBody;
            Date = DateTime.Now;
        }
    }
}