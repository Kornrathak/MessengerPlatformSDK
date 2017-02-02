using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Messaging
    {
        [JsonProperty("sender")]
        public Sender Sender { get; set; }

        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("optin")]
        public Optin Optin { get; set; }

        [JsonProperty("delivery")]
        public Delivery Delivery { get; set; }

        [JsonProperty("read")]
        public Read Read { get; set; }

        [JsonProperty("postback")]
        public Postback Postback { get; set; }
    }
}
