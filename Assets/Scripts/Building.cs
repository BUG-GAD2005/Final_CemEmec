using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject textPrefab;
    public GameObject canvas;
    private GameObject goldFloatingText;
    private GameObject gemFloatingText;
    private GameObject buildingGoldFloatingText;
    private GameObject buildingGemFloatingText;

    public Transform goldTransform;
    public Transform gemTransform;

    public int BuildingIndex;

    public int goldIncrease;
    public int gemIncrease;
    public float IncreaseCountdown;
    private float nextIncreaseTime;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        nextIncreaseTime = IncreaseCountdown;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        goldTransform = canvas.transform.FindChild("Gold").transform;
        gemTransform = canvas.transform.FindChild("Gem").transform;
    }

    private void Update()
    {
        if (Time.time > nextIncreaseTime) 
        {
            nextIncreaseTime = Time.time + IncreaseCountdown;

            gameManager.gold += goldIncrease;
            gameManager.gem += gemIncrease;


            goldFloatingText = Instantiate(textPrefab, goldTransform.position, Quaternion.identity, canvas.transform);
            goldFloatingText.GetComponent<FloatingText>().text.text = "+" + goldIncrease;
            gemFloatingText = Instantiate(textPrefab, gemTransform.position, Quaternion.identity, canvas.transform);
            gemFloatingText.GetComponent<FloatingText>().text.text = "+" + gemIncrease;

            if (BuildingIndex == 0) 
            {
            buildingGoldFloatingText = Instantiate(textPrefab, new Vector3(transform.position.x - 0.4f, transform.position.y, transform.position.z), Quaternion.identity, canvas.transform);
            buildingGoldFloatingText.GetComponent<FloatingText>().text.text = "+" + goldIncrease;
            buildingGemFloatingText = Instantiate(textPrefab, new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z), Quaternion.identity, canvas.transform);
            buildingGemFloatingText.GetComponent<FloatingText>().text.text = "+" + gemIncrease;
            }
        }
    }
}
