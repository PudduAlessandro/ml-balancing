using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DjikstraNode : IComparable<DjikstraNode>
{
    public bool walkable;
    public Vector3Int position;
    public int cost;
    public TileBase tile;
    public List<Vector3Int> pathToNode; // New property to store the path to this node
    public DjikstraNode parent;
    
    public DjikstraNode(Vector3Int position, bool walkable, int cost, TileBase tile)
    { 
        this.position = position;
        this.walkable = walkable;
        this.cost = cost;
        this.tile = tile;
        this.pathToNode = new List<Vector3Int>();
        this.parent = null;
    }

    public int CompareTo(DjikstraNode other)
    {
        if (other == null)
        {
            return 1;
        }

        return cost.CompareTo(other.cost);
    }
}
