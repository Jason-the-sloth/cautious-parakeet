using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GlobalVariables globalVariables;
    public GameObject shotOwner;
    
    // Start is called before the first frame update
    void Start()
    {
        globalVariables = Resources.Load<GlobalVariables>("GlobalVariables");


    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;


        globalVariables.deathCount -= Time.deltaTime;
        if(globalVariables.deathCount < 1 || GetComponent<Rigidbody2D>().velocity.magnitude < 1)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(transform.gameObject);
    }
}
