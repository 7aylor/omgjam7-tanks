using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SpawnersText : MonoBehaviour
{
    TextMeshProUGUI text;
    int numSpawners = 0;
    PlayerStats player;

    Portal portal;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        numSpawners = FindObjectsOfType<VirusSpawner>().Length;
        portal = FindObjectOfType<Portal>();
        UpdateSpawnersCountText(0);
    }

    public void UpdateSpawnersCountText(int amount)
    {
        if(numSpawners > 0)
        {
            numSpawners += amount;
            if (numSpawners == 0)
                portal.EnablePortal();

            text.text = numSpawners.ToString();
        }
    }
}
