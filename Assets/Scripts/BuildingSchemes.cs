using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSchemes : MonoBehaviour
{ 
    public GameManager gameManager;
    public GameObject canvas;

    private float nearestDistance;
    private Tile nearestTile;

    public List<Tile> nearestTileList;


    public Color vacantColor;
    public Color occupiedColor;
    public Color defaultColor;

    private GameObject goldFloatingText;
    private GameObject gemFloatingText;
    private GameObject buildingGoldFloatingText;
    private GameObject buildingGemFloatingText;

    public Transform goldTransform;
    public Transform gemTransform;

    public GameObject textPrefab;




    /*
    public void SetColor() 
    {
        for (int i = 0; i < gameManager.customCursorGameObject.transform.GetChild(0).transform.childCount; i++)
        {
            nearestTileList.Clear();

            nearestDistance = float.MaxValue;
            nearestTile = null;

            foreach (Tile tile in gameManager.tiles)
            {
                float distance = Vector2.Distance(tile.transform.position, gameManager.customCursorGameObject.transform.GetChild(0).transform.GetChild(i).transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTile = tile;
                }
            }

            nearestTileList.Add(nearestTile);
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (IsNearestTilesVacant() && IsNearestTilesDifferent() && hit.collider != null && hit.collider.name == "Grid")
        {
            for (int i = 0; i < gameManager.customCursorGameObject.transform.GetChild(0).transform.childCount; i++)
            {
                SpriteRenderer spriteRenderer = gameManager.customCursorGameObject.transform.GetChild(0).transform.GetChild(i).GetComponent<SpriteRenderer>();
                spriteRenderer.color = vacantColor;
            }
        }
        else
        {
            for (int i = 0; i < gameManager.customCursorGameObject.transform.GetChild(0).transform.childCount; i++)
            {
                SpriteRenderer spriteRenderer = gameManager.customCursorGameObject.transform.GetChild(0).transform.GetChild(i).GetComponent<SpriteRenderer>();
                spriteRenderer.color = occupiedColor;
            }
        }

                gameManager.boool = true;
        
    }

    public bool IsNearestTilesDifferent()
    {
        int totalChecker = 0;

        for (int i = 1; i < nearestTileList.Count; i++)
        {
            for (int a = i - 1; a < i; a++)
            {
                if (nearestTileList[i] != nearestTileList[a])
                {
                    totalChecker++;
                }
            }
        }

        Debug.Log("diff" + totalChecker);

        if (totalChecker == gameManager.customCursorGameObject.transform.GetChild(0).transform.childCount - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsNearestTilesVacant()
    {
        int totalChecker = 0;

        for (int i = 0; i < nearestTileList.Count; i++)
        {
            if (nearestTileList[i].isOccupied==false)
            {
                totalChecker++;
            }
        }

        Debug.Log("vacant" + totalChecker);

        if (totalChecker == gameManager.customCursorGameObject.transform.GetChild(0).transform.childCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
















    /* public void Build() 
     {
         RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

         if (IsNearestTilesDifferent() && IsNearestTilesVacant() && hit.collider != null && hit.collider.name == "Grid")
         {
             for (int i = 0; i < gameManager.buildingToPlace.transform.childCount; i++)
             {
                 SpriteRenderer spriteRenderer = gameManager.buildingToPlace.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                 spriteRenderer.color = defaultColor;
             }

             gameManager.gold -= gameManager.goldCost;
             gameManager.gem -= gameManager.gemCost;
             foreach (BuildingCard thisBuildingCard in gameManager.buildingCards)
             {
                 thisBuildingCard.SetButtonInteractability();
             }

             Vector3 spawnPosition = new Vector3(nearestTile.transform.position.x + ((_2ndTile.transform.position.x - nearestTile.transform.position.x) / 2), nearestTile.transform.position.y, 0);
             placedBuilding = Instantiate(gameManager.buildingToPlace, spawnPosition, Quaternion.identity);
             placedBuilding.GetComponent<Building>().enabled = true;

             goldFloatingText = Instantiate(textPrefab, goldTransform.position, Quaternion.identity, canvas.transform);
             goldFloatingText.GetComponent<FloatingText>().text.text = "-" + gameManager.goldCost;
             gemFloatingText = Instantiate(textPrefab, gemTransform.position, Quaternion.identity, canvas.transform);
             gemFloatingText.GetComponent<FloatingText>().text.text = "-" + gameManager.gemCost;
             buildingGoldFloatingText = Instantiate(textPrefab, new Vector3(spawnPosition.x - 0.4f, spawnPosition.y, spawnPosition.z), Quaternion.identity, canvas.transform);
             buildingGoldFloatingText.GetComponent<FloatingText>().text.text = "-" + gameManager.goldCost;
             buildingGemFloatingText = Instantiate(textPrefab, new Vector3(spawnPosition.x + 0.4f, spawnPosition.y, spawnPosition.z), Quaternion.identity, canvas.transform);
             buildingGemFloatingText.GetComponent<FloatingText>().text.text = "-" + gameManager.gemCost;

             gameManager.buildingToPlaceScript = null;
             nearestTile.isOccupied = true;
             _2ndTile.isOccupied = true;
             Object.Destroy(gameManager.customCursor.gameObject.transform.GetChild(0).gameObject);
         }
         else
         {
             Object.Destroy(gameManager.customCursor.gameObject.transform.GetChild(0).gameObject);
         }
     }

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

         if (nearestTile != _2ndTile && !nearestTile.isOccupied && !_2ndTile.isOccupied && hit.collider != null && hit.collider.name == "Grid")
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

         if (nearestTile != _2ndTile && !nearestTile.isOccupied && !_2ndTile.isOccupied && hit.collider != null && hit.collider.name == "Grid")
         {
             for (int i = 0; i < gameManager.buildingToPlace.transform.childCount; i++)
             {
                 SpriteRenderer spriteRenderer = gameManager.buildingToPlace.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                 spriteRenderer.color = defaultColor;
             }

             gameManager.gold -= gameManager.goldCost;
             gameManager.gem -= gameManager.gemCost;
             foreach (BuildingCard thisBuildingCard in gameManager.buildingCards)
             {
                 thisBuildingCard.SetButtonInteractability();
             }

             Vector3 spawnPosition = new Vector3(nearestTile.transform.position.x + ((_2ndTile.transform.position.x - nearestTile.transform.position.x) / 2), nearestTile.transform.position.y, 0);
             placedBuilding = Instantiate(gameManager.buildingToPlace, spawnPosition, Quaternion.identity);
             placedBuilding.GetComponent<Building>().enabled = true;

             goldFloatingText = Instantiate(textPrefab, goldTransform.position, Quaternion.identity, canvas.transform);
             goldFloatingText.GetComponent<FloatingText>().text.text = "-" + gameManager.goldCost;
             gemFloatingText = Instantiate(textPrefab, gemTransform.position, Quaternion.identity, canvas.transform);
             gemFloatingText.GetComponent<FloatingText>().text.text = "-" + gameManager.gemCost;
             buildingGoldFloatingText = Instantiate(textPrefab, new Vector3(spawnPosition.x - 0.4f, spawnPosition.y, spawnPosition.z), Quaternion.identity, canvas.transform);
             buildingGoldFloatingText.GetComponent<FloatingText>().text.text = "-" + gameManager.goldCost;
             buildingGemFloatingText = Instantiate(textPrefab, new Vector3(spawnPosition.x + 0.4f, spawnPosition.y, spawnPosition.z), Quaternion.identity, canvas.transform);
             buildingGemFloatingText.GetComponent<FloatingText>().text.text = "-" + gameManager.gemCost;

             gameManager.buildingToPlaceScript = null;
             nearestTile.isOccupied = true;
             _2ndTile.isOccupied = true;
             Object.Destroy(gameManager.customCursor.gameObject.transform.GetChild(0).gameObject);
         }
         else 
         {
             Object.Destroy(gameManager.customCursor.gameObject.transform.GetChild(0).gameObject);
         }
     }
    */
}

