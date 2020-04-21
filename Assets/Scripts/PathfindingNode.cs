using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode
{
    public bool isWalkable;
    public Vector2 worldPosition;

    public PathfindingNode(bool _isWalkable, Vector2 _worldPosition)
    {
        isWalkable = _isWalkable;
        worldPosition = _worldPosition;
    }
}
