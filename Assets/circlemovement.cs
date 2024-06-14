using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlemovement : MonoBehaviour
{
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

    }


    private void onTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boundry")

        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            moveSpeed *= -1;
        }
    }
}
