using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SpawnersText : MonoBehaviour
{
    TextMeshProUGUI text;
    int numSpawners = 0;
    PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        numSpawners = FindObjectsOfType<VirusSpawner>().Length;
        UpdateSpawnersCountText(0);
    }

    public void UpdateSpawnersCountText(int amount)
    {
        Debug.Log("Updating Spanwer count");
        numSpawners += amount;
        text.text = numSpawners.ToString();
    }
}
