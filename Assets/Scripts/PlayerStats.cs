using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    int health = 3;

    /// <summary>
    /// Called from object giving damage. Kills player if health less than or equal to 0
    /// </summary>
    /// <param name="damage">amount damage given</param>
    public void DamagePlayer(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
