using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmorText : MonoBehaviour
{
    TextMeshProUGUI text;
    PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Tank>().GetComponent<PlayerStats>();
        text = GetComponent<TextMeshProUGUI>();
        UpdateArmorText();
    }

    public void UpdateArmorText()
    {
        text.text = player.health.ToString();
    }
}
