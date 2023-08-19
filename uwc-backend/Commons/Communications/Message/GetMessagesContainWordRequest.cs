using Newtonsoft.Json;

namespace Commons.Communications.Message
{
    public class GetMessagesContainWordRequest
    {
        [JsonProperty("Sender")] public int Sender { get; set; }
        [JsonProperty("Receiver")] public int Receiver { get; set; }
        [JsonProperty("Word")] public string Word { get; set; }
    }
}