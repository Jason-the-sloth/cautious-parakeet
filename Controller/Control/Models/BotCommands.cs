﻿using UnityEngine;

namespace Control.Models
{
    public class BotCommands
    {
        private Vector2 move;

        private readonly float rotate;

        private readonly bool shoot;

        public BotCommands()
        {
            move = Vector2.zero;
            rotate = 0f;
            shoot = false;
        }

        public BotCommands(Vector2 move, float rotate, bool shoot)
        {
            this.move = move;
            this.rotate = rotate;
            this.shoot = shoot;
        }

        public Vector2 GetMove()
        {
            return move;
        }

        public float GetRotate()
        {
            return rotate;
        }

        public bool GetShoot()
        {
            return shoot;
        }
    }
}
