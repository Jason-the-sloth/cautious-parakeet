using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public int Width, Height;
    [SerializeField] private Tile Tile;
    [SerializeField] private Transform Camera;

    void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var spawnTile = Instantiate(Tile, new Vector3(x, y), Quaternion.identity);
                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                bool isWall = false;
                spawnTile.name = $"Grid : {x} {y} : {isOffset}";

                if (x == 0 || x == Width - 1 || y == Height - 1 || y == 0)
                {
                    spawnTile.tag = "wall";
                    var rigidbody = spawnTile.AddComponent<Rigidbody2D>();
                    rigidbody.bodyType = RigidbodyType2D.Static;
                    var collider = spawnTile.AddComponent<BoxCollider2D>();
                    isWall = true;
                }
                spawnTile.Init(isOffset, isWall);
            }

            Camera.transform.position = new Vector3((float)Width / 2 - 0.5f, (float)Height / 2 - 0.5f, -10);
        }
    }
}
