using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class QuickReply
    {
        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}