using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Referral
    {
        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}