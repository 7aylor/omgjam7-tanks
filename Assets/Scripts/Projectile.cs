using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    int damage = 1;

    [SerializeField]
    float moveSpeed = 2f;

    public Vector2 Direction;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats otherPlayerStats = collision.gameObject.GetComponent<PlayerStats>();

        if(otherPlayerStats != null && collision.tag != "Player")
        {
            otherPlayerStats.DamagePlayer(damage);
        }

        if(collision.tag != "Player")
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity, null);
            newExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, -3);
            newExplosion.transform.eulerAngles = GetNewExplosionRotation();
            Destroy(gameObject);
        }
    }

    private Vector3 GetNewExplosionRotation()
    {
        if(Direction == Vector2.down)
        {
            return new Vector3(0, 0, 0);
        }
        else if (Direction == Vector2.up)
        {
            return new Vector3(0, 0, 180);
        }
        else if (Direction == Vector2.left)
        {
            return new Vector3(0, 0, 270);
        }
        else
        {
            return new Vector3(0, 0, 90);
        }
    }
    
    //Called from gamemanager
    public void Move()
    {
        StartCoroutine(MoveProjectileCoroutine());
    }

    private IEnumerator MoveProjectileCoroutine()
    {
        Vector2 startPosition = transform.position;
        Vector2 newPosition = (Vector2)transform.position + Direction;

        //Rotate body and turrent if needed
        //play sound
        //while (Vector2.Distance(transform.position, newPosition) > 0.05f)
        //{
        //    transform.Translate(Direction * Time.deltaTime * moveSpeed);
        //    yield return new WaitForEndOfFrame();
        //}

        float duration = GameManager.timePerTurn;
        float timeCount = 0f;
        while (timeCount < duration)
        {
            timeCount += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, newPosition, timeCount / duration);
            yield return new WaitForEndOfFrame();
        }

        transform.position = newPosition;
    }
}
