using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BoardSetUpScript : MonoBehaviour
{
    public float rectangleWidth = 10f;
    public float rectangleHeight = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obstacles = GameObject.Find("Obstacles");
        GameObject circle = GameObject.Find("Circle");
        GameObject borders = GameObject.Find("Borders");
        GameObject border = GameObject.Find("BorderE");
        

        GameObject duplicateBorder = Instantiate(border, new Vector2(0,8), Quaternion.identity);
        duplicateBorder.transform.Rotate(new Vector3(0,0, 90));
        duplicateBorder.name = "BoardN";
        duplicateBorder.transform.SetParent(borders.transform, true);

        duplicateBorder = Instantiate(border, new Vector2(-10,0), Quaternion.identity);
        duplicateBorder.name = "BoardW";
        duplicateBorder.transform.SetParent(borders.transform, true);

        duplicateBorder = Instantiate(border, new Vector2(0,-8), Quaternion.identity);
        duplicateBorder.transform.Rotate(new Vector3(0,0, 90));
        duplicateBorder.name = "BoardS";
        duplicateBorder.transform.SetParent(borders.transform, true);

        for (int  i = 0; i < 10; i++)
        {
            float x = UnityEngine.Random.Range(-rectangleWidth / 2, rectangleWidth / 2);
            float y = UnityEngine.Random.Range(-rectangleHeight / 2, rectangleHeight / 2);
            Vector2 randomPosition = new(x, y);
            GameObject duplicateCircle = Instantiate(circle, randomPosition, Quaternion.identity);
            duplicateCircle.name = "DupCircle" + i;
            duplicateCircle.transform.SetParent(obstacles.transform, true);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
