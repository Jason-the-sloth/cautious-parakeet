using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class human : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 0.5f;

    public GameObject bullet;
    public GameObject bullets;

    // Start is called before the first frame update
    void Start()
    {
        bullet = GameObject.Find("Bullet");
        bullets = GameObject.Find("Bullets");
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

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);


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
    
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject duplicateBullet = Instantiate(bullet, new Vector2(0,1), Quaternion.identity);
            duplicateBullet.transform.SetParent(bullets.transform, true);
            duplicateBullet.GetComponent<BulletScript>().original = false;
            // duplicateBullet.transform.
        }

    }
}
