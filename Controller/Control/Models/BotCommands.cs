namespace Control.Models
{
    public class BotCommands
    {
        public SimpleVector? Move { get; private set; }

        public float Rotate { get; private set; }

        public bool Shoot { get; private set; }

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
