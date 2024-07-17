namespace Control.Models
{
    public class BotInput
    {
        public Player? Player { get; internal set; }
        public List<Player>? OtherPlayers { get; internal set; }
        public List<Bullet>? Bullets { get; internal set; }
        public List<Obstacle>? Obstacles { get; internal set; }
        public List<Border>? Borders { get; internal set; }
    }
}
