using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Postback
    {
        [JsonProperty("payload")]
        public string Payload { get; set; }

        [JsonProperty("referral")]
        public Referral Referral { get; set; }
    }
}
