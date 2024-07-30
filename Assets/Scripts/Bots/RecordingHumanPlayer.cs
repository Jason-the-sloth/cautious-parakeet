using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Services;
using Unity.Serialization.Json;
using UnityEngine;

public class RecordingHumanPlayer : IBotScript
{
    private ControlService controlService;
    public RecordingHumanPlayer()
    {
        controlService = new ControlService();
    }

    public BotCommands GetCommands(BotInput botinput)
    {
        BotCommands botCommands = new(new(Move()), Rotate(), Shoot());
        controlService.Capture(botinput, botCommands);
        return botCommands;
    }

    private Vector2 Move()
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

    private float Rotate()
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

    private bool Shoot()
    {
        return Input.GetKey(KeyCode.Space);
    }
}
