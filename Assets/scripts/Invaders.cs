using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector3 _direction = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        this.transform.position += _direction * speed * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorlPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorlPoint(Vector3.right);
        if(_direction == Vector3.right &&  leftEdge != rightEdge)
        {
            AdvanceRow();
        }

    }

    private void AdvanceRow()
    {
        _direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }*/
}
