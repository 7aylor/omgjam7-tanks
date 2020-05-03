using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Pathfinding pathfinding;

    [SerializeField]
    GameObject directionArrow;

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
        if(pathfinding.path.Count > 0)
        {
            Vector2 newPosition = GetNextMove();
            StartCoroutine(MoveCoroutine(newPosition));
        }
    }

    public void UpdateDirectionArrowPosition(Vector2 newPosition)
    {
        directionArrow.transform.position = newPosition;

        Vector2 direction = new Vector2(transform.position.x - newPosition.x, transform.position.y - newPosition.y);

        if(direction == Vector2.up)
        {
            directionArrow.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (direction == Vector2.down)
        {
            directionArrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == Vector2.left)
        {
            directionArrow.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (direction == Vector2.right)
        {
            directionArrow.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

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
