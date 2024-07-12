using Control.Models;

namespace Control.Services
{
    public interface IBotService
    {
        BotCommands GetCommands(BotInput botInput);
    }
}
