using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int startGold;
    [SerializeField] private int startGem;

    [HideInInspector] public int gold;
    [HideInInspector] public int gem;
    private List<Button> buildingCardButtons;

    [HideInInspector] public GameObject canvas;
    [HideInInspector] private GameObject grid;
    [HideInInspector] public GameObject buildingHolder;
    [HideInInspector] public List<GameObject> tiles;

    [HideInInspector] private GameObject customCursor;
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private GameObject floatingText;
    private GameObject activeBuildingPrefab;
    [HideInInspector] public BuildingCardVariables activeBuildingCardVariables;

    [HideInInspector] public GameObject buildingToPlace;
    [HideInInspector] public BuildingCardVariables buildingCardVariablesToPlace;

    [HideInInspector] private ResourceView resourceView;
    [HideInInspector] private TilePrefabCreator tilePrefabCreator;

    private bool isSetColor = false;
    private bool isBuildingPlaceable = false;
    [HideInInspector] private List<GameObject> pickedTiles;
    [SerializeField] private Color vacantColor;
    [SerializeField] private Color occupiedColor;
    [SerializeField] private Color defaultColor;

    private string saveFilePath = "saveData.json";
    private GameData gameData;

    private void Start()
    {
        gold = startGold;
        gem = startGem;

        resourceView = gameObject.GetComponent<ResourceView>();
        resourceView.SetGoldAndGemView(gold, gem);
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        buildingHolder = GameObject.Find("BuildingHolder");

        GetBuildingCardButtons();
        SetBuildingCardButtonsInteractibility();

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

    private void OnDestroy()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        gameData = new GameData();

        gameData.gold = gold;
        gameData.gem = gem;
        gameData.buildings = new List<GameObject>();

        for (int i = 0; i < buildingHolder.transform.childCount; i++)
        {
            gameData.buildings.Add(buildingHolder.transform.GetChild(i).gameObject);
        }

        string jsonData = JsonUtility.ToJson(gameData);

        File.WriteAllText(saveFilePath, jsonData);
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

    private void GetBuildingCardButtons()
    {
        buildingCardButtons = new List<Button>();
        GameObject[] buildingCardsArray = GameObject.FindGameObjectsWithTag("BuildingCard");

        foreach (GameObject buildingCard in buildingCardsArray)
        {
            buildingCardButtons.Add(buildingCard.GetComponent<Button>());
        }
    }

    private void SetBuildingCardButtonsInteractibility()
    {
        foreach (Button button in buildingCardButtons)
        {
            if (gold >= button.gameObject.GetComponent<BuildingCardVariables>().goldCost && gem >= button.gameObject.GetComponent<BuildingCardVariables>().gemCost)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    private void SetAllBuildingCardsButtonsInactive() 
    {
        foreach (Button button in buildingCardButtons)
        {
            button.interactable = false;
        }
    }

    public void GetClickedBuilding(BuildingCardVariables buildingCardVariables)
    {
        SetAllBuildingCardsButtonsInactive();

        activeBuildingPrefab = Instantiate(buildingPrefab, customCursor.transform);
        tilePrefabCreator.CreateTiles(activeBuildingPrefab, buildingCardVariables.tilePositions);

        activeBuildingCardVariables = buildingCardVariables;
        buildingToPlace = activeBuildingPrefab;
        isSetColor = true;
    }

    private void SetColor()
    {
        pickedTiles = new List<GameObject>();

        int blockCount = activeBuildingPrefab.transform.childCount;

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
        for (int i = 0; i < pickedTiles.Count; i++)
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
            gold -= activeBuildingCardVariables.goldCost;
            gem -= activeBuildingCardVariables.gemCost;

            for (int i = 0; i < blockCount; i++)
            {
                SpriteRenderer spriteRenderer = buildingToPlace.transform.GetChild(i).GetComponent<SpriteRenderer>();
                spriteRenderer.color = defaultColor;
            }

            for (int i = 0; i < blockCount; i++)
            {
                pickedTiles[i].GetComponent<Tile>().isOccupied = true;
            }

            Vector2 distanceBtwMouseAndBlock0 = new Vector2(
                customCursor.transform.position.x - activeBuildingPrefab.transform.GetChild(0).position.x,
                customCursor.transform.position.y - activeBuildingPrefab.transform.GetChild(0).position.y);

            Vector2 spawnPoint = new Vector2(
                pickedTiles[0].transform.position.x + distanceBtwMouseAndBlock0.x,
                pickedTiles[0].transform.position.y + distanceBtwMouseAndBlock0.y);

            GameObject activeBuildingToPlace = Instantiate(buildingToPlace, spawnPoint, Quaternion.identity, buildingHolder.transform);

            SpawnResourceFloatingTexts();
            SpawnBuildingFloatingTexts(spawnPoint);

            SetBuildingCardButtonsInteractibility();

            resourceView.SetGoldAndGemView(gold, gem);

            activeBuildingToPlace.GetComponent<Building>().enabled = true;

            Destroy(activeBuildingPrefab);
        }
        else if (Input.GetMouseButton(0) && !isBuildingPlaceable)
        {
            Destroy(activeBuildingPrefab);
        }
    }

    private void SpawnResourceFloatingTexts() 
    {
        Vector2 goldSpawnPoint = resourceView.goldValueText.gameObject.transform.parent.transform.position;
        Vector2 gemSpawnPoint = resourceView.gemValueText.gameObject.transform.parent.transform.position;

        GameObject goldFloatingText = Instantiate(floatingText, goldSpawnPoint, Quaternion.identity, canvas.transform);
        GameObject gemFloatingText = Instantiate(floatingText, gemSpawnPoint, Quaternion.identity, canvas.transform);

        goldFloatingText.GetComponent<FloatingText>().SetFloatingTextValue(activeBuildingCardVariables.goldCost, "-");
        gemFloatingText.GetComponent<FloatingText>().SetFloatingTextValue(activeBuildingCardVariables.gemCost, "-");
    }

    private void SpawnBuildingFloatingTexts(Vector2 spawnPoint) 
    {
        Vector2 goldSpawnPoint = new Vector2(spawnPoint.x - 0.4f, spawnPoint.y);
        Vector2 gemSpawnPoint = new Vector2(spawnPoint.x + 0.4f, spawnPoint.y);

        GameObject goldFloatingText = Instantiate(floatingText, goldSpawnPoint, Quaternion.identity, canvas.transform);
        GameObject gemFloatingText = Instantiate(floatingText, gemSpawnPoint, Quaternion.identity, canvas.transform);

        goldFloatingText.GetComponent<FloatingText>().SetFloatingTextValue(activeBuildingCardVariables.goldCost, "-");
        gemFloatingText.GetComponent<FloatingText>().SetFloatingTextValue(activeBuildingCardVariables.gemCost, "-");
    }

    public void SpawnGeneratedResourceFloatingTexts(GameObject building) 
    {
        Vector2 goldSpawnPoint = resourceView.goldValueText.gameObject.transform.parent.transform.position;
        Vector2 gemSpawnPoint = resourceView.gemValueText.gameObject.transform.parent.transform.position;

        GameObject goldFloatingText = Instantiate(floatingText, goldSpawnPoint, Quaternion.identity, canvas.transform);
        GameObject gemFloatingText = Instantiate(floatingText, gemSpawnPoint, Quaternion.identity, canvas.transform);

        goldFloatingText.GetComponent<FloatingText>().SetFloatingTextValue(building.GetComponent<Building>().goldIncrease, "+");
        gemFloatingText.GetComponent<FloatingText>().SetFloatingTextValue(building.GetComponent<Building>().gemIncrease, "+");
    }

    public void SpawnBuildingGeneratedFloatingTexts(GameObject building) 
    {
        Vector2 goldSpawnPoint = new Vector2(building.transform.position.x - 0.4f, building.transform.position.y);
        Vector2 gemSpawnPoint = new Vector2(building.transform.position.x + 0.4f, building.transform.position.y);

        GameObject goldFloatingText = Instantiate(floatingText, goldSpawnPoint, Quaternion.identity, canvas.transform);
        GameObject gemFloatingText = Instantiate(floatingText, gemSpawnPoint, Quaternion.identity, canvas.transform);

        goldFloatingText.GetComponent<FloatingText>().SetFloatingTextValue(building.GetComponent<Building>().goldIncrease, "+");
        gemFloatingText.GetComponent<FloatingText>().SetFloatingTextValue(building.GetComponent<Building>().gemIncrease, "+");
    }
}




