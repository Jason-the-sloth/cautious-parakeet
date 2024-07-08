using System.Collections.Generic;
using UnityEngine;

public class RoseBotScript : IBotScript
{
    // Game Objects
   
    private GameObject RoseBot = null;

    public RoseBotScript() { }

    public BotCommands GetCommands(List<Collider2D> gameObjects)
    {
        Collider2D playerCollider = null;
        BotCommands botCommands = new BotCommands();

        // Print the list of game objects
        Debug.Log("Game Objects:");
        foreach (Collider2D obj in gameObjects)
        {
            Debug.Log("Object: " + obj.gameObject.name);

            if (obj.gameObject.name == "Player 1")
            {
                RoseBot = obj.gameObject;
            }
            else if (obj.gameObject.name == "Player 2")
            {
                playerCollider = obj;
            }
        }

        if (playerCollider != null)
        {
            botCommands = PlayerFound(playerCollider);
        }
        else
        {

           
            botCommands = SearchForPlayer(playerCollider);
        }

        return botCommands;
    }


    private BotCommands PlayerFound(Collider2D player)
    {
        Vector2 move = Vector2.zero;
        float rotate = 0f;
        bool shoot = false;

        float angle = Vector2.SignedAngle(player.transform.position - RoseBot.transform.position, RoseBot.transform.up);

        if (angle < -5.0F)
        {
            rotate = 1f;
        }
        else if (angle > 5.0F)
        {
            rotate = -1f;
        }
        else
        {
            shoot = true;
        }

        float distance = Vector2.Distance(player.transform.position, RoseBot.transform.position);
        if (distance > 4)
        {
            move = Vector2.up;
        }
        else if (distance < 2)
        {
            move = Vector2.down;
        }
        return new BotCommands(move, rotate, shoot);
    }

    private BotCommands SearchForPlayer(Collider2D player)
    {
        BotCommands botCommands;
        if (player == null || player != null )
        {
            // Assuming 'RoseBot' is your current GameObject (e.g., the bot itself)
            Vector2 pos = RoseBot.transform.position;

            // Calculate angle between Vector2.zero and the bot's up direction
            float angle = Vector2.SignedAngle(Vector2.zero - pos, RoseBot.transform.up);

            float rotate = 0f;

            // Determine rotation based on angle
            if (angle < -60.0F)
            {
                rotate = 1f;
            }
            else if (angle > 60.0F)
            {
                rotate = -1f;
            }

            // Create BotCommands with movement up, rotation, and no shooting
            return new BotCommands(Vector2.up, rotate, false);
        }
        else
        {
            // If no borders were found, return default BotCommands (no movement, no rotation, no shooting)
           
            return new BotCommands(Vector2.zero, 0f, false);
        }
    }
}
