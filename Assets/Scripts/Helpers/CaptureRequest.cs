using System;

namespace Helpers
{
    [Serializable]
    public class CaptureRequest
    {
        public BotInput BotInput;
        public BotCommands BotCommands;

        public CaptureRequest(BotInput botInput, BotCommands botCommands)
        {
            BotInput = botInput;
            BotCommands = botCommands;
        }
    }
}