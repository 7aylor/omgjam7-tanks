using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Enemy[] enemies;

    [SerializeField]
    Projectile[] projectiles;

    [SerializeField]
    public static float timePerTurn = 0.5f;

    VirusSpawner[] spawners;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerActionTaken()
    {
        MoveAllObjects();
        SpawnViruses();
    }

    private void MoveAllObjects()
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

    private void SpawnViruses()
    {
        spawners = FindObjectsOfType<VirusSpawner>();

        foreach(VirusSpawner spawner in spawners)
        {
            spawner.DecideToSpawnVirus();
        }
    }
}
