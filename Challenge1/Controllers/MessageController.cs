using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Challenge1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly Message _message;

        private readonly IConfiguration _configuration;

        public MessageController(IConfiguration configuration)
        {
            //_message = new Message();
            _configuration = configuration;
        }

        [HttpPost(Name = "CreateMessage")]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            var messageByPost = JsonSerializer.Serialize(message);

            string connectionString = _configuration.GetValue<string>("ConnectionString");
            //string connectionString = "DefaultEndpointsProtocol=https;AccountName=sachallenge;AccountKey=pbIdZlD+WZs4sPsbMJF4CuGPVjhKBtQldxR5bn2Rmg5zMFaspWqTKVLF6nU8XDpH8BN8C7cHfEhG+AStniUSEw==;EndpointSuffix=core.windows.net";

            QueueClient queue = new QueueClient(connectionString, "va-queue");

            await InsertMessageAsync(queue, messageByPost);

            return Ok();

        }

        static async Task InsertMessageAsync(QueueClient theQueue, string newMessage)
        {
            await theQueue.SendMessageAsync(newMessage);
        }


    }
}