using FacebookMessengerSDK.Received;
using Newtonsoft.Json;

namespace FacebookMessengerSDK.Sended
{
    public class MessageData
    {
        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("message")]
        public MessageText Message { get; set; }
    }
}
