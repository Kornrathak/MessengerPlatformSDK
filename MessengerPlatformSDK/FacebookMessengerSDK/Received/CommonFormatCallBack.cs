using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class CommonFormatCallBack
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("entry")]
        public Entry[] Entries { get; set; }
    }
}
