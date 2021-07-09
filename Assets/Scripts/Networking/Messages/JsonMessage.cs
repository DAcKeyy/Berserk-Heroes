using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Berserk.Messaging
{
    public class JsonMessage
    {
        public string Type { get; set; }
        public JToken Value { get; set; }

        public static JsonMessage FromValue<T>(T value)
        {
            return new JsonMessage
            {
                Type = typeof(T).FullName, 
                Value = JToken.FromObject(value, new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore
                })
            };
        }

        public static string Serialize(JsonMessage message)
        {
            return JToken.FromObject(message, 
                new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore
                }).ToString();
        }

        public static JsonMessage Deserialize(string data)
        {
            return JToken.Parse(data).ToObject<JsonMessage>();
        }
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            Debug.Log(context.ToString());
            errorContext.Handled = true;
        }
        
    }
}