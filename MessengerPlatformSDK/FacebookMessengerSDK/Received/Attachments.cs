using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Attachments
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("payload")]
        public Payload Payload { get; set; }
    }
}