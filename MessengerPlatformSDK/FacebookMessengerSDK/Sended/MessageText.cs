using Newtonsoft.Json;

namespace FacebookMessengerSDK.Sended
{
    public class MessageText
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
