using System;

namespace Helpers
{
    [Serializable]
    public class Player
    {
        public SimpleVector Position = new();
        public SimpleVector Velocity = new();
        public float Rotation = 0f;
        public string Color  = "RGBA(0, 0, 0, 1)";
        public float Health = 0f;
        public float Score = 0f;
    }
}
