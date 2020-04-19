using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    [SerializeField]
    Sprite sprite;

    [SerializeField]
    GameObject virus;

    [SerializeField]
    int turnsToWaitUntilNextSpawn = 1;
    int turnsSinceLastSpawn = 0;

    GameObject enemiesParent;

    Animator animator;
    SpriteRenderer spriteRenderer;

    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemiesParent = FindObjectOfType<EnemiesParent>().gameObject;
    }

    private void Update()
    {
        if(isAlive)
        {
            FindOpenSpawnPosition();
        }
    }

    public void DecideToSpawnVirus()
    {
        turnsSinceLastSpawn++;
        if(isAlive && turnsSinceLastSpawn >= turnsToWaitUntilNextSpawn)
        {
            SpawnVirus();
            turnsSinceLastSpawn = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            DestroyVirusSpawner();
        }
    }

    public void DestroyVirusSpawner()
    {
        //player glass breaking sound
        spriteRenderer.sprite = sprite;
        isAlive = false;
        Destroy(animator);
    }

    public void SpawnVirus()
    {
        Vector3? spawnPosition = FindOpenSpawnPosition();
        if(spawnPosition != null)
        {
            Instantiate(virus, (Vector3)spawnPosition, Quaternion.identity, enemiesParent.transform);
        }
    }

    private Vector3? FindOpenSpawnPosition()
    {
        for(int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                //ignore position of spanwer
                if(x == 0 && y == 0)
                {
                    continue;
                }
                else
                {
                    LayerMask layerMask = LayerMask.GetMask("Obstacles", "Walls", "Players", "Enemies", "Spawners");
                    RaycastHit hit;

                    Vector2 castOrigin = (Vector2)transform.position;
                    Vector2 newDirection = new Vector2(x, y);
                    Vector3 newPosition = (Vector3)(newDirection + castOrigin);

                    if (!Physics2D.Raycast(castOrigin, newDirection, 1.25f, layerMask))
                    {
                        return newPosition;
                    }
                    else
                    {
                        Debug.DrawLine(castOrigin, newPosition, Color.red);
                    }
                }
            }
        }

        return null;
    }
}
