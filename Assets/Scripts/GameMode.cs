using System;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] Sprite[] p1;
    [SerializeField] Sprite[] p2;
    [SerializeField] GameObject player1Object;
    [SerializeField] GameObject player2Object;
    Player player1;
    Player player2;

    float delta1X = 0;
    float delta1Y = 0;
    float delta2X = 0;
    float delta2Y = 0;

    const int MOVES_PER_SECOND = 4;
    const float MOVE_ONCE_INTERVAL = 0.010f / MOVES_PER_SECOND;
    float movesSeondCounter;
    bool canMove;
    float MOVE_SPEED = 0.3f;
    float MOVE_SPEED_LIMIT = 0.7f;

    const float SPEED = 0.0008f;
    const int FIRES_PER_SECOND = 2;
    const float FIRE_ONCE_INTERVAL = 1.0f / FIRES_PER_SECOND;
    float firesSeondCounter;
    [SerializeField] bool gameModeActive;
    void Start()
    {
        this.gameModeActive = false;
    }

    void Initiate()
    {
        player1 = new Player(-180, -100, 1);
        player2 = new Player(180, -100, 2);
        movesSeondCounter = 0.0f;
        firesSeondCounter = 0.0f;
        canMove = true;
    }

    public void toggleGameMode(MenuStateEnum prevStateEnum)
    {
        this.gameModeActive = !this.gameModeActive;
        if (this.gameModeActive)
        {
            if (prevStateEnum == MenuStateEnum.GAME_LAUNCHED
                || prevStateEnum == MenuStateEnum.GAME_OVER)
            {
                Initiate();
            }
        }


    }
    void Update()
    {
        if (this.gameModeActive)
        {
            firesSeondCounter += Time.deltaTime;
            keyStrokes();
            movePlayers();
        }
    }

    private void keyStrokes()
    {
        movesSeondCounter += Time.deltaTime;
        if (movesSeondCounter <= MOVE_ONCE_INTERVAL)
        {

            canMove = false;
            Debug.Log(canMove);
        }
        else
        {

            canMove = true;
            Debug.Log(canMove);
            movesSeondCounter = 0;
        }

        if (Input.GetKeyDown(KeyCode.W))
            delta1Y += canMove ? MOVE_SPEED : 0;
        if (Input.GetKeyDown(KeyCode.S))
            delta1Y -= canMove ? MOVE_SPEED : 0;
        if (Input.GetKeyDown(KeyCode.A))
            delta1X -= canMove ? MOVE_SPEED : 0;
        if (Input.GetKeyDown(KeyCode.D))
            delta1X += canMove ? MOVE_SPEED : 0;

        if (Input.GetKeyDown(KeyCode.I))
            delta2Y += canMove ? MOVE_SPEED : 0;
        if (Input.GetKeyDown(KeyCode.K))
            delta2Y -= canMove ? MOVE_SPEED : 0;
        if (Input.GetKeyDown(KeyCode.J))
            delta2X -= canMove ? MOVE_SPEED : 0;
        if (Input.GetKeyDown(KeyCode.L))
            delta2X += canMove ? MOVE_SPEED : 0;

    }
    private void movePlayers()
    {
        limitMoveSpeed();
        if (delta1X != 0 || delta1Y != 0)
        {
            player1.Move(delta1X, delta1Y);
            player1Object.transform.position = new Vector3(player1.X, player1.Y, 0);

        }
        if (delta2X != 0 || delta2Y != 0)
        {
            player2.Move(delta2X, delta2Y);
            player2Object.transform.position = new Vector3(player2.X, player2.Y, 0);
        }
        reduce(ref delta1X);
        reduce(ref delta1Y);
        reduce(ref delta2X);
        reduce(ref delta2Y);
    }

    private void reduce(ref float val)
    {
        if (val > 0.1f)
            val -= SPEED;
        else if (val < -0.1f)
            val += SPEED;
        else
            val = 0;
    }

    private void limitMoveSpeed()
    {
        if (delta1X > MOVE_SPEED_LIMIT)
            delta1X = MOVE_SPEED_LIMIT;
        if (delta1X < -MOVE_SPEED_LIMIT)
            delta1X = -MOVE_SPEED_LIMIT;
        if (delta1Y > MOVE_SPEED_LIMIT)
            delta1Y = MOVE_SPEED_LIMIT;
        if (delta1Y < -MOVE_SPEED_LIMIT)
            delta1Y = -MOVE_SPEED_LIMIT;

        if (delta2X > MOVE_SPEED_LIMIT)
            delta2X = MOVE_SPEED_LIMIT;
        if (delta2X < -MOVE_SPEED_LIMIT)
            delta2X = -MOVE_SPEED_LIMIT;
        if (delta2Y > MOVE_SPEED_LIMIT)
            delta2Y = MOVE_SPEED_LIMIT;
        if (delta2Y < -MOVE_SPEED_LIMIT)
            delta2Y = -MOVE_SPEED_LIMIT;
    }
}
