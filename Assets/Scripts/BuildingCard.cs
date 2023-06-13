using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCard : MonoBehaviour
{
    private GameManager gameManager;

    public int goldCost;
    public int gemCost;
    public Text goldCostText;
    public Text gemCostText;

    private Button button;

    public int buildingIndex;
    public GameObject buildingPrefab;
    public Building building;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        building = buildingPrefab.GetComponent<Building>();
        goldCostText.text = goldCost.ToString();
        gemCostText.text = gemCost.ToString();
        button = GetComponent<Button>();
    }

    public void SetButtonInteractability() 
    {
        if (gameManager.gold >= goldCost && gameManager.gem >= gemCost)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
