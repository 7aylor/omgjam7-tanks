using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode
{
    public bool isWalkable;
    public Vector3 worldPosition;
    public PathfindingNode parent;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public PathfindingNode(bool _isWalkable, Vector2 _worldPosition, int _gridX, int _gridY)
    {
        isWalkable = _isWalkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }
}
