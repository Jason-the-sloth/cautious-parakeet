namespace Control.Models
{
    public class Player
    {
        public SimpleVector? Position { get; set; }
        public SimpleVector? Velocity { get; set; }
        public float Rotation { get; set; }
        public string? Color { get; set; }
        public float Health { get; set; }
        public float Score { get; set; }
    }
}
