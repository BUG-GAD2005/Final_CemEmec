using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int gold;
    public int gem;
    public Text goldText;
    public Text gemText;

    private BuildingCard buildingCard;
    private GameObject buildingPrefab;
    private Building buildingToPlace;

    public CustomCursor customCursor;

    public Tile[] tiles;    

    private void Update()
    {
        goldText.text = gold.ToString();
        gemText.text = gem.ToString();

        if (Input.GetMouseButtonDown(0) && buildingToPlace != null) 
        {
            Tile nearestTile = null;
            float nearestDistance = float.MaxValue;
            foreach(Tile tile in tiles) 
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
            if (nearestTile.isOccupied == false) 
            {
                Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);
                buildingToPlace = null;
                nearestTile.isOccupied = true;
                customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }
        }
    }

    public void BuyBuilding(BuildingCard buildingCard) 
    {
        if (gold >= buildingCard.goldCost && gem >= buildingCard.gemCost) 
        {
            customCursor.gameObject.SetActive(true);
            if (customCursor.transform.childCount > 0) 
            {
                Object.Destroy(customCursor.gameObject.transform.GetChild(0).gameObject);
            }
            buildingPrefab = Instantiate(buildingCard.buildingPrefab);
            buildingPrefab.transform.parent = customCursor.gameObject.transform;
            //Cursor.visible = false;
            
            gold -= buildingCard.goldCost;
            gem -= buildingCard.gemCost;
            buildingToPlace = buildingCard.building;
        }
    }
}
