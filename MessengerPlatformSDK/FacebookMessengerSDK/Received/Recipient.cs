using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Recipient
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
