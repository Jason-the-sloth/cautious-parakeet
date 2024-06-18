using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class bot : MonoBehaviour
{
    //private Vector2 MoveInput;
    //public float MoveSpeed = 2;
    //private Rigidbody2D Rigidbody;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    Rigidbody = GetComponent<Rigidbody2D>();   
    //}

    //private void FixedUpdate()
    //{
    //    Rigidbody.MovePosition(Rigidbody.position + (MoveInput*MoveSpeed*Time.fixedDeltaTime));
    //}

    // void OnMove(InputValue inputValue)
    //{
    //    MoveInput = inputValue.Get<Vector2>();
    //}

    public float MoveSpeed = 2;
    Rigidbody2D Rigidbody2D;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public AudioSource GunSound;
    public int NumberOfFramesToSkip = 0;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        GunSound = GetComponent<AudioSource>();
     

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //Shooting();
    }
    void Shooting()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
            //GunSound.Play();
        //}
    }
    void Movement()
    {
        if (NumberOfFramesToSkip == 100)
        {
           
            Shooting();
            NumberOfFramesToSkip = 0;
        }
        else
        {
            NumberOfFramesToSkip = NumberOfFramesToSkip+1;
        }
    }
}
