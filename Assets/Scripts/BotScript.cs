using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class BotScript : MonoBehaviour
{
    public GlobalVariables globalVariables;



    //Game Objects
    private GameObject botStats;
    private GameObject bullets;
	public IBotScript botScript = new HumanPlayer();
    public float health;

	//Internal Variables
    private float lastFired;
    private Slider healthSlider;

    public string team;
    public Color teamColor;
    public void SetBotScript(IBotScript botScript)
	{
		this.botScript = botScript;
	}


    // Start is called before the first frame update
    private void Start()
    {
        globalVariables = Resources.Load<GlobalVariables>("GlobalVariables");
        botScript ??= new HumanPlayer();
		bullets = GameObject.Find("Bullets");
        healthSlider = GetComponentInChildren<Slider>();
        health = globalVariables.maxHealth;
        UpdateHealthBar(health);
        
        CreateArcAndCircle();
        InitializeBotStats();
    }
    void InitializeBotStats()
    {
        botStats = Instantiate(globalVariables.stats);
        botStats.transform.Find("PlayerName").GetComponent<Text>().text = gameObject.name;
        botStats.GetComponent<BotStatsScript>().textColor = teamColor;
        var _team = GameObject.Find(team);
        botStats.transform.SetParent(_team.transform,false);

    }


    // Update is called once per frame
    private void Update()
    {


        BotCommands botCommands = botScript.GetCommands(CheckConeCollision());


        Move(botCommands.GetMove());
        Rotate(botCommands.GetRotate());
        Shoot(botCommands.GetShoot());
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (team == "Team1")
        {
            botStats.GetComponent<BotStatsScript>().addScore(SharedData.TeamOneScore);
        }
        else
        {
            botStats.GetComponent<BotStatsScript>().addScore(SharedData.TeamTwoScore);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision != null) {
            if (collision.gameObject.CompareTag("bullet"))
            {
                switch (team)
                {
                    case "Team1":
                        SharedData.TeamTwoScore++;
                        break;
                    default:
                        SharedData.TeamOneScore++;
                        break;

                }      

                TakeDamage(collision.relativeVelocity.magnitude);
            }
        }
    }
    private void CreateArcAndCircle()
    {

        //drawing the arc
        Transform circle = transform.Find("CollisionCircle");
        Transform arc1 = transform.Find("CollisionLine1");
        Transform arc2 = transform.Find("CollisionLine2");

        circle.localScale = new Vector3(globalVariables.viewRadius * 2, globalVariables.viewRadius *2);

        arc1.localScale = new Vector3(0.01f, globalVariables.viewRadius );
        arc1.Rotate(new Vector3(0, 0, globalVariables.viewAngle / 2 ));
        arc1.position += arc1.up * (globalVariables.viewRadius / 2);

        arc2.localScale = new Vector3(0.01f, globalVariables.viewRadius);
        arc2.Rotate(new Vector3(0, 0, -globalVariables.viewAngle /2));
        arc2.position += arc2.up * (globalVariables.viewRadius / 2);

    }

    private string CheckConeCollision ()
	{
        Collider2D[] raycastHits = Physics2D.OverlapCircleAll(transform.position, globalVariables.viewRadius);
        BotInput botInput = new();

        if (raycastHits.Length > 0)
        {
            foreach (Collider2D raycastHit in raycastHits)
            {
                bool isSeen = false;
                //Lets pretend you can hear bullets
                if (raycastHit.CompareTag("bullet") || raycastHit.CompareTag("border"))
                {
                    isSeen = true;
                }
                else
                {
                    Vector3 targetDir = raycastHit.transform.position - transform.position;

                    float angle = Vector3.Angle(targetDir, transform.up);

                    if (angle <= globalVariables.viewAngle / 2.0f) // Object is within half the cone angle
                    {
                        isSeen = true;

                    }
                }

                if (isSeen)
                {

                    if (raycastHit.CompareTag("bullet"))
                    {
                        botInput.bullets.Add(MapColliderToBullet(raycastHit));
                    }
                    else if (raycastHit.CompareTag("obstacle"))
                    {
                        botInput.obstacles.Add(MapColliderTObstacle(raycastHit));
                    }
                    else if (raycastHit.CompareTag("border"))
                    {
                        botInput.borders.Add(MapColliderToBorder(raycastHit));
                    }
                    else if (raycastHit.transform == transform)
                    {
                        botInput.player = MapColliderToPlayer(raycastHit.gameObject);
                    }
                    else
                    {
                        botInput.otherPlayers.Add(MapColliderToPlayer(raycastHit.gameObject));
                    }
                }
            }
        }



        string json = JsonUtility.ToJson(botInput,true);

        return json;
       
    }

    private void Shoot(bool shoot)
    {

        if(lastFired >= 0)
            lastFired -= Time.deltaTime;
    
        if (shoot && (lastFired < 0f))
        {
            lastFired = globalVariables.shootingInterval;

            CreateBullet();
            botStats.GetComponent<BotStatsScript>().addBulletsFired();

        }
        else if(shoot)
		{
			Debug.Log("Shooting is on cooldown");
		}
        
    }

    private void CreateBullet()
    {
        GameObject duplicateBullet = Instantiate(globalVariables.bullet, transform.position+(transform.up * 0.5f), transform.rotation * Quaternion.Euler(0, 0, 90));
        duplicateBullet.transform.SetParent(bullets.transform, true);
        Physics2D.IgnoreCollision(duplicateBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        duplicateBullet.transform.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity;
        duplicateBullet.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * globalVariables.bulletForce);
		duplicateBullet.gameObject.name = "Bullet"+Time.time;
        duplicateBullet.GetComponent<BulletScript>().shotOwner = gameObject; 

    }

    private void Rotate(float z)
    {
		bool validateRotate = ValidateRotate(z);

		if(validateRotate)
		{
			transform.rotation *= Quaternion.Euler(0, 0, globalVariables.rotationSpeed * z);
		}
    }

	private bool ValidateRotate(float rotate)
	{
		if(!BetweenOne(rotate))
		{
			Debug.LogError("Rotate failed Validation");
			return false;
		}
		return true;
	}

    private void Move(Vector2 move)
    {
		bool validated = ValidateMove(move);
		if(validated)
		{
			transform.GetComponent<Rigidbody2D>().AddRelativeForce(move);
		}
    }

	private bool ValidateMove(Vector2 move)
	{
		if(!BetweenOne(move.x))
		{
			Debug.LogError("Move X failed Validation");
			return false;
		}
		if(!BetweenOne(move.y))
		{
			Debug.LogError("Move Y failed Validation");
			return false;
		}
		return true;

	}

	private bool BetweenOne(float value)
	{
		return value<=1 && value >= -1;
	}
    void TakeDamage(float velocity)
    {
        float damage = globalVariables.bulletDamage + Mathf.Clamp(velocity * globalVariables.bulletCoefficientDamage, 0.0f, health - globalVariables.bulletDamage);
        health -= damage;
        UpdateHealthBar(health);
        botStats.GetComponent<BotStatsScript>().updateHealth(health);
        botStats.GetComponent<BotStatsScript>().addHits();

        if (health <= 0)
        {
            //death implementation or triiger game over event.
            SceneManager.LoadScene("Base");
        }

    } 
    public void UpdateHealthBar(float value)
    {
        healthSlider.value = value/globalVariables.maxHealth;
    }
    BotInput.Player MapColliderToPlayer(GameObject gameObject)
    {


        BotInput.Player player = new();
        player.position = gameObject.transform.position;
        player.rotation = gameObject.transform.rotation;
        player.velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        player.color = gameObject.GetComponent<Renderer>().material.color;

        return player;
       
    }
    BotInput.Border MapColliderToBorder(Collider2D collider)
    {
        BotInput.Border border = new BotInput.Border();
        border.position = collider.transform.position;
        border.width = collider.GetComponent<Renderer>().bounds.size.x;
        border.height = collider.GetComponent<Renderer>().bounds.size.y;

        return border;
    }
    BotInput.Bullet MapColliderToBullet(Collider2D collider)
    {

        BotInput.Bullet bullet = new ();
        bullet.position = collider.transform.position;
        bullet.velocity = collider.GetComponent <Rigidbody2D>().velocity;
        bullet.force = collider.GetComponent<Rigidbody2D>().totalForce;
        var parent = collider.GetComponent<BulletScript>().shotOwner;
        bullet.firedBy = MapColliderToPlayer(parent);

        return bullet;

    }
    BotInput.Obstacle MapColliderTObstacle(Collider2D collider)
    {

        BotInput.Obstacle obstacle = new ();
        obstacle.position = collider.transform.position;
        obstacle.velocity = collider.GetComponent<Rigidbody2D>().velocity;
        obstacle.radius = collider.GetComponent<CircleCollider2D>().radius;

        return obstacle;
    }
}
