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

    private GameObject buildingPrefab;
    public GameObject buildingToPlace;
    public Building buildingToPlaceScript;
    public BuildingSchemes buildingSchemes;
    private int buildingIndex;

    public CustomCursor customCursor;

    public Tile[] tiles;
    public BuildingCard[] buildingCards;

    private void Update()
    {
        goldText.text = gold.ToString();
        gemText.text = gem.ToString();

        if (buildingToPlace != null) 
        {
            if (buildingIndex == 0) 
            {
                buildingSchemes.SetColorA();

                if (Input.GetMouseButtonDown(0)) 
                {
                    buildingSchemes.BuildA();
                }
            }
        }
    }

    public void BuyBuilding(BuildingCard buildingCard) 
    {
        buildingPrefab = Instantiate(buildingCard.buildingPrefab, customCursor.transform.position, Quaternion.identity);
        buildingPrefab.transform.parent = customCursor.gameObject.transform;

        gold -= buildingCard.goldCost;
        gem -= buildingCard.gemCost;
        foreach(BuildingCard thisBuildingCard in buildingCards) 
        {
            thisBuildingCard.SetButtonInteractability();
        }

        buildingToPlace = buildingCard.buildingPrefab;
        buildingToPlaceScript = buildingCard.building;
        buildingIndex = buildingCard.buildingIndex;
    }
}
