using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Text text;
    public float duration;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y + 0.01f, text.transform.position.y);
    }


    public void SetFloatingTextValue(int value,string _operator) 
    {
        text.text = _operator + value.ToString();
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
