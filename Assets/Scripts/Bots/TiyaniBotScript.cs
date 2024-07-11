using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class TiyaniBotScript : IBotScript
{

    enum ObjectType
    {
        ENEMY, BULLET, OBSTACLE, NOTHING
    }
    float Tolerance = 0.3f;

    public TiyaniBotScript() { }

    public BotCommands GetCommands(string botinput)
    {
        var movement = Vector2.zero;
        var gameObjects = new List<Collider2D>();

        float rotation = 0f;
        bool shoot = false;

        var player = gameObjects.Where(obj => obj.name == nameof(TiyaniBotScript)).FirstOrDefault();

        var bullets = gameObjects.Where(obj => obj.CompareTag("bullet")).ToList();
        var enemies = gameObjects.Where(obj => obj.name != nameof(TiyaniBotScript) && obj.CompareTag("player")).ToList();
        var borders = gameObjects.Where(obj => obj.CompareTag("border")).ToList();

        ObjectType action = GetAction(bullets,enemies,borders);

        switch (action)
        {
            case ObjectType.ENEMY:
                shoot = SameLine(enemies.FirstOrDefault(),Vector2.left, player.transform.position);
                rotation = DetermineRotation(enemies.FirstOrDefault().transform.position, player.transform.position ); // Rotate towards enemy
                movement = Vector2.zero; // Stop movement
                Debug.Log($"action : {ObjectType.ENEMY} : {rotation}" );

                break;
            case ObjectType.OBSTACLE:
                //shoot = false;
                //rotation = DetermineRotation(borders.FirstOrDefault().transform.position, player.transform.position); // Rotate to avoid obstacle
                //movement = CalculateSafeMovement(borders.FirstOrDefault().transform.position, player.transform.position); // Move around obstacle
                //Debug.Log($"action : {ObjectType.OBSTACLE}");

                break;
            case ObjectType.BULLET :
                    //shoot = false;
                    //rotation = 0f;
                    //movement = Vector2.zero; // Stop movement or adjust based on bullet impact
                    //Debug.Log($"action : {ObjectType.BULLET}");
                break;
            case ObjectType.NOTHING:
                //shoot = false;
                //rotation = 0f;
                //movement = Vector2.up;
                //Debug.Log($"action : {ObjectType.NOTHING}");

                break;
        }

        BotCommands botCommands = new(movement, rotation, shoot);

        return botCommands;
    }

    private ObjectType GetAction(List<Collider2D> bullets, List<Collider2D> enemies , List<Collider2D> borders)
    {
   
        if (bullets.Any())
        {
            return ObjectType.BULLET;
        }
        if (enemies.Any())
        {
            return ObjectType.ENEMY;
        }
        if (borders.Any())
        {
            return ObjectType.OBSTACLE;
        }

        return ObjectType.NOTHING;
    }
    private float DetermineRotation(Vector3 targetPosition , Vector3 playerPosition)
    {
        float rotate = 0f;
        float angle = Vector2.SignedAngle(Vector3.zero - playerPosition, Vector3.up);

        if (angle < -20.0F)
        {
            rotate = 1f;
        }
        else if (angle > 20.0F)
        {
            rotate = -1f;
        }

        return rotate;
    }

    private Vector2 CalculateSafeMovement(Vector3 obstaclePosition, Vector3 playerPosition)
    {
       
        Vector2 obstacleDir = obstaclePosition - playerPosition;
        var move = new Vector2(obstacleDir.y, -obstacleDir.x).normalized;
       
        return move;
    }
    bool SameLine(Collider2D target, Vector2 dir, Vector3 bot)
    {

        Vector3 distance = bot - target.transform.position;
        float dotProduct = Vector3.Dot(distance.normalized, dir);

        if (Mathf.Abs(dotProduct) > (1 - Tolerance))
        {
            return true;
        }
        else
        {
           return false;
        }
    }
}

