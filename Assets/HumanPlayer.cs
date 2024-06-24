using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class human : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 0.5f;

    public float shootingInterval;

    public float bulletForce;

    public GameObject bullet;
    public GameObject bullets;

    private float lastFired;

    // Start is called before the first frame update
    void Start()
    {
        bullets = GameObject.Find("Bullets");
        lastFired = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Shoot();
    }

    void Move()
    {
// Initialize movement direction
        Vector3 moveDirection = Vector3.zero;

        // Check for each arrow key and adjust the move direction accordingly
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }

        transform.GetComponent<Rigidbody2D>().AddRelativeForce(moveDirection);


        if (Input.GetKey(KeyCode.Q))
        {
            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.rotation *= Quaternion.Euler(0, 0, -rotationSpeed);
        }
    }

    void Shoot()
    {
        if(lastFired >= 0)
            lastFired -= Time.deltaTime;
    
        if (Input.GetKey(KeyCode.Space) && (lastFired < 0f))
        {
            lastFired = shootingInterval;


            GameObject duplicateBullet = Instantiate(bullet, transform.position+(transform.up * 0.5f), transform.rotation * Quaternion.Euler(0, 0, 90));
            duplicateBullet.transform.SetParent(bullets.transform, true);
            Physics2D.IgnoreCollision(duplicateBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            duplicateBullet.transform.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity;
            duplicateBullet.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * bulletForce);
            duplicateBullet.GetComponent<BulletScript>().original = false;
            // duplicateBullet.transform.
        }

    }
}
