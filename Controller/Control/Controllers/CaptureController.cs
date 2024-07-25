using Control.Models;
using Control.Services;
using Microsoft.AspNetCore.Mvc;

namespace Control.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaptureController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;
        private readonly ICaptureService _captureService;

        public CaptureController(ILogger<BotController> logger, ICaptureService captureService)
        {
            _logger = logger;
            _captureService = captureService;
        }

        [HttpPost(Name = "Capture")]
        public async Task Post(CaptureRequest request)
        {
            if (request?.BotInput == null)
            {
                return;
            }
            await _captureService.CaptureData(request);
        }
    }
}
