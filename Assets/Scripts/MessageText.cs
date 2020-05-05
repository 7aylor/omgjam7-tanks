using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageText : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField]
    float timer = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        EventBroker.DisplayMessage += DisplayMessage;
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    public void DisplayMessage(string message)
    {
        text.text = message;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(timer);
        text.text = "";
    }
}
