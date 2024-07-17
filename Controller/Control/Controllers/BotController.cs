using Control.Models;
using Control.Services;
using Microsoft.AspNetCore.Mvc;

namespace Control.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;
        private readonly IBotService _botService;

        public BotController(ILogger<BotController> logger, IBotService botService)
        {
            _logger = logger;
            _botService = botService;
        }

        [HttpPost(Name = "Process")]
        public async Task<BotCommands> Post(BotInput botInput)
        {
            return await _botService.GetCommands(botInput);
        }
    }
}
