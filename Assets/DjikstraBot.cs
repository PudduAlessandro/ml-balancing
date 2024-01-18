using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class DjikstraBot : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int closestTargetTile;
    public List<string> targetTileTypes; // Specify the tile types the bot is interested in
    [FormerlySerializedAs("currentTile")] public Vector3Int currentPosition;
    
    private Dictionary<Vector3Int, DjikstraNode> nodes = new();
    private List<Vector3Int> path = new();
    private int currentPathIndex = 0;
    

    public void SetupBot()
    {
        InitializeNodes();
        
    }

    public Vector3Int Move()
    {
        FindClosestTile();
        return MoveAlongPath();
    }

    void InitializeNodes()
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(position) != null)
            {
                TileBase tile = tilemap.GetTile(position);
                bool walkable = IsTileWalkable(tile);
                int cost = walkable ? 1 : int.MaxValue; // Set cost for walkable and non-walkable tiles accordingly

                DjikstraNode node = new DjikstraNode(position, walkable, cost, tile);
                nodes[position] = node;
            }
        }
    }

    void FindClosestTile()
    {
        Vector3Int startCell = currentPosition;
        PriorityQueue<DjikstraNode> queue = new PriorityQueue<DjikstraNode>();
        DjikstraNode startNode = nodes[startCell];
        startNode.cost = 0;
        queue.Enqueue(startNode);

        bool foundTarget = false;

        while (queue.Count > 0)
        {
            DjikstraNode current = queue.Dequeue();

            if (IsTargetTile(current))
            {
                path = ReconstructPath(current);
                foundTarget = true;
                break;
            }

            foreach (var neighbor in GetNeighbors(current.position))
            {
                int newCost = current.cost + neighbor.cost;
                if (newCost < neighbor.cost)
                {
                    neighbor.cost = newCost;
                    neighbor.pathToNode = new List<Vector3Int>(current.pathToNode);
                    neighbor.pathToNode.Add(neighbor.position); // Add current neighbor to the path
                    queue.Enqueue(neighbor);
                }
            }
        }

        if (!foundTarget)
        {
            Debug.LogWarning("No valid path to the target tile.");
        }
    }
    
    List<Vector3Int> ReconstructPath(DjikstraNode endNode)
    {
        List<Vector3Int> reversedPath = new List<Vector3Int>();

        while (endNode != null)
        {
            reversedPath.Add(endNode.position);
            endNode = endNode.parent;
        }

        reversedPath.Reverse();
        return reversedPath;
    }

    private Vector3Int MoveAlongPath()
    {
        if (path.Count == 0 || currentPathIndex >= path.Count)
            return Vector3Int.forward;

        Vector3Int targetPosition = path[currentPathIndex];
        currentPathIndex++;

        return targetPosition;
    }

    List<DjikstraNode> GetNeighbors(Vector3Int position)
    {
        List<DjikstraNode> neighbors = new List<DjikstraNode>();

        Vector3Int[] directions = {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };

        foreach (var direction in directions)
        {
            Vector3Int neighborPos = position + direction;
            if (nodes.ContainsKey(neighborPos) && nodes[neighborPos].walkable)  // Only include walkable neighbors
            {
                neighbors.Add(nodes[neighborPos]);
            }
        }

        return neighbors;
    }

    bool IsTileWalkable(TileBase tile)
    {
        return !tile.name.Equals("2_WALL") && !tile.name.Equals("4_WATER");
    }

    bool IsTargetTile(DjikstraNode node)
    {
        // Check if the current node's tile matches the target tile type
        foreach (string targetTileType in targetTileTypes)
        {
            if (node.tile.name.Equals(targetTileType))
                return true;
        }

        return false;
    }
}
