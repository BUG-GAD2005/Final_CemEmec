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

    private void Update()
    {
        goldText.text = gold.ToString();
        gemText.text = gem.ToString();
    }

    public void BuyBuilding(Building building) 
    {
        if (gold >= building.goldCost && gem >= building.gemCost) 
        {
            gold -= building.goldCost;
            gem -= building.gemCost;
            buildingToPlace = building;
        }
    }
}
