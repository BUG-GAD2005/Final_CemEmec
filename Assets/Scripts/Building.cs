using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private GameManager gameManager;

    public int BuildingIndex;

    public int goldIncrease;
    public int gemIncrease;
    public float IncreaseCountdown;
    private float nextIncreaseTime;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Time.time > nextIncreaseTime) 
        {
            nextIncreaseTime = Time.time + IncreaseCountdown;
            gameManager.gold += goldIncrease;
            gameManager.gem += gemIncrease;
        }
    }
}
