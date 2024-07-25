namespace Control.Models
{
    public class BotCommands
    {
        public SimpleVector? Move { get; set; }

        public float Rotate { get; set; }

        public bool Shoot { get; set; }

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
