using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmorText : MonoBehaviour
{
    TextMeshProUGUI text;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<Tank>().health;
        EventBroker.PlayerDamaged += UpdateArmorText;
        text = GetComponent<TextMeshProUGUI>();
        UpdateArmorText(0);
    }

    public void UpdateArmorText(int damage)
    {
        health -= damage;
        text.text = health.ToString();
    }
}
