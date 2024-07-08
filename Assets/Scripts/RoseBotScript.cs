using System.Collections.Generic;
using UnityEngine;

public class RoseBotScript : MonoBehaviour, IBotScript
{
    public float speed = 5f;

    public float timeBwtShots;
    public float startTimeBwtShots;
    public float bulletForce;
    public float searchSpeed = 20f;


    //Game Objects
    public GameObject bullet;
    private bool shoot = false;
    private GameObject RoseBot = null;

    private Vector2 searchDirection = Vector2.right; // Default search direction
    private float lastShotTime;


    public RoseBotScript() { }

    public BotCommands GetCommands(List<Collider2D> gameObjects)
    {

        GameObject player = null;

        // Identify RoseBot and player from gameObjects list
        foreach (Collider2D obj in gameObjects)
        {
            Debug.Log("Visible GameObjects ****: " + obj.gameObject.name);
            if (obj.gameObject.name == "Player 1")
            {
                RoseBot = obj.gameObject;
                Debug.Log("RoseBot found: " + RoseBot.name);
            }
            if (obj.gameObject.name == "Player 2")
            {
                player = obj.gameObject;
                Debug.Log("Player found: " + player.name);
            }


        }

        // Ensure both RoseBot and player are assigned
        if (RoseBot == null || player == null)
        {
            Vector2 searchMovement = SearchForPlayer();
            return new BotCommands(searchMovement, 0f, false);
        }

        // Move the bot
        Vector2 movement = Move(player);


        // Validate the movement vector
        if (!ValidateMove(movement))
        {
            Debug.LogError("Move validation failed: " + movement);
            return new BotCommands(Vector2.zero, 0f, false);
        }
        bool shootEnemy = shoot;
        float rotate = Rotate(gameObjects);

        // Return bot commands
        Debug.Log("Movement+++++++++: " + movement);
        Debug.Log("shootEnemy+++++++++: " + shootEnemy);
        // Debug.Log("Rotate+++++++++: " + rotate);


        BotCommands botCommands = new BotCommands(movement, 0f, shootEnemy);
        return botCommands;
    }

    /*private BotCommands getGameObjects(List<Collider2D> gameObjectList) { 
    }*/

    private Vector2 Move(GameObject player)
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
    }



}
