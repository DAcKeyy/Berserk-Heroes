using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;

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
            return JObject.FromObject(message, 
                new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore
                }).ToString();
        }

        public static JsonMessage Deserialize(string data)
        {
            return JToken.Parse(data).ToObject<JsonMessage>();
        }
    }
}