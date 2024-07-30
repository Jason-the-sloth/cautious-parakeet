using System;
using System.Collections.Generic;

namespace Helpers
{
    [Serializable]
    public class BotInput
    {
        public Player Player = new();
        public List<Player> OtherPlayers = new();
        public List<Bullet> Bullets = new();
        public List<Obstacle> Obstacles = new();
        public List<Border> Borders = new();
    }
}
