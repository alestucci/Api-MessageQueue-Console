using Azure.Storage.Queues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Challenge1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class MessageController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MessageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost(Name = "CreateMessage")]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            var messageByPost = JsonSerializer.Serialize(message);

            string connectionString = _configuration.GetValue<string>("ConnectionString");

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