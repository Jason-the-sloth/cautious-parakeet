using Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicBotScript : IBotScript
{
    private GameObject me = null;

    public BasicBotScript() { }

    public BotCommands GetCommands(BotInput botinput)
    {
        var colliders = new List<Collider2D>();

        Dictionary<String, List<Collider2D>> colliderMap = new();

        if (me == null)
        {
            WhoAmI(colliders);
        }

        foreach (var collider in colliders)
        {
            var gameObject = collider.gameObject;
            var tag = gameObject.tag;
            if (gameObject == me)
                continue;

            if (!colliderMap.ContainsKey(tag))
            {
                colliderMap[tag] = new List<Collider2D>();
            }

            colliderMap[tag].Add(collider);
        }

        BotCommands botCommands = new();

        //if found the enemy player shoot at and try to maintain distance
        if (colliderMap.ContainsKey("player"))
        {
            botCommands = FoundEnemy(colliderMap["player"][0]);

        }
        //else search
        else
        {
            List<Collider2D> borders = colliderMap.GetValueOrDefault("border", null);

            botCommands = SearchForEnemy(borders);
        }

        return botCommands;
    }

    private BotCommands SearchForEnemy(List<Collider2D> borders)
    {
        BotCommands botCommands;
        if (borders != null && borders.Count > 0)
        {
            Vector2 pos = me.transform.position;
            float rotate = 0f;

            float angle = Vector2.SignedAngle(Vector2.zero - pos, me.transform.up);

            if (angle < -20.0F)
            {
                rotate = 1f;
            }
            else if (angle > 20.0F)
            {
                rotate = -1f;
            }
            botCommands = new(SimpleVector.Up, rotate, false);
        }
        else
        {
            //fly straight
            botCommands = new(SimpleVector.Up, 0f, false);
        }
        return botCommands;
    }

    private BotCommands FoundEnemy(Collider2D firstPlayer)
    {
        Vector2 move = Vector2.zero;
        float rotate = 0f;
        bool shoot = false;

        float angle = Vector2.SignedAngle(firstPlayer.transform.position - me.transform.position, me.transform.up);

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

        float distance = Vector2.Distance(firstPlayer.transform.position, me.transform.position);
        if (distance > 4)
        {
            move = Vector2.up;
        }
        else if (distance < 2)
        {
            move = Vector2.down;
        }
        return new BotCommands(new(move), rotate, shoot);
    }

    private void WhoAmI(List<Collider2D> colliders)
    {

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("player"))
            {
                me = collider.gameObject;
            }
        }
        Debug.Log("I am " + me.name);
    }
}