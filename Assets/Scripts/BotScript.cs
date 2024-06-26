using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BotScript : MonoBehaviour
{
	//Game Variables
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 0.5f;
    public float shootingInterval;
    public float bulletForce;
	public float viewRadius;
	public float viewAngle;

	//Game Objects
    public GameObject bullet;
    private GameObject bullets;
	public IBotScript botScript = new HumanPlayer();


	//Internal Variables
    private float lastFired;


	public void SetBotScript(IBotScript botScript)
	{
		this.botScript = botScript;
	}


    // Start is called before the first frame update
    private void Start()
    {
		botScript ??= new HumanPlayer();
		bullets = GameObject.Find("Bullets");

        CreateArcAndCircle();
    }

    // Update is called once per frame
    private void Update()
    {
		

		BotCommands botCommands = botScript.GetCommands(CheckConeCollision());


		Move(botCommands.GetMove());
        Rotate(botCommands.GetRotate());
        Shoot(botCommands.GetShoot());
    }

    private void CreateArcAndCircle()
    {



        //drawing the arc
        Transform circle = transform.Find("CollisionCircle");
        Transform arc1 = transform.Find("CollisionLine1");
        Transform arc2 = transform.Find("CollisionLine2");

        circle.localScale = new Vector3(viewRadius*2, viewRadius*2);

        arc1.localScale = new Vector3(0.01f, viewRadius );
        arc1.Rotate(new Vector3(0, 0, viewAngle/2 ));
        arc1.position += arc1.up * (viewRadius / 2);

        arc2.localScale = new Vector3(0.01f, viewRadius );
        arc2.Rotate(new Vector3(0, 0, -viewAngle/2));
        arc2.position += arc2.up * (viewRadius / 2);

    }

    private List<Collider2D> CheckConeCollision ()
	{
        Collider2D[] raycastHits = Physics2D.OverlapCircleAll(transform.position, viewRadius);

        List<Collider2D> raycastHitsSeen = new();
        if (raycastHits.Length > 0)
        {
            foreach (Collider2D raycastHit in raycastHits)
            {
                //Lets pretend you can hear bullets
                if (raycastHit.CompareTag("bullet") || raycastHit.CompareTag("border"))
                {
                    raycastHitsSeen.Add(raycastHit);
                }
                else
                {
                    Vector3 targetDir = raycastHit.transform.position - transform.position;

                    float angle = Vector3.Angle(targetDir, transform.up);

                    if (angle <= viewAngle / 2.0f) // Object is within half the cone angle
                    {
                        raycastHitsSeen.Add(raycastHit);
                    }
                }
            }
        }
       
        return raycastHitsSeen;
       
    }


    private void Shoot(bool shoot)
    {

        if(lastFired >= 0)
            lastFired -= Time.deltaTime;
    
        if (shoot && (lastFired < 0f))
        {
            lastFired = shootingInterval;

            CreateBullet();

        }
		else if(shoot)
		{
			Debug.Log("Shooting is on cooldown");
		}
        
    }

    private void CreateBullet()
    {
        GameObject duplicateBullet = Instantiate(bullet, transform.position+(transform.up * 0.5f), transform.rotation * Quaternion.Euler(0, 0, 90));
        duplicateBullet.transform.SetParent(bullets.transform, true);
        Physics2D.IgnoreCollision(duplicateBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        duplicateBullet.transform.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity;
        duplicateBullet.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * bulletForce);
        duplicateBullet.GetComponent<BulletScript>().original = false;
		duplicateBullet.gameObject.name = "Bullet"+Time.time;
    }

    private void Rotate(float z)
    {
		bool validateRotate = ValidateRotate(z);

		if(validateRotate)
		{
			transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * z);
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

   
}
