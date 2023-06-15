using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject TilePrefab;
    [HideInInspector] public List<GameObject> tileGameObjects;

    public void CreateTiles(GameObject buildingPrefab,Vector2[] tilePositions) 
    {
        tileGameObjects = new List<GameObject>();

        for (int i = 0; i < tilePositions.Length; i++)
        {
            Vector2 spawnPoint = new Vector2(
                tilePositions[i].x + buildingPrefab.transform.position.x,
                tilePositions[i].y + buildingPrefab.transform.position.y
                );

            GameObject spawnedTile = Instantiate(TilePrefab, spawnPoint, Quaternion.identity, buildingPrefab.transform);

            tileGameObjects.Add(spawnedTile);
        }
    }
}
