using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCardViews : MonoBehaviour
{
    private BuildingCardVariables buildingCardVariables;

    [SerializeField] private Text cardName;
    [SerializeField] private Text cardGoldCost;
    [SerializeField] private Text cardGemCost;

    private void Start()
    {
        buildingCardVariables = gameObject.GetComponent<BuildingCardVariables>();

        cardName.text = buildingCardVariables.cardName;
        cardGoldCost.text = buildingCardVariables.goldCost.ToString();
        cardGemCost.text = buildingCardVariables.gemCost.ToString();

    }
}
