using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Sender
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
