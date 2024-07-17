using Control.Models;

namespace Control.Services
{
    public interface IBotService
    {
        Task<BotCommands> GetCommands(BotInput botInput);
    }
}
