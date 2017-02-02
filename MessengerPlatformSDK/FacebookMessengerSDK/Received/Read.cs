using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Read
    {
        [JsonProperty("watermark")]
        public long Watermark { get; set; }

        [JsonProperty("seq")]
        public long Seq { get; set; }
    }
}