using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color BaseColor, OffsetColor;
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Color WallColor;

    public void Init(bool isOffset, bool isWall = false)
    {
        if (isWall)
        {
            Renderer.color = WallColor;
        }
        else
        {
            Renderer.color = isOffset ? OffsetColor : BaseColor;

        }
    }
}
