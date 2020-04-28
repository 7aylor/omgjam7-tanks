using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    PathFindingGrid grid;
    public Transform seeker;
    public Transform target;

    private void Awake()
    {
        grid = GetComponent<PathFindingGrid>();
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector2 startPosition, Vector2 targetPosition)
    {
        PathfindingNode startNode = grid.GetNodeFromWorldPoint(startPosition);
        PathfindingNode targetNode = grid.GetNodeFromWorldPoint(targetPosition);

        List<PathfindingNode> openSet = new List<PathfindingNode>();
        HashSet<PathfindingNode> closedSet = new HashSet<PathfindingNode>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            PathfindingNode currentNode = openSet[0];
            for(int i = 0; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost || 
                    openSet[i].fCost == currentNode.fCost)
                {
                    if (openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if(currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (PathfindingNode neighbor in grid.GetNeighbors(currentNode))
                {
                    if(!neighbor.isWalkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);

                    if(newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;

                        if(!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
        }
    }

    void RetracePath(PathfindingNode startNode, PathfindingNode endNode)
    {
        List<PathfindingNode> path = new List<PathfindingNode>();
        PathfindingNode currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;
    }

    int GetDistance(PathfindingNode nodeA, PathfindingNode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(distX > distY)
        {
            return (14 * distY) + (10 * (distX - distY));
        }
        else
        {
            return (14 * distX) + (10 * (distY - distX));
        }
    }
}
