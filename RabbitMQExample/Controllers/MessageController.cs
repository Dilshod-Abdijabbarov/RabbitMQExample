using Microsoft.AspNetCore.Mvc;
using RabbitMQExample.Services;

namespace RabbitMQExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService;

        public MessageController(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] string message)
        {
            _rabbitMQService.SendMessage(message);

            return Ok(new { Message = "Xabar yuborildi!", SentMessage = message });
        }
    }
}
