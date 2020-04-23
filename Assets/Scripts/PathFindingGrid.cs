using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingGrid : MonoBehaviour
{
    public LayerMask[] unwalkableLayers;
    PathfindingNode[,] grid;
    public Vector2 gridWorldSize;

    int gridSizeX;
    int gridSizeY;

    private void Start()
    {
        foreach(var layer in unwalkableLayers)
        {
            Debug.Log(layer.ToString());
            Debug.Log(layer.value);
        }

        gridSizeX = (int)gridWorldSize.x;
        gridSizeY = (int)gridWorldSize.y;
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new PathfindingNode[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * (gridSizeX / 2) - Vector2.up * (gridSizeY / 2);   

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + (Vector2.right * (x + 0.5f)) + (Vector2.up * (y + 0.5f));

                bool walkable = !Physics2D.OverlapBox(worldPoint, Vector2.one * 0.5f, 0, LayerMask.GetMask("Walls", "Obstacles", "Spawners"))? true : false;
                grid[x, y] = new PathfindingNode(walkable, worldPoint);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));

        if(grid != null)
        {
            foreach(var node in grid)
            {
                Gizmos.color = node.isWalkable ? new Color(0,1,0,0.5f) : new Color(1, 0, 0, 0.5f);
                Gizmos.DrawCube(node.worldPosition, Vector3.one * 0.9f);
            }
        }
    }
}
