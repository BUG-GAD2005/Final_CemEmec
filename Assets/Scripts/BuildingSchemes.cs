using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSchemes : MonoBehaviour
{
    public GameManager gameManager;

    public Color vacantColor;
    public Color occupiedColor;
    public Color defaultColor;

    private float nearestDistance;
    private Tile nearestTile;
    private Tile _2ndTile;

    public void SetColorA() 
    {
        nearestDistance = float.MaxValue;
        nearestTile = null;

        foreach (Tile tile in gameManager.tiles)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.x -= 0.4f;
            float distance = Vector2.Distance(tile.transform.position, mousePosition);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTile = tile;
            }
        }

        nearestDistance = float.MaxValue;
        _2ndTile = null;

        foreach (Tile tile in gameManager.tiles)
        {
            Vector2 _2ndTileVector = new Vector2(nearestTile.transform.position.x + 0.8f, nearestTile.transform.position.y);
            float distance = Vector2.Distance(tile.transform.position, _2ndTileVector);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                _2ndTile = tile;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (!nearestTile.isOccupied && !_2ndTile.isOccupied && hit.collider != null && hit.collider.name == "Grid")
        {
            for (int i = 0; i < gameManager.customCursor.transform.GetChild(0).gameObject.transform.childCount; i++)
            {
                SpriteRenderer spriteRenderer = gameManager.customCursor.transform.GetChild(0).gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                spriteRenderer.color = vacantColor;
            }
        }
        else
        {
            for (int i = 0; i < gameManager.customCursor.transform.GetChild(0).gameObject.transform.childCount; i++)
            {
                SpriteRenderer spriteRenderer = gameManager.customCursor.transform.GetChild(0).gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                spriteRenderer.color = occupiedColor;
            }
        }
    }

    public void BuildA()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (!nearestTile.isOccupied && !_2ndTile.isOccupied && hit.collider != null && hit.collider.name == "Grid")
        {
            Debug.Log("Target name: " + hit.collider.name);

            for (int i = 0; i < gameManager.buildingToPlace.transform.childCount; i++)
            {
                SpriteRenderer spriteRenderer = gameManager.buildingToPlace.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.color = defaultColor;
            }

            Vector3 spawnPosition = new Vector3(nearestTile.transform.position.x + ((_2ndTile.transform.position.x - nearestTile.transform.position.x) / 2), nearestTile.transform.position.y, 0);
            Instantiate(gameManager.buildingToPlaceScript, spawnPosition, Quaternion.identity);
            gameManager.buildingToPlaceScript = null;
            nearestTile.isOccupied = true;
            _2ndTile.isOccupied = true;
            Object.Destroy(gameManager.customCursor.gameObject.transform.GetChild(0).gameObject);
        }
    }

}

