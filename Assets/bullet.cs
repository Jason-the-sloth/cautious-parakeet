using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float life = 2f;
    public logic logic;
    private GridManager GridManager;
    public float heightOffset = 3;
    [SerializeField] float widthOffset = 4;
    public GameObject Bot;
    void Awake()
    {
        Destroy(gameObject, life);
    }
    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logic>();
        GridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "wall")
        {
            Destroy(gameObject);

            if (collision.gameObject.tag == "Bot")
            {
                logic.addScore();
                var rotation = collision.gameObject.transform.rotation;
                rotation.z = (new float[] { 0,90,190,270 })[Random.Range(0, 4)];
                
                Instantiate(Bot, new Vector3(Random.Range(1, GridManager.Width-1), Random.Range(1, GridManager.Height-1), 0), rotation );
                
            }

            Destroy(collision.gameObject);
            

        }
        else
        {
            Destroy(gameObject);
        }
        
    }
 
}
