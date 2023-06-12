using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCard : MonoBehaviour
{
    public int goldCost;
    public int gemCost;

    public GameObject buildingPrefab;
    public Building building;

    private void Start()
    {
        building = buildingPrefab.GetComponent<Building>();
    }
}
