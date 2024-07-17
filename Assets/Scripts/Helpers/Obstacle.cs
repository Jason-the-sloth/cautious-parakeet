using System;

namespace Helpers
{
    [Serializable]
    public class Obstacle
    {
        public SimpleVector Position = new();
        public SimpleVector Velocity = new();
        public float Radius = new();
    }
}
