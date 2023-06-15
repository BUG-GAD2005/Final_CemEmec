using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceView : MonoBehaviour
{
    public Text goldValueText;
    public Text gemValueText;

    public void SetGoldAndGemView(int goldValue, int gemValue) 
    {
        goldValueText.text = goldValue.ToString();
        gemValueText.text = gemValue.ToString();
    }
}
