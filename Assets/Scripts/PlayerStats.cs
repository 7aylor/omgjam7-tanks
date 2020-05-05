using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 3;

    public int damage = 1;
    public bool hasDied = false;
    Animator animator;
    EnemyText enemyText;
    ArmorText armorText;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyText = FindObjectOfType<EnemyText>();
        armorText = FindObjectOfType<ArmorText>();
    }

    /// <summary>
    /// Called from object giving damage. Kills player if health less than or equal to 0
    /// </summary>
    /// <param name="damage">amount damage given</param>
    public void DamagePlayer(int damage)
    {
        health -= damage;
        if (health <= 0 && !hasDied)
        {
            hasDied = true;
            var spawner = GetComponent<VirusSpawner>();

            if (spawner == null)
            {
                //enemyText.UpdateEnemyCountText(-1);
                EventBroker.InvokeEnemiesChanged(-1);
                animator.SetTrigger("Death");
            }
            else
            {
                spawner.DestroyVirusSpawner();
            }
        }
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
    }
}
