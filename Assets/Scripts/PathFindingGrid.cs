using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingGrid : MonoBehaviour
{
    public LayerMask[] unwalkableLayers;
    PathfindingNode[,] grid;
    public Vector2 gridWorldSize;
    public Transform player;
    public List<PathfindingNode> paths;
    public Pathfinding[] enemyPaths;

    int gridSizeX;
    int gridSizeY;

    private void Awake()
    {
        //singleton
        var grids = FindObjectsOfType<PathFindingGrid>();

        if(grids.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        paths = new List<PathfindingNode>();
        foreach(var layer in unwalkableLayers)
        {
            Debug.Log(layer.ToString());
            Debug.Log(layer.value);
        }

        gridSizeX = (int)gridWorldSize.x;
        gridSizeY = (int)gridWorldSize.y;
        CreateGrid();
        UpdateAllPaths();
    }

    public void UpdateAllPaths()
    {
        paths.Clear();
        enemyPaths = FindObjectsOfType<Pathfinding>();
        foreach(var enemyPath in enemyPaths)
        {
            enemyPath.UpdatePath();
        }
    }

    private void CreateGrid()
    {
        grid = new PathfindingNode[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * (gridWorldSize.x / 2) - Vector2.up * (gridWorldSize.y / 2);   

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + (Vector2.right * (x + 0.5f)) + (Vector2.up * (y + 0.5f));

                bool walkable = !Physics2D.OverlapBox(worldPoint, Vector2.one * 0.5f, 0, LayerMask.GetMask("Walls", "Obstacles", "Spawners", "Enemies"))? true : false;
                grid[x, y] = new PathfindingNode(walkable, worldPoint, x, y);
            }
        }
    }

    public List<PathfindingNode> GetNeighbors(PathfindingNode node)
    {
        List<PathfindingNode> neighbors = new List<PathfindingNode>();
        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //ignore center and corners
                if(x == 0 && y == 0 ||
                   x == -1 && y == -1 ||
                   x == -1 && y == 1 ||
                   x == 1 && y == -1 ||
                   x == 1 && y == 1)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    public PathfindingNode GetNodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x - 0.5f + (gridWorldSize.x / 2)) / gridWorldSize.x;
        float percentY = (worldPosition.y + 0.5f + (gridWorldSize.y / 2)) / gridWorldSize.y;
        Debug.Log(percentX + ", " +percentY);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        Debug.Log(percentX + ", " +percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));

        if(grid != null)
        {
            PathfindingNode playerNode = GetNodeFromWorldPoint(player.position);
            foreach (var node in grid)
            {
                Gizmos.color = node.isWalkable ? new Color(0,1,0,0.5f) : new Color(1, 0, 0, 0.5f);
                if(paths != null)
                {
                    if(paths.Contains(node))
                    {
                        Gizmos.color = new Color(0,0,0,0.5f);
                    }
                }
                if(playerNode == node)
                {
                    Gizmos.color = new Color(0,1,1,0.5f);
                }
                Gizmos.DrawCube(node.worldPosition, Vector3.one * 0.9f);
            }
        }
    }
}
