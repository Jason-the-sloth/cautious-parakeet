using Control.Models;
using Control.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Control.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;
        private readonly IBotService _botService;
        private readonly IAIBotService _aiBotService;

        public BotController(ILogger<BotController> logger, IBotService botService, IAIBotService aiBotService)
        {
            _logger = logger;
            _botService = botService;
            _aiBotService = aiBotService;
        }

        [HttpPost(Name = "Process")]
        public async Task<BotCommands> Post([FromBody] BotInput botInput)
        {
            _logger.LogInformation("{botInput}", JsonSerializer.Serialize(botInput));
            return await _botService.GetCommands(botInput);
        }

        [HttpPost(Name = "ProcessAI")]
        public async Task ProcessAI([FromBody] BotInput botInput)
        {
            _logger.LogInformation("{botInput}", JsonSerializer.Serialize(botInput));
            _aiBotService.GetCommands(botInput);
        }
    }
}
