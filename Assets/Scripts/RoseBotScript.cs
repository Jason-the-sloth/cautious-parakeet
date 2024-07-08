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
            // Call SearchForPlayer with the borders list
         
            botCommands = SearchForPlayer(RoseBot);
        }

        return botCommands;
    }

    // Example method to filter game objects
    /*public List<Collider2D> GetBorders(List<Collider2D> gameObjects)
    {
        List<Collider2D> listOfGameObjects = new List<Collider2D>();

        foreach (Collider2D obj in gameObjects)
        {
            // Check for specific game object names
            if (obj.CompareTag("Border"))
            {
                listOfGameObjects.Add(obj);
            }
        }

        return listOfGameObjects;
    }*/

    /*private Vector2 Move(GameObject player)
    {
        // Check if the player is null, then search for a player
        if (player == null)
        {

            shoot = false; // Stop shooting or other actions

            // Search for a player
            return SearchForPlayer();
        }
        else
        {
            shoot = true; // Start shooting 
            // Calculate direction towards the player
            Vector2 direction = (player.transform.position - RoseBot.transform.position).normalized;

            // Calculate the movement vector
            Vector2 movement = direction * speed * Time.deltaTime;

            movement.x = Mathf.Clamp(movement.x, -1f, 1f);
            movement.y = Mathf.Clamp(movement.y, -1f, 1f);



            return movement;
        }
    }


    private bool ValidateMove(Vector2 move)
    {

        // , check if the move is within expected bounds
        if (move.x < -1f || move.x > 1f || move.y < -1f || move.y > 1f)
        {
            Debug.LogError("Move validation failed: " + move);
            return false;
        }
        return true;
    }

    private Vector2 SearchForPlayer()
    {
        // Move RoseBot in a specific direction to search for player
        Vector2 movement = searchDirection * searchSpeed * Time.deltaTime;

        // Move RoseBot
        RoseBot.transform.position += (Vector3)movement;

        // Change search direction periodically (e.g., every second)
        if (Time.frameCount % 60 == 0) // Change direction every second (assuming 60 FPS)
        {
            searchDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        return movement;
    }

    private float Rotate(List<Collider2D> gameObjects)
    {
        GameObject borderS = null;
        GameObject borderE = null;
        GameObject borderW = null;
        GameObject borderN = null;
        float rotate = 0f;

        foreach (Collider2D obj in gameObjects)
        {
            Debug.Log("Visible Border GameObjects ****: " + obj.gameObject.name);

            if (obj.gameObject.name == "BorderS")
            {
                borderS = obj.gameObject;
                RotateBot(-4); // Example value for BorderS
                Debug.Log("BorderS found: " + borderS.name);
            }
            else if (obj.gameObject.name == "BorderE")
            {
                borderE = obj.gameObject;
                RotateBot(4); // Example value for BorderE
                Debug.Log("BorderE found: " + borderE.name);
            }
            else if (obj.gameObject.name == "BorderW")
            {
                borderW = obj.gameObject;
                RotateBot(-4); // Example value for BorderW
                Debug.Log("BorderW found: " + borderW.name);
            }
            else if (obj.gameObject.name == "BorderN")
            {
                borderN = obj.gameObject;
                RotateBot(4); // Example value for BorderN

                Debug.Log("BorderN found: " + borderN.name);
            }
        }

        // Adjust rotation based on the found borders
        return rotate;
    }

    private void RotateBot(float z)
    {
        float rotationSpeed = 10f; // Assign a default rotation speed or set it in the inspector

        bool validateRotate = ValidateRotate(z);

        if (validateRotate)
        {
            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * z);
        }
    }

    private bool ValidateRotate(float rotate)
    {
        if (!BetweenOne(rotate))
        {
            Debug.LogError("Rotate failed Validation");
            return false;
        }
        return true;
    }

    private bool BetweenOne(float value)
    {
        return value >= -1f && value <= 1f;
    }*/

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

    private BotCommands SearchForPlayer(GameObject player)
    {
        BotCommands botCommands;

        if (player != null )
        {
            // Assuming 'RoseBot' is your current GameObject (e.g., the bot itself)
            Vector2 pos = RoseBot.transform.position;

            // Calculate angle between Vector2.zero and the bot's up direction
            float angle = Vector2.SignedAngle(Vector2.zero - pos, RoseBot.transform.up);

            float rotate = 0f;

            // Determine rotation based on angle
            if (angle < -95.0F)
            {
                rotate = 1f;
            }
            else if (angle > 95.0F)
            {
                rotate = -1f;
            }

            // Create BotCommands with movement up, rotation, and no shooting
            return new BotCommands(Vector2.up, rotate, false);
        }
        else
        {
            
            return new BotCommands(Vector2.zero, 0f, false);
        }
    }
}
