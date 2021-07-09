using System;
using System.Runtime.Serialization;
using Berserk.Messaging;
using Berserk.Networking.Messages;
using Newtonsoft.Json.Serialization;

namespace Berserk.Messaging.Messages
{
    public class Message
    {
        public uint UserID { get; set; }
        public string AccessToken { get; set; }
        //public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool Vaild { get; set; }
        public JsonMessage MessageBody { get; set; }
        
        public Message()
        {
            Text = "";
            UserID = 0;
            AccessToken = "";
            MessageBody = new JsonMessage();
            Vaild = false;
        }

        public Message(string text, uint userID, string accessToken, JsonMessage messageBody)
        {
            Text = text;
            UserID = userID;
            AccessToken = accessToken;
            MessageBody = messageBody;
            Vaild = false;
        }

        public Message(string text, bool isValid ,uint userID, string accessToken, JsonMessage messageBody)
        {
            Text = text;
            Vaild = isValid;
            UserID = userID;
            AccessToken = accessToken;
            MessageBody = messageBody;
        }
        
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            errorContext.Handled = true;
        }
    }
}