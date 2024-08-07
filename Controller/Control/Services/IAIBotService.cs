using Control.Models;

namespace Control.Services
{
    public interface IAIBotService
    {
        void GetCommands(BotInput botInput);
    }
}