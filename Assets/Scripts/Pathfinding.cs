using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    PathFindingGrid grid;

    private void Awake()
    {
        grid = GetComponent<PathFindingGrid>();
    }

    void FindPath(Vector2 startPosition, Vector2 targetPosition)
    {
        PathfindingNode startNode = grid.GetNodeFromWorldPoint(startPosition);
        PathfindingNode targetNode = grid.GetNodeFromWorldPoint(targetPosition);


    }
}
