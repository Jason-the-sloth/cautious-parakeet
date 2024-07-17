using System;

namespace Helpers
{
    [Serializable]
    public class Bullet
    {
        public SimpleVector Position = new();
        public SimpleVector Velocity = new();
        public Player FiredBy = new();

    }
}
