using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSchemes : MonoBehaviour
{
    public GameManager gameManager;

    private float nearestDistance;
    private Tile nearestTile;
    private Tile _2ndTile;

    public void BuildA()
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

        if (!nearestTile.isOccupied && !_2ndTile.isOccupied)
        {
            Vector3 spawnPosition = new Vector3(nearestTile.transform.position.x + ((_2ndTile.transform.position.x - nearestTile.transform.position.x) / 2), nearestTile.transform.position.y, 0);
            Instantiate(gameManager.buildingToPlace, spawnPosition, Quaternion.identity);
            gameManager.buildingToPlace = null;
            nearestTile.isOccupied = true;
            _2ndTile.isOccupied = true;
            gameManager.customCursor.gameObject.SetActive(false);
            //Cursor.visible = true;
        }
    }
}

