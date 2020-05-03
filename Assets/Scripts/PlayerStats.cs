using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 3;

    public int damage = 1;
    Animator animator;
    GameManager gameManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Called from object giving damage. Kills player if health less than or equal to 0
    /// </summary>
    /// <param name="damage">amount damage given</param>
    public void DamagePlayer(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (gameObject.tag == "Player")
            {
                FindObjectOfType<Tank>().TriggerDeathAnimation();
            }
            else
            {
                gameManager.EnemyDestroyed();
                animator.SetTrigger("Death");
            }
        }

        if (gameObject.tag == "Player")
        {
            gameManager.ArmorDamaged();
        }
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
    }
}
