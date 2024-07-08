using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TiyaniBotScript : IBotScript
{
    Collider2D BorderW,
        BorderE,
        BorderN,
        BorderS;

    public enum MovementType
    {
        RIGHT, LEFT, UP, DOWN,IDLE
    }
    List<MovementType> MovementTypes = Enum.GetValues(typeof(MovementType)).Cast<MovementType>().ToList();

    public enum RotationType
    {
        RIGHT, LEFT
    }
    float Tolerance = 0.1f;

    Vector2 Movement;
    float Rotation;
    bool Shoot;

    //List<Collider2D> Collider2Ds = new List<Collider2D>();
    Collider2D Bot = null;
    public TiyaniBotScript() { }

    public BotCommands GetCommands(List<Collider2D> gameObjects)
    {
        ResetBotCommands();
        //Collider2Ds = gameObjects;
        Bot = gameObjects.Where(c => c.name == nameof(TiyaniBotScript)).FirstOrDefault();

        OnObjects(gameObjects);
        BotCommands botCommands = new(Movement, Rotation, Shoot);

        return botCommands;
    }

    void ResetBotCommands()
    {
        //Collider2Ds.Clear();
        Shoot = false;
        Rotation = 0f;
        Movement = Vector2.zero;

        BorderW = null;
        BorderE =null;
        BorderN  = null;
        BorderS = null;
    }

    public Vector2 Move(MovementType movement)
    {
        bool canMakeThisMove = IsValidMove(movement);
        if (!canMakeThisMove) {
            foreach (var move in MovementTypes)
            {
                if (IsValidMove(move))
                {
                    movement = move;
                    break;
                }
            }
        }
        switch (movement)
        {
            case MovementType.LEFT:
                return Vector2.left;

            case MovementType.DOWN:
                return Vector2.down;

            case MovementType.RIGHT:
                return Vector2.right;

            case MovementType.UP:
                return Vector2.up;

            default:
                return Vector2.zero;
        }

    }

    public void OnObjects(List<Collider2D> Collider2Ds)
    {
        var bullets = Collider2Ds.Where(c => c.CompareTag("bullet")).ToList();
        var borders = Collider2Ds.Where(c => c.CompareTag("border")).ToList();

         BorderW = borders.Where(b => b.name == "BoardW").DefaultIfEmpty(null).First();
         BorderE = borders.Where(b => b.name == "BorderE").DefaultIfEmpty(null).First();
         BorderN = borders.Where(b => b.name == "BoardN").DefaultIfEmpty(null).First();
         BorderS = borders.Where(b => b.name == "BoardS").DefaultIfEmpty(null).First();
        #region on  bullet(s)
        if (bullets.Count != 0 )
        {
            foreach (var bullet in bullets)
            {
                bool sameLineHorizontal = SameLine(bullet,Vector2.left);
                if (sameLineHorizontal)
                {
                    // move up or down
                    if (borders.Count == 0)
                    {
                        Movement = Move(MovementType.UP);
                        Rotation = Follow(bullet);
                        return;
                    }
                    else
                    {
              

                        if (BorderN != null)
                        {
                            Movement = Move(MovementType.DOWN);
                            return;
                        }
                        else if (BorderS != null)
                        {
                            Movement = Move(MovementType.UP);
                            return;
                        }
                        else {

                            Movement = Move(MovementType.DOWN); // Default to down
                            return;

                        }


                    }
                }

                bool sameLineVertical = SameLine(bullet,Vector2.up);
                if (sameLineVertical)
                {
                    if (borders.Count == 0)
                    {
                        Movement  = Move(MovementType.RIGHT);
                        Rotation = Follow(bullet);
                    }
                    else
                    {
                        

                        if (BorderW != null)
                        {
                            Movement = Move(MovementType.RIGHT);
                            Rotation = Follow(bullet);
                            return;
                        }
                        else if (BorderE != null)
                        {
                            Movement = Move(MovementType.LEFT);
                            Rotation = Follow(bullet);
                            return;
                        }
                        else
                        {

                            Movement = Move(MovementType.LEFT); // Default to down
                            Rotation = Follow(bullet);
                            return ;
                        }
                    }
                    
                }

            }
        }
        #endregion
        #region on enemies

        var enemies = Collider2Ds.Where(c => !(c.CompareTag("bullet")) && !(c.CompareTag("border")) && !c.name.Contains("DupCircle")).ToList();

        if (enemies.Count != 0)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.name != nameof(TiyaniBotScript))
                {
                    //bool onSameLineUp = SameLine(enemy, Vector2.up);
                    //if (onSameLineUp)
                    //{
                    //    Shoot = true;
                    //    Rotation = Follow(enemy);
                    //    Movement = Vector2.up;
                    //    return;
                    //}
                    Shoot = true;
                    Rotation = Follow(enemy);
                }
            }
        }

        #endregion
        #region on nothing

        #endregion

    }



    bool SameLine(Collider2D target, Vector2 dir)
    {


        Vector3 distance = Bot.transform.position - target.transform.position;
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

    bool IsValidMove( MovementType movementType) {

        var x = Bot.transform.position.x;
        var y = Bot.transform.position.y;

        if (movementType == MovementType.LEFT && BorderW != null) {

            return x > BorderW.transform.position.x+1; 
        }
        else if (movementType == MovementType.RIGHT && BorderE != null) {

            return x < BorderE.transform.position.x -1;
        }
        else if (movementType == MovementType.UP && BorderN != null) {

            return y < BorderN.transform.position.y-1;
        }
        else if (movementType == MovementType.DOWN && BorderS != null) {

            return y > BorderS.transform.position.y+1;

        }else if (BorderW == null && BorderE == null && BorderS == null && BorderN == null)
        {
            return true;
        }
        else {
            return false;
        }
    }
    float Follow(Collider2D target) {

        Vector3 normalizedDirection = (target.transform.position - Bot.transform.position).normalized;

        float targetAngle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;
        targetAngle = Mathf.Clamp(targetAngle, -180f, 180f) / 360f; 
        float rotationAmount = Vector3.Dot(Bot.transform.position.normalized, normalizedDirection) * Mathf.DeltaAngle(Bot.transform.eulerAngles.z, targetAngle);

        return Mathf.SmoothStep(-1f, 1f, Mathf.Lerp(-100f, 100f, rotationAmount));
    }
}

