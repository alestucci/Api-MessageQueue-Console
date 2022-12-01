using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Challenge1;

namespace MessageQueueConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=sachallenge;AccountKey=pbIdZlD+WZs4sPsbMJF4CuGPVjhKBtQldxR5bn2Rmg5zMFaspWqTKVLF6nU8XDpH8BN8C7cHfEhG+AStniUSEw==;EndpointSuffix=core.windows.net";

            // Instantiate a QueueClient which will be used to manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, "va-queue");


            //if (queueClient.Exists())
            while (queueClient.Exists())
            {

                // Get the next message
                QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();

                if (retrievedMessage.Length > 0)
                {
                    var lastMessage = retrievedMessage[0].Body;

                    var deserializedMessage = JsonSerializer.Deserialize<Message>(lastMessage);

                    // Process (i.e. print) the message in less than 30 seconds
                    Console.WriteLine($"Date & Time: '{deserializedMessage.DateTime}'");
                    Console.WriteLine($"Message Title: '{deserializedMessage.MessageTitle}'");
                    Console.WriteLine($"Message Body: '{deserializedMessage.MessageBody}'");
                    Console.WriteLine($"Sender: '{deserializedMessage.Sender}'");

                    // Delete the message
                    queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);

                }

            }
        }
    }
}