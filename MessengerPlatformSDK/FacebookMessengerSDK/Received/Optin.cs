using Newtonsoft.Json;

namespace FacebookMessengerSDK.Received
{
    public class Optin
    {
        [JsonProperty("ref")]
        public string Ref { get; set; }
    }
}
