using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Serialization.Json;
using UnityEngine;

public class BasicBotScript : IBotScript
{
    public BasicBotScript() { }

    public BotCommands GetCommands(BotInput botInput)
    {
        //if found the enemy player shoot at and try to maintain distance
        if (botInput?.OtherPlayers.FirstOrDefault() != null)
        {
            return FoundEnemy(botInput.Player,botInput.OtherPlayers.First());
        }
        
        return SearchForEnemy(botInput.Player,botInput.Borders);
    }

    private BotCommands SearchForEnemy(Player me, List<Border> borders)
    {
        BotCommands botCommands;
        if (borders != null && borders.Count > 0)
        {
            Vector2 pos = me.Position.ToVector2();
            float rotate = 0f;

            float angle = Vector2.SignedAngle(Vector2.zero - pos, Vector2.up);

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

    private BotCommands FoundEnemy(Player me, Player firstPlayer)
    {
        Vector2 move = Vector2.zero;
        float rotate = 0f;
        bool shoot = false;

        float angle = Vector2.SignedAngle(firstPlayer.Position.ToVector2() - me.Position.ToVector2(), Vector2.up);

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

        float distance = Vector2.Distance(firstPlayer.Position.ToVector2(), me.Position.ToVector2());
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
}