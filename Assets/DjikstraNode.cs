using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DjikstraNode
{
    public bool walkable;
    public Vector3Int position;
    public int cost;
    public TileBase tile;
    
    public DjikstraNode(Vector3Int position, bool walkable, int cost, TileBase tile)
    { 
        this.position = position;
        this.walkable = walkable;
        this.cost = cost;
        this.tile = tile;
    }
}
