using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Pathfinding pathfinding;
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        StartCoroutine(MoveCoroutine(GetNextMove()));
    }

    private Vector2 GetNextMove()
    {
        return pathfinding.path[0].worldPosition;
    }

    public IEnumerator MoveCoroutine(Vector2 newPosition)
    {
        float duration = GameManager.timePerTurn;

        float timeCount = 0f;
        while (timeCount < duration)
        {
            timeCount += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newPosition, timeCount / duration);
            yield return new WaitForEndOfFrame();
        }

        transform.position = newPosition;
    }
}
