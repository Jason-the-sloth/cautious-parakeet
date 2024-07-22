namespace Control.Models
{
    public class BotInput
    {
        public Player? Player { get; set; }
        public List<Player>? OtherPlayers { get; set; }
        public List<Bullet>? Bullets { get; set; }
        public List<Obstacle>? Obstacles { get; set; }
        public List<Border>? Borders { get; set; }
    }
}
