using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private GameManager gameManager;

    [HideInInspector] public int goldIncrease;
    [HideInInspector] public int gemIncrease;
    private float increaseCountdown;
    private float nextIncreaseTime;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        increaseCountdown = gameManager.activeBuildingCardVariables.increaseCountdown;
        goldIncrease = gameManager.activeBuildingCardVariables.goldIncreaseValue;
        gemIncrease = gameManager.activeBuildingCardVariables.gemIncreaseValue;

        nextIncreaseTime = increaseCountdown;
    }

    private void Update()
    {
        nextIncreaseTime -= 1 * Time.deltaTime;

        if (nextIncreaseTime <= 0) 
        {
            nextIncreaseTime = increaseCountdown;

            gameManager.gold += goldIncrease;
            gameManager.gem += gemIncrease;

            gameManager.gameObject.GetComponent<ResourceView>().SetGoldAndGemView(gameManager.gold, gameManager.gem);
                
            gameManager.SpawnGeneratedResourceFloatingTexts(this.gameObject);
            gameManager.SpawnBuildingGeneratedFloatingTexts(this.gameObject);
        }
    }

    

}
