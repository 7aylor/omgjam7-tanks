using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Enemy[] enemies;

    [SerializeField]
    Projectile[] projectiles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveAllObjects()
    {
        enemies = FindObjectsOfType<Enemy>();
        projectiles = FindObjectsOfType<Projectile>();

        foreach(Enemy enemy in enemies)
        {
            enemy.Move();
        }

        foreach(Projectile projectile in projectiles)
        {
            projectile.Move();
        }
    }
}
