using Azure.Storage.Queues;
using System.Collections;
using System.Text.Json.Serialization;

namespace Challenge1
{
    public class Message
    {
        [JsonIgnore]
        public DateTime DateTime { get; set; } = DateTime.Now;
        
        public string MessageTitle { get; set; }

        public string MessageBody { get; set; }

        public string Sender { get; set; }
    }
}
