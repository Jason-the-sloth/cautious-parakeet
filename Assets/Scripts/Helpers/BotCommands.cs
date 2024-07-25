using System;

namespace Helpers
{
    [Serializable]
    public class BotCommands
    {
        public SimpleVector Move;
        public float Rotate;
        public bool Shoot;

        public BotCommands()
        {
            Move = SimpleVector.Zero;
            Rotate = 0f;
            Shoot = false;
        }

        public BotCommands(SimpleVector move, float rotate, bool shoot)
        {
            Move = move;
            Rotate = rotate;
            Shoot = shoot;
        }
    }
}
