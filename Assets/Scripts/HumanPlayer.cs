using System.Collections.Generic;
using UnityEngine;


public class HumanPlayer : IBotScript
{
    public BotCommands GetCommands(List<RaycastHit2D> objects)
    {
        BotCommands botCommands = new(Move(), Rotate(), Shoot());

        return botCommands;
    }

    Vector2 Move()
    {
// Initialize movement direction
        Vector2 moveDirection = Vector2.zero;

        // Check for each arrow key and adjust the move direction accordingly
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector2.right;
        }


        return moveDirection;
        
    }

    float Rotate()
    {
        float rotate = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rotate += 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotate -= 1;
        }

        return rotate;
    }

    bool Shoot()
    {
        return Input.GetKey(KeyCode.Space);

    }
}
