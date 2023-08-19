using Newtonsoft.Json;

namespace Commons.Communications.Message
{
    public class GetMessagesOfTwoUsersRequest
    {
        [JsonProperty("Sender")] public int Sender { get; set; }
        [JsonProperty("Receiver")] public int Receiver { get; set; }
    }
}