using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject buildingCounterPrefab;
    private Slider slider;
    private Text counterText;

    [HideInInspector] public int goldIncrease;
    [HideInInspector] public int gemIncrease;
    [HideInInspector] public float increaseCountdown;
    private float nextIncreaseTime;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        increaseCountdown = gameManager.activeBuildingCardVariables.increaseCountdown;
        goldIncrease = gameManager.activeBuildingCardVariables.goldIncreaseValue;
        gemIncrease = gameManager.activeBuildingCardVariables.gemIncreaseValue;

        nextIncreaseTime = increaseCountdown;

        Vector2 spawnPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.4f);
        slider = Instantiate(buildingCounterPrefab, spawnPoint, Quaternion.identity, GameObject.Find("CounterHolder").transform).GetComponentInChildren<Slider>();
        counterText = slider.gameObject.GetComponentInChildren<Text>();

        counterText.text = increaseCountdown.ToString("F0");

        slider.minValue = 0;
        slider.maxValue = increaseCountdown;
        slider.wholeNumbers = false;
        slider.value = slider.minValue;
    }

    private void Update()
    {
        nextIncreaseTime -= 1 * Time.deltaTime;
        counterText.text = nextIncreaseTime.ToString("F0");
        slider.value += Time.deltaTime;

        if (nextIncreaseTime <= 0) 
        {
            nextIncreaseTime = increaseCountdown;
            counterText.text = increaseCountdown.ToString("F0");
            slider.value = slider.minValue;

            gameManager.gold += goldIncrease;
            gameManager.gem += gemIncrease;

            gameManager.SetBuildingCardButtonsInteractibility();
            gameManager.gameObject.GetComponent<ResourceView>().SetGoldAndGemView(gameManager.gold, gameManager.gem);
                
            gameManager.SpawnGeneratedResourceFloatingTexts(this.gameObject);
            gameManager.SpawnBuildingGeneratedFloatingTexts(this.gameObject);
        }
    }
}
