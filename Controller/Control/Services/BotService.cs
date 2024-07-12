using Control.Controllers;
using Control.Models;

namespace Control.Services
{
    public class BotService : IBotService
    {
        private readonly ILogger<BotController> _logger;

        public BotService(ILogger<BotController> logger)
        {
            _logger = logger;
        }

        public BotCommands GetCommands(BotInput botInput)
        {
            if (botInput?.Player == null)
            {
                return new BotCommands();
            }

            //if found the enemy player shoot at and try to maintain distance
            if (botInput.OtherPlayers != null)
            {
                return FoundEnemy(botInput.Player, botInput.OtherPlayers[0]);
            }
            return SearchForEnemy(botInput.Player, botInput.Borders);
        }

        private BotCommands SearchForEnemy(Player player, List<Border> borders)
        {
            if (borders != null && borders.Count > 0)
            {
                float rotate = 0f;
                float angle = SimpleVector.SignedAngle(SimpleVector.Zero - player.Position, SimpleVector.Up);

                if (angle < -20.0F)
                {
                    rotate = 1f;
                }
                else if (angle > 20.0F)
                {
                    rotate = -1f;
                }
                return new(SimpleVector.Up, rotate, false);
            }
            //fly straight
            return new(SimpleVector.Up, 0f, false);
        }

        private BotCommands FoundEnemy(Player player, Player enemy)
        {
            SimpleVector move = SimpleVector.Zero;
            float rotate = 0f;
            bool shoot = false;

            float angle = SimpleVector.SignedAngle(enemy.Position - player.Position, SimpleVector.Up);

            if (angle < -5.0F)
            {
                rotate = 1f;
            }
            else if (angle > 5.0F)
            {
                rotate = -1f;
            }
            else
            {
                shoot = true;
            }

            float distance = SimpleVector.Distance(enemy.Position, player.Position);
            if (distance > 4)
            {
                move = SimpleVector.Up;
            }
            else if (distance < 2)
            {
                move = SimpleVector.Down;
            }
            return new BotCommands(move, rotate, shoot);
        }
    }
}
