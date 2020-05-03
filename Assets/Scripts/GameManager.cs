using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TankDeathHandler();
public delegate void ArmorDamagedHandler();
public delegate void EnemySpawnedHandler(int amount);
public delegate void EnemyDestroyedHandler(int amount);

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Enemy[] enemies;

    [SerializeField]
    Projectile[] projectiles;

    [SerializeField]
    public static float timePerTurn = 0.5f;

    VirusSpawner[] spawners;

    [SerializeField]
    EnemyText enemyText;

    [SerializeField]
    ArmorText armorText;

    public event ArmorDamagedHandler armorDamaged;
    public event EnemyDestroyedHandler enemyDestroyed;
    public event EnemySpawnedHandler enemySpawned;

    public void PlayerActionTaken()
    {
        MoveAllObjects();
        SpawnViruses();
    }

    private void Start()
    {
        armorDamaged += armorText.UpdateArmorText;
        enemySpawned += enemyText.UpdateEnemyCountText;
        enemyDestroyed += enemyText.UpdateEnemyCountText;
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

    public void EnemySpawned()
    {
        if (enemySpawned != null)
            enemySpawned(1);
    }

    public void EnemyDestroyed()
    {
        if (enemySpawned != null)
            enemySpawned(-1);
    }

    public void ArmorDamaged()
    {
        if (armorDamaged != null)
            armorDamaged();
    }
}
