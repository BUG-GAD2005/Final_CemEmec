using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCard : MonoBehaviour
{
    public int goldCost;
    public int gemCost;
    public Text goldCostText;
    public Text gemCostText;

    public int buildingIndex;
    public GameObject buildingPrefab;
    public Building building;

    private void Start()
    {
        building = buildingPrefab.GetComponent<Building>();
        goldCostText.text = goldCost.ToString();
        gemCostText.text = gemCost.ToString();
    }
}
