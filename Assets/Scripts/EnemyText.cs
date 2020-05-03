using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyText : MonoBehaviour
{
    TextMeshProUGUI text;
    int numEnemies = 0;
    PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        numEnemies = FindObjectsOfType<Enemy>().Length + FindObjectsOfType<VirusSpawner>().Length;
        UpdateEnemyCountText(0);
    }

    public void UpdateEnemyCountText(int amount)
    {
        Debug.Log("Updating enemy count");
        numEnemies += amount;
        text.text = numEnemies.ToString();
    }
}
