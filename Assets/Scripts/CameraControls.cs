using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize movement direction
        Vector3 moveDirection = Vector3.zero;

        // Check for each arrow key and adjust the move direction accordingly
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += Vector3.down;
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
