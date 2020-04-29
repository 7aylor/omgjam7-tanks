using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    PathFindingGrid grid;
    public List<PathfindingNode> path;
    private Transform target;

    private void Awake()
    {
        target = FindObjectOfType<Tank>().transform;
        path = new List<PathfindingNode>();
        grid = FindObjectOfType<PathFindingGrid>();
    }

    public void UpdatePath()
    {
        FindPath(transform.position, target.position);
        //for(int i = 0; i < path.Count; i++)
        //{
        //    Debug.Log(name + "--" + path[i].gridX + ", " + path[i].gridY + " : " + path[i].worldPosition.x + ", " + path[i].worldPosition.y);
        //}
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
        List<PathfindingNode> newPath = new List<PathfindingNode>();
        PathfindingNode currentNode = endNode;

        while(currentNode != startNode)
        {
            newPath.Add(currentNode);
            currentNode = currentNode.parent;
            if (!grid.paths.Contains(currentNode))
            {
                grid.paths.Add(currentNode);
            }
        }

        newPath.Reverse();
        path = newPath;
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
