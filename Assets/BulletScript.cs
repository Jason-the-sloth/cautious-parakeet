using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public int deathCount = -1;
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
        deathCount--;
        if(deathCount < 1)
        {
            Destroy(transform.gameObject);
        }
    }
}
