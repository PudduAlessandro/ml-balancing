using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarBot : MonoBehaviour
{
    public Tilemap tilemap;
    public string targetTileName;
    public Vector3Int currentPosition;
    public bool lockCurrentTargetType;

    private Grid grid;
    public Queue<Vector3Int> path;

    public void SetupBot()
    {
        tilemap.CompressBounds();
        grid = tilemap.GetComponent<Grid>();
    }

    public void FindPath()
    {
        Vector3Int targetPosition = FindTargetTilePosition();
        path = FindPath(currentPosition, targetPosition);
    }
    
    Vector3Int FindTargetTilePosition()
    {
        Vector3Int targetPosition = Vector3Int.zero;

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(pos).name != targetTileName) continue;
            
            targetPosition = pos;
            break;
        }

        return targetPosition;
    }
    
    public Vector3Int MoveOneStep()
    {
        if (path != null && path.Count > 0)
        {
            if (path.Peek() == currentPosition) path.Dequeue();
            
            return MoveToNextTile();
        }

        return Vector3Int.forward;
    }
    
    private Vector3Int MoveToNextTile()
    {
        Vector3Int nextTile = path.Dequeue();

        // Check if the destination tile is the target tile
        if (nextTile == FindTargetTilePosition())
        {

        }

        return nextTile;
    }
    
    
    public static Queue<Vector3Int> FindPath(Vector3Int start, Vector3Int goal)
    {
        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
        HashSet<Vector3Int> openSet = new HashSet<Vector3Int>() { start };
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        Dictionary<Vector3Int, float> gScore = new Dictionary<Vector3Int, float> { { start, 0 } };
        Dictionary<Vector3Int, float> fScore = new Dictionary<Vector3Int, float> { { start, HeuristicCostEstimate(start, goal) } };

        while (openSet.Count > 0)
        {
            Vector3Int current = GetLowestFScore(openSet, fScore);
            if (current == goal)
                return ReconstructPath(cameFrom, goal);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float tentativeGScore = gScore[current] + 1; // Assuming all tile costs are the same (1)
                if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, goal);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }

    static float HeuristicCostEstimate(Vector3Int start, Vector3Int goal)
    {
        return Vector3Int.Distance(start, goal);
    }

    static Vector3Int GetLowestFScore(HashSet<Vector3Int> openSet, Dictionary<Vector3Int, float> fScore)
    {
        float lowestFScore = float.MaxValue;
        Vector3Int lowestFScoreNode = Vector3Int.zero;

        foreach (Vector3Int node in openSet)
        {
            if (fScore.ContainsKey(node) && fScore[node] < lowestFScore)
            {
                lowestFScore = fScore[node];
                lowestFScoreNode = node;
            }
        }

        return lowestFScoreNode;
    }

    static Queue<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        Queue<Vector3Int> path = new Queue<Vector3Int>();
        path.Enqueue(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Enqueue(current);
        }

        return new Queue<Vector3Int>(path.Reverse());
    }

    static List<Vector3Int> GetNeighbors(Vector3Int current)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>
        {
            current + new Vector3Int(1, 0, 0), // Right
            current + new Vector3Int(-1, 0, 0), // Left
            current + new Vector3Int(0, 1, 0), // Up
            current + new Vector3Int(0, -1, 0) // Down
        };

        return neighbors;
    }
}
