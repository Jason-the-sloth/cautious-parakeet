using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldscript : MonoBehaviour
{
    public GameObject circle;
    public int numberOfCircles = 15;
    public float heightOffset = 4;
    [SerializeField] float widthOffset = 8;
    public GridManager GridManager;
    // Start is called before the first frame update
    void Start()
    {

        RenderCircles();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RenderCircles()
    {
        GridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();

       
        for (int i = 0; i < numberOfCircles; i++)
        {

            Instantiate(circle, new Vector3(Random.Range(1, GridManager.Width-2), Random.Range(1, GridManager.Height-2), 0), transform.rotation);
        }
    }
}
