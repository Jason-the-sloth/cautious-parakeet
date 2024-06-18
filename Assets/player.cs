using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public logic logic;
    public GridManager GridManager;
    public float MoveSpeed = 2;
    Rigidbody2D Rigidbody2D;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public AudioSource GunSound;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        GunSound = GetComponent<AudioSource>();
        GridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
        gameObject.transform.position = new Vector3(GridManager.Width - 3, GridManager.Height - 3);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }
    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
            GunSound.Play();
        }
    }
    void Movement()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0)
        {
            transform.position += new Vector3(horizontal * MoveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, -90 * horizontal);
        }
        else if (vertical != 0)
        {
            transform.position += new Vector3(0, vertical * MoveSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.Euler(0, 0, 90 - 90 * vertical);
        }
    }
}
