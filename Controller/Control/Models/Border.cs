using System.Numerics;

namespace Control.Models
{
    [Serializable]
    public class Border
    {
        public SimpleVector Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }
}
