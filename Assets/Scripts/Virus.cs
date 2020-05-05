using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tank tank = collision.GetComponent<Tank>();
        if(tank != null)
        {
            //kill this virus
            PlayerStats virusStats = GetComponent<PlayerStats>();
            virusStats.DamagePlayer(int.MaxValue);

            //damage player
            EventBroker.InvokePlayerDamaged(virusStats.damage);
        }
    }
}
