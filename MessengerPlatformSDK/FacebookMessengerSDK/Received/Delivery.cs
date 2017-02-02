using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Delivery
    {
        [JsonProperty("mids")]
        public string[] Mids { get; set; }

        [JsonProperty("watermark")]
        public long WaterMark { get; set; }

        [JsonProperty("seq")]
        public long Seq { get; set; }
    }
}
