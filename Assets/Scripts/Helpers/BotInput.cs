using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BotInput 
{
    public Player player;

    public List<Player> otherPlayers = new List<Player>();

    public List<Bullet> bullets = new List<Bullet>();

    public List<Obstacle> obstacles = new List<Obstacle>();

    public List<Border> borders = new List<Border>();

    [Serializable]
    public class Player
    {
        public Vector3 position;
        public Vector3 velocity;
        public Quaternion rotation;
        public Color color;

    }
    [Serializable]
    public class Obstacle
    {
        public Vector3 position;
        public float radius;
        public Vector3 velocity;
    }
    [Serializable]
    public class Bullet
    {
        public Vector3 position;
        public Vector3 velocity;
        public Vector3 force;
        public Player firedBy;

    }
    [Serializable]
    public class Border
    {
        public Vector3 position;
        public float width;
        public float height;
    }

}





