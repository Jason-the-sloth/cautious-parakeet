using System;
using UnityEngine;
public class Player
{
    private int health;
    private float x, y;
    private int playerIndex;
    private float distancePerSecond;
    public event Action<int> HasDiedEvent;

    public Player(float X, float Y, int index)
    {
        health = 100;
        x = X;
        y = Y;
        playerIndex = index;

        distancePerSecond = 1.5f;
    }

    public void Move(float X, float Y)
    {
        Debug.Log("Player " + playerIndex + " is moving"
        + " X: " + X + " Y: " + Y);
        x += X * distancePerSecond;
        y += Y * distancePerSecond;
    }

    public float X
    {
        get { return x; }
    }

    public float Y
    {
        get { return y; }
    }
    public void TakeHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            HasDiedEvent.Invoke(playerIndex);
        }
    }
}