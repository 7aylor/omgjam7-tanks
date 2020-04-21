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
        gridSizeX = (int)gridWorldSize.x;
        gridSizeY = (int)gridWorldSize.y;
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new PathfindingNode[gridSizeX, gridSizeY];

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
    }
}
