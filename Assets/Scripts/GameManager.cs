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

    public GameObject grid;
    private Building buildingToPlace;
    private GameObject buildingPrefab;


    public CustomCursor customCursor;

    private void Update()
    {
        goldText.text = gold.ToString();
        gemText.text = gem.ToString();
    }

    public void BuyBuilding(Building building) 
    {
        if (gold >= building.goldCost && gem >= building.gemCost) 
        {
            customCursor.gameObject.SetActive(true);
            if (customCursor.transform.childCount > 0) 
            {
                Object.Destroy(customCursor.gameObject.transform.GetChild(0).gameObject);
            }
            buildingPrefab = Instantiate(building.buildingPrefab);
            buildingPrefab.transform.parent = customCursor.gameObject.transform;
            Cursor.visible = false;
            
            gold -= building.goldCost;
            gem -= building.gemCost;
            buildingToPlace = building;
        }
    }
}
