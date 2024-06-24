using System;
using UnityEngine;

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
	public BotScriptInterface botScript = new HumanPlayer();


	//Internal Variables
    private float lastFired;


	public void SetBotScript(BotScriptInterface botScript)
	{
		this.botScript = botScript;
	}


    // Start is called before the first frame update
    private void Start()
    {
		botScript ??= new HumanPlayer();
		bullets = GameObject.Find("Bullets");
    }

    // Update is called once per frame
    private void Update()
    {

		BotCommands botCommands = botScript.GetCommands(null);


		Move(botCommands.GetMove());
        Rotate(botCommands.GetRotate());
        Shoot(botCommands.GetShoot());
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
