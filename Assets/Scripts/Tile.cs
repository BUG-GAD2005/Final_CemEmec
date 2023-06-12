using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied = false;

    public Color vacantColor;
    public Color occupiedColor;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isOccupied == true) 
        {
            spriteRenderer.color = occupiedColor;
        }
        else 
        {
            spriteRenderer.color = vacantColor;
        }
    }
}
