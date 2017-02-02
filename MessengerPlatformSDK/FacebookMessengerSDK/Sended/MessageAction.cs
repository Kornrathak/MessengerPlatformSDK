using FacebookMessengerSDK.Received;
using Newtonsoft.Json;

namespace FacebookMessengerSDK.Sended
{
    public class MessageAction
    {
        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("sender_action")]
        public string Sender_action { get; set; }
    }
}
