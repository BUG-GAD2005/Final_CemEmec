using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCardVariables : MonoBehaviour
{
    [SerializeField] public string cardName;
    [SerializeField] public int goldCost;
    [SerializeField] public int gemCost;
    [SerializeField] public Vector2[] tilePositions;
    [SerializeField] public int goldIncreaseValue;
    [SerializeField] public int gemIncreaseValue;
}
