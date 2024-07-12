namespace Control.Models
{
    [Serializable]
    public class BotInput
    {
        public Player? Player;
        public List<Player> OtherPlayers = new List<Player>();
        public List<Bullet> Bullets = new List<Bullet>();
        public List<Obstacle> Obstacles = new List<Obstacle>();
        public List<Border> Borders = new List<Border>();
    }
}
