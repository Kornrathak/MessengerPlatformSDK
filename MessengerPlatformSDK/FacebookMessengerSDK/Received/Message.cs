using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Message
    {
        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("seq")]
        public long Seq { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("is_echo")]
        public bool Is_Echo { get; set; }

        [JsonProperty("app_id")]
        public long App_Id { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("quick_reply")]
        public QuickReply Quick_Reply { get; set; }

        [JsonProperty("attachments")]
        public Attachments Attachments { get; set; }
    }
}