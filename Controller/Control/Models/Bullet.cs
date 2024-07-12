using System.Numerics;

namespace Control.Models
{
    [Serializable]
    public class Bullet
    {
        public SimpleVector Position { get; set; }
        public SimpleVector Velocity { get; set; }
        public Player? FiredBy { get; set; }

    }
}
