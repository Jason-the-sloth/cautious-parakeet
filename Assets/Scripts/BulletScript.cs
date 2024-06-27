using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float deathCount = 1000;
    public Boolean original = true;

    // Start is called before the first frame update
    void Start()
    {
        if(original)
            return;

    }

    // Update is called once per frame
    void Update()
    {
        if(original)
            return;
        deathCount-=Time.deltaTime;
        if(deathCount < 1)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(transform.gameObject);
    }
}
