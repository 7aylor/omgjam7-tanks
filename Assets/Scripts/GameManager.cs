using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void TankDeathHandler();

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Enemy[] enemies;

    [SerializeField]
    Projectile[] projectiles;

    [SerializeField]
    public static float timePerTurn = 0.5f;

    VirusSpawner[] spawners;

    private void Awake()
    {
        //singleton
        var gameManagers = FindObjectsOfType<GameManager>();

        if (gameManagers.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerActionTaken()
    {
        FindObjectOfType<PathFindingGrid>().CreateGrid();
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

    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene + 1 <= SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentScene + 1);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
