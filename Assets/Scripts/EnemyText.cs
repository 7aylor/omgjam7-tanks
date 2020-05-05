using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyText : MonoBehaviour
{
    TextMeshProUGUI text;
    int numEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        EventBroker.EnemiesChanged += UpdateEnemyCountText;
        text = GetComponent<TextMeshProUGUI>();
        numEnemies = FindObjectsOfType<Enemy>().Length;
        UpdateEnemyCountText(0);
    }

    public void UpdateEnemyCountText(int amount)
    {
        numEnemies += amount;
        text.text = numEnemies.ToString();
    }
}
