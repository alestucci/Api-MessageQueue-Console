using Azure.Identity;
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
        private readonly IConfiguration _configuration;

        public MessageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CreateMessage")]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            // Create a QueueClient that will authenticate through Active Directory
            Uri queueUri = new Uri("https://sachallenge.queue.core.windows.net/va-queue");
            QueueClient queue = new QueueClient(queueUri, new DefaultAzureCredential()); 

            var messageByPost = JsonSerializer.Serialize(message);

            await InsertMessageAsync(queue, messageByPost);

            return Ok();
        }

        static async Task InsertMessageAsync(QueueClient theQueue, string newMessage)
        {
            await theQueue.SendMessageAsync(newMessage);
        }


    }
}