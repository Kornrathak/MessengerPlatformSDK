using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Payload
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}