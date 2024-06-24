using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUpScript : MonoBehaviour
{
    public GameObject playerTriangle;

    public Vector2 p1;
    public Vector2 p2;

    // Start is called before the first frame update
    void Start()
    {
        createPlayer(p1);
        createPlayer(p2);
    }

    void createPlayer(Vector2 vec)
    {
        GameObject player = Instantiate(playerTriangle, vec, Quaternion.identity);
        player.transform.Rotate(new Vector3(0,0,180-Vector2.SignedAngle(vec, Vector2.up)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
