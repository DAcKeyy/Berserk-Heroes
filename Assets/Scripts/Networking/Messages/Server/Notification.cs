using System;
using Berserk.Networking.Messages;
using Newtonsoft.Json;

namespace Berserk.Messaging.Messages
{
    class Notification : IDescriptional
    {
        public string Description { get ; set; }
        public JsonMessage Message{ get ; set; }
        public bool isValid{ get ; set; }
        
        public Notification()
        {
            Description = "";
            isValid = false;
        }
        
        public Notification(string description, bool valid)
        {
            Description = description;
            isValid = valid;
        }
        
        public Notification(string description, bool valid, JsonMessage message)
        {
            Description = description;
            isValid = valid;
            Message = message;
        }
    }
}