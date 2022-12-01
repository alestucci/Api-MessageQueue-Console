using Azure.Storage.Queues;
using System.Collections;

namespace Challenge1
{
    public class Message
    {
        public string DateTime { get; set; }
        
        public string MessageTitle { get; set; }

        public string MessageBody { get; set; }

        public string Sender { get; set; }

        public Message()
        {

        }
    }
}
