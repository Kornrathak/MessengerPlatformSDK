using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("messaging")]
        public Messaging[] Messaginges { get; set; }
    }
}
