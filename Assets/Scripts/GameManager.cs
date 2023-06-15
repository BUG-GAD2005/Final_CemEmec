using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int gold;
    [SerializeField] public int gem;

    [HideInInspector] private GameObject grid;
    [HideInInspector] public List<GameObject> tiles;

    [HideInInspector] private GameObject customCursor;
    [SerializeField] private GameObject buildingPrefab;
    private GameObject activeBuildingPrefab;

    [HideInInspector] public GameObject buildingToPlace;
    [HideInInspector] public BuildingCardVariables buildingCardVariablesToPlace;

    [HideInInspector] private TilePrefabCreator tilePrefabCreator;

    private bool isSetColor = false;
    private bool isBuildingPlaceable = false;
    [HideInInspector] private List<GameObject> pickedTiles;
    [SerializeField] private Color vacantColor;
    [SerializeField] private Color occupiedColor;
    [SerializeField] private Color defaultColor;

    private void Start()
    {
        SetGridTilemap();
        customCursor = GameObject.FindGameObjectWithTag("CustomCursor");
        tilePrefabCreator = gameObject.GetComponent<TilePrefabCreator>();
    }

    private void Update()
    {
        if (isSetColor) 
        {
            isSetColor = false;
            SetColor();
        }
    }

    private void SetGridTilemap() 
    {
        tiles = new List<GameObject>();
        grid = GameObject.FindGameObjectWithTag("Grid");
        int numberOfTiles = GameObject.FindGameObjectsWithTag("Tile").Length;

        for (int i = 0; i < numberOfTiles; i++)
        {
            tiles.Add(grid.transform.GetChild(i).gameObject);
        }
    }

    public void GetClickedBuilding(BuildingCardVariables buildingCardVariables) 
    {
        activeBuildingPrefab = Instantiate(buildingPrefab, customCursor.transform);
        tilePrefabCreator.CreateTiles(activeBuildingPrefab, buildingCardVariables.tilePositions);

        buildingToPlace = activeBuildingPrefab;
        isSetColor = true;
    }

    private void SetColor() 
    {
        pickedTiles = new List<GameObject>();

        int blockCount = activeBuildingPrefab.transform.childCount;
        int tileCount= GameObject.FindGameObjectsWithTag("Tile").Length;

        MatchBlocksAndTiles(blockCount);

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        PaintBlocks(blockCount, hit);

        PlaceBuilding(blockCount);

        isSetColor = true;
    }

    private void MatchBlocksAndTiles(int blockCount)
    {
        for (int i = 0; i < blockCount; i++)
        {
            float nearestDistance = float.MaxValue;
            GameObject nearestTile = null;

            foreach (GameObject tile in tiles)
            {

                GameObject blockGameObject = activeBuildingPrefab.transform.GetChild(i).gameObject;
                float distance = Vector2.Distance(tile.transform.position, blockGameObject.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTile = tile;
                }
            }

            pickedTiles.Add(nearestTile);
        }
    }

    private void PaintBlocks(int blockCount, RaycastHit2D hit)
    {
        if (IsNearestTilesVacant() && IsNearestTilesDifferent() && hit.collider != null && hit.collider.name == "Grid")
        {
            for (int i = 0; i < blockCount; i++)
            {
                isBuildingPlaceable = true;
                SpriteRenderer spriteRenderer = activeBuildingPrefab.transform.GetChild(i).GetComponent<SpriteRenderer>();
                spriteRenderer.color = vacantColor;
            }
        }
        else
        {
            for (int i = 0; i < blockCount; i++)
            {
                isBuildingPlaceable = false;
                SpriteRenderer spriteRenderer = activeBuildingPrefab.transform.GetChild(i).GetComponent<SpriteRenderer>();
                spriteRenderer.color = occupiedColor;
            }
        }
    }

    private bool IsNearestTilesDifferent()
    {
        for (int i = 1; i < pickedTiles.Count; i++)
        {
            for (int a = i - 1; a < i; a++)
            {
                if (pickedTiles[i] == pickedTiles[a])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsNearestTilesVacant()
    {
        for(int i = 0; i < pickedTiles.Count; i++) 
        {
            if (pickedTiles[i].GetComponent<Tile>().isOccupied) 
            {
                return false;
            }
        }

        return true;
    }

    private void PlaceBuilding(int blockCount) 
    {
        if (Input.GetMouseButton(0) && isBuildingPlaceable)
        {
            for (int i = 0; i < blockCount; i++)
            {
                SpriteRenderer spriteRenderer = buildingToPlace.transform.GetChild(i).GetComponent<SpriteRenderer>();
                spriteRenderer.color = defaultColor;
            }

            Vector2 distanceBtwMouseAndBlock0 = new Vector2(
                customCursor.transform.position.x - activeBuildingPrefab.transform.GetChild(0).position.x,
                customCursor.transform.position.y - activeBuildingPrefab.transform.GetChild(0).position.y);

            Vector2 spawnPoint = new Vector2(
                pickedTiles[0].transform.position.x + distanceBtwMouseAndBlock0.x,
                pickedTiles[0].transform.position.y + distanceBtwMouseAndBlock0.y);

            Instantiate(buildingToPlace, spawnPoint, Quaternion.identity);

            for (int i = 0; i < blockCount; i++)
            {
                pickedTiles[i].GetComponent<Tile>().isOccupied = true;
            }

            Destroy(activeBuildingPrefab);
        }
        else if(Input.GetMouseButton(0) && !isBuildingPlaceable) 
        {
            Destroy(activeBuildingPrefab);
        }
    }
}

/*public class ZortManager
{
    [SerializeField] private GameObject buildingPrefabGameObject;
    [SerializeField] public GameObject customCursorGameObject;

    public GameObject buildingTile;


    public bool boool;


    public int gold;
    public int gem;
    public Text goldText;
    public Text gemText;

    public int goldCost;
    public int gemCost;

    public GameObject buildingToPlace;
    public Building buildingToPlaceScript;
    public BuildingSchemes buildingSchemes;
    private int buildingIndex;

    

    public Tile[] tiles;
    public BuildingCard[] buildingCards;


    private void Update()
    {
        if (buildingToPlace != null)
        {

            if (boool) 
            {
                boool = false;
                buildingSchemes.SetColor();
            }

            if (Input.GetMouseButtonDown(0))
            {
                //  buildingSchemes.Build();
            }
        }
    }


    public void BuyBuilding(GameObject buildingCardGameObject) 
    {

        boool = true;

        TilePrefabCreator tilePrefabCreator = buildingCardGameObject.GetComponent<TilePrefabCreator>();

        //buildingPrefabGameObject = Instantiate(buildingPrefabGameObject, customCursorGameObject.transform.position, Quaternion.identity, customCursorGameObject.transform);
        tilePrefabCreator.CreateTiles(buildingPrefabGameObject);

        buildingToPlace = buildingPrefabGameObject;
    }



















    /*public void NewBuyBuilding(BuildingCard buildingCard)
    {


        goldCost = buildingCard.goldCost;
        gemCost = buildingCard.gemCost;

        buildingToPlace = buildingCard.buildingPrefab;
        buildingToPlaceScript = buildingCard.building;
        buildingIndex = buildingCard.buildingIndex;
        //tilePrefabCreator = buildingCard.GetComponent<TilePrefabCreator>();

        //buildingPrefab = Instantiate(buildingPrefab, customCursor.transform.position, Quaternion.identity, customCursor.gameObject.transform);
        //tilePrefabCreator.CreateTiles(buildingPrefab);
    }*/




